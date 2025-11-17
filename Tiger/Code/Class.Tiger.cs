using System;
using System.Collections.Concurrent;
using System.Collections.Generic; // Added for List<string> in AppendAllLines
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Vi.Extensions.String;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Vi.Statics
{
    /// <summary>
    /// Provides a static, asynchronous logging utility for handling normal, error, and emergency logs.
    /// <para>Normal logs consist of simple text messages, while error logs are used for exceptions and managed errors.</para>
    /// <para>The emergency log file is used as a fallback when an internal exception, such as a file being locked, prevents writing to the primary log.
    /// This allows the system to attempt writing to an alternative location.</para>
    /// <para>Write operations occur on a separate thread to ensure non-blocking log operations.
    /// Robust error handling guarantees the highest possible probability of a successful write.</para>
    /// <para>As a final measure, the <see cref="OnDataLost"/> event is raised to allow the application to handle data loss gracefully.</para>
    /// </summary>
    public static partial class Tiger
    {
        #region Threading elements
        private static BlockingCollection<LogEntryE> LogQueueE = new BlockingCollection<LogEntryE>();
        private static BlockingCollection<LogEntryN> LogQueueN = new BlockingCollection<LogEntryN>();
        private static Thread WriterThreadE;
        private static Thread WriterThreadN;
        private static volatile bool IsRunning = true;
        private static readonly object LockObject = new object(); // Oggetto per il lock
        #endregion

        #region Fields
        /// <summary>
        /// Counter for exception log entries. Useful for detecting sequence gaps, which might indicate data loss.
        /// </summary>
        private static int CounterE = 0;

        /// <summary>
        /// Counter for normal log entries. Useful for detecting sequence gaps, which might indicate data loss.
        /// </summary>
        private static int CounterN = 0;

        /// <summary>
        /// Gets the "AppContext.BaseDirectory;", which is the base directory of the application.
        /// </summary>
        public static string BaseDirectory { get; } = AppContext.BaseDirectory;

        /*
        public static string LogRoot{ get; private set; }
        */

        /// <summary>
        /// Gets the full path to the current normal log file (ending with '.N.log').
        /// </summary>
        public static string LogFileN { get; private set; }

        /// <summary>
        /// Gets the full path to the current error log file (ending with '.E.log').
        /// </summary>
        public static string LogFileE { get; private set; }

        /// <summary>
        /// Gets the full path to the current emergency log file (ending with '.X.log').
        /// </summary>
        public static string LogFileX { get; private set; }

        #endregion

        #region Delegates & Events
        /// <summary>
        /// Represents the logging levels.
        /// </summary>
        /// <param name="level">The severity level of the log message.</param>
        /// <param name="message">The content of the log message.</param>
        /// <param name="line">Automatically populated with the line number in the source file where this method is called.</param>
        /// <param name="member">Automatically populated with the name of the calling member (method, property, etc.).</param>
        /// <param name="file">Automatically populated with the full path of the source file where this method is called.</param>
        public delegate void LogDelegate(Vi.Logger.Levels level, string message, int line, string member, string file);

        /// <summary>
        /// Occurs when a log message is processed, allowing external subscribers to receive log events.
        /// </summary>
        public static event LogDelegate OnLog;

        /// <summary>
        /// Represents a delegate for emergency logging events.
        /// </summary>
        /// <param name="message">The emergency message to be sent.</param>
        public delegate void EmergencyDelegate(string message);
        /// <summary>
        /// Occurs when an **internal exception** prevents writing to the primary log files,
        /// causing the system to attempt writing to the emergency log file (<see cref="LogFileX"/>).
        /// </summary>
        public static event EmergencyDelegate OnEmergency;

        /// <summary>
        /// Represents a delegate for data loss events.
        /// </summary>
        /// <param name="message">The message indicating the data loss.</param>
        public delegate void DataLostDelegate(string message);

        // Note: Original was EmergencyDelegate, changed to DataLostDelegate for clarity if it's a distinct event type. Reverted for consistency with original.
        /// <summary>
        /// The OnDataLost event is triggered as a last resort when all attempts to save a log have failed. (Even the emergency log file <see cref="LogFileX"/>) 
        /// The purpose is to give the event handler a final chance to record the message using an alternative method.
        /// </summary>
        public static event EmergencyDelegate OnDataLost; 
        #endregion

        #region Nested classes: LogEntry, LogEntryN, LogEntryE
        /// <summary>
        /// Abstract base class for all log data entries, providing common timestamping, contextual information, and formatting.
        /// </summary>
        private abstract partial class LogEntry { }

        /// <summary>
        /// Specializes <see cref="LogEntry"/> for standard informational or error log entries that carry a simple message.
        /// </summary>
        private partial class LogEntryN : LogEntry { /* Implemented in another file to keep light this file */ }

        /// <summary>
        /// Specializes <see cref="LogEntry"/> for logging exception details, including stack traces.
        /// </summary>
        private partial class LogEntryE : LogEntry { /* Implemented in another file to keep light this file */ }
        #endregion

        #region enums
        /// <summary>
        /// Specifies the type of log file.
        /// </summary>
        private enum LogType
        {
            /// <summary>
            /// Normal log file, used for general informational, debug, and warning messages.
            /// </summary>
            N,

            /// <summary>
            /// Error log file, specifically used for logging exceptions and managed errors.
            /// </summary>
            E,

            /// <summary>
            /// Emergency log file, created as a fallback when an exception occurs during the writing
            /// of the normal or error log files. Ensures critical messages are still recorded.
            /// </summary>
            X,
        }
        #endregion

        #region CTor
        /// <summary>
        /// Static constructor for <see cref="Tiger"/>.
        /// Initializes log file paths and starts the two dedicated background threads for normal and error log writing.
        /// It also registers an event handler for application exit to ensure graceful shutdown.
        /// </summary>
        /// <remarks>
        /// This constructor is automatically called by the .NET runtime when the <see cref="Tiger"/> class is first accessed.
        /// It sets up the logging infrastructure, including file paths and background worker threads,
        /// to ensure non-blocking log operations.
        /// </remarks>
        static Tiger()
        {
            SetLogFiles();

            Tiger.WriterThreadN = new Thread(ProcessLogQueueN);
            Tiger.WriterThreadN.IsBackground = true;
            Tiger.WriterThreadN.Start();

            Tiger.WriterThreadE = new Thread(ProcessLogQueueE);
            Tiger.WriterThreadE.IsBackground = true;
            Tiger.WriterThreadE.Start();

            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
        }
        #endregion

        #region Event handlers
        /// <summary>
        /// Handles the <see cref="AppDomain.CurrentDomain.ProcessExit"/> event.
        /// This method is called when the application is shutting down to ensure
        /// that all buffered log data is flushed to the disk and the writer threads are properly terminated.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        private static void OnProcessExit(object sender, EventArgs e)
        {
            Tiger.IsRunning = false; // Signal writer threads to stop taking new messages

            double timeout = 5; // Timeout in seconds for threads to complete

            // Complete adding to queues and wait for writer threads to finish processing remaining items
            Tiger.LogQueueN.CompleteAdding();
            Tiger.WriterThreadN.Join(TimeSpan.FromSeconds(timeout)); // Wait for N-log thread to finish gracefully

            Tiger.LogQueueE.CompleteAdding();
            Tiger.WriterThreadE.Join(TimeSpan.FromSeconds(timeout)); // Wait for E-log thread to finish gracefully
        }

        /// <summary>
        /// Handles internal logging errors that occur during the process of writing logs to the file system.
        /// Attempts to write the original log data to an emergency log file (<see cref="LogFileX"/>) and
        /// raises the <see cref="OnEmergency"/> event. If even this fails, it raises <see cref="OnDataLost"/>.
        /// </summary>
        /// <param name="text">The raw log text that failed to be written to the primary log file.</param>
        /// <param name="message">A contextual message describing the log writing failure, sent with the event.</param>
        private static void _OnLogError(string text, string message)
        {
            string fallbackMessage = $"Log on default file failure. " + Environment.NewLine + text;
            try
            {
                // Attempt to write the failed log data to the emergency log file.
                System.IO.File.AppendAllText(LogFileX, text);
                // Notify subscribers that an emergency logging situation has occurred.
                OnEmergency?.Invoke(message);
            }
            catch (System.Exception se)
            {
                // If writing to the emergency log file also fails, signal critical data loss.
                string criticalErrorMessage = $"Failed to write even to emergency file '{LogFileX}'. Original log message lost: {message}. Last error: {se.Message}";
                OnDataLost?.Invoke(message); // Notify subscribers of irrecoverable data loss.
            }
        }
        #endregion

        #region ProcessLogQueue
        /// <summary>
        /// Processes a <see cref="BlockingCollection{T}"/> of log data entries in a background thread,
        /// writing them to the appropriate log file in batches. Handles file rotation based on date changes.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="LogEntry"/> being processed (e.g., <see cref="LogEntryE"/> or <see cref="LogDataE"/>).</typeparam>
        /// <param name="logQueue">The <see cref="BlockingCollection{T}"/> from which log entries are consumed.</param>
        /// <param name="getLogFilePath">A function that returns the current full path of the target log file.</param>
        /// <remarks>
        /// This method continuously pulls log entries from the queue, buffers them, and writes them in batches
        /// to minimise file I/O operations. It also includes logic for daily log file rotation and ensures
        /// all remaining logs are flushed during shutdown.
        /// </remarks>
        private static void ProcessLogQueue<T>(BlockingCollection<T> logQueue, Func<string> getLogFilePath) where T : LogEntry
        {
            List<string> logBuffer = new List<string>();
            const int BATCH_SIZE = 10; // Number of log entries to buffer before writing to file.

            try
            {
                // Continue processing as long as the queue is not marked as completed OR
                // there are still items in the queue OR there are items in the internal buffer.
                while (!logQueue.IsCompleted || logQueue.Count > 0 || logBuffer.Count > 0)
                {
                    // If the application is running, wait indefinitely for new items (-1 timeout).
                    // If shutting down, try to take existing items without waiting (0 timeout).
                    int timeoutMillis = Tiger.IsRunning ? -1 : 0;

                    T logData;

                    // this function (ProcessLogQueue) writes logs for two different
                    // file type: 'N' & 'E'. The function 'getLogFilePath' gets the
                    // rifgt full file name.
                    var logFilePath = getLogFilePath(); 

                    if (logQueue.TryTake(out logData, timeoutMillis))
                    {
                        // Check for daily log file rotation based on the timestamp of the current log entry.
                        if (!Tiger.IsSameDay(logFilePath, logData.Now.Date))
                        {
                            // Day has changed. First, flush any buffered logs to the *old* file before rotation.
                            lock (logBuffer)
                            {
                                if (logBuffer.Count > 0)
                                {
                                    AppendAllLines(logFilePath, logBuffer);
                                }
                            }

                            // Clear old logs if necessary.
                            Tiger.ClearLogs(); 
                            
                            // Then, rotate the log files. SetLogFiles will update Tiger.FileName and global static paths.
                            SetLogFiles();
                        }

                        var text = logData.ToReport((uint)logQueue.Count);

                        // Protect access to the buffer during addition and writing
                        lock (logBuffer) 
                        {
                            // If buffer size reaches batch size, write to file and clear buffer.
                            if (logQueue.Count < BATCH_SIZE)
                            {
                                logBuffer.Add(text);
                                AppendAllLines(logFilePath, logBuffer);
                            }
                            else
                            {
                                logBuffer.Add(text);
                                if (logBuffer.Count >= BATCH_SIZE)
                                {
                                    AppendAllLines(logFilePath, logBuffer);
                                }
                            }
                        }
                    }
                    // TryTake did not retrieve anything (timeout expired or queue empty/completed)
                    else
                    {
                        // If no new items were available from the queue, flush any remaining items in the buffer.
                        lock (logBuffer) // Protect access to the buffer for flushing
                        {
                            if (logBuffer.Count > 0)
                            {
                                AppendAllLines(getLogFilePath(), logBuffer);
                            }
                        }
                    }
                }
            }
            finally
            {
                // Final flush of any remaining logs in the buffer before the thread terminates.
                // This ensures no data is left unwritten during graceful shutdown.
                lock (logBuffer)
                {
                    if (logBuffer.Count > 0)
                    {
                        // Use the provided function to get the *current* log file path for the final flush.
                        AppendAllLines(getLogFilePath(), logBuffer);
                    }
                }
            }
        }

        /// <summary>
        /// The dedicated background thread method for processing and writing error log entries (<see cref="LogDataE"/>)
        /// from the internal error log queue (<see cref="Tiger.LogQueueE"/>) to the error log file (<see cref="LogFileE"/>).
        /// </summary>
        private static void ProcessLogQueueE()
        {
            ProcessLogQueue<LogEntryE>(Tiger.LogQueueE, () => Tiger.LogFileE);
        }

        /// <summary>
        /// The dedicated background thread method for processing and writing normal log entries (<see cref="LogEntryE"/>)
        /// from the internal normal log queue (<see cref="Tiger.LogQueueN"/>) to the normal log file (<see cref="LogFileN"/>).
        /// </summary>
        private static void ProcessLogQueueN()
        {
            ProcessLogQueue<LogEntryN>(Tiger.LogQueueN, () => Tiger.LogFileN);
        }
        #endregion

        #region Staff Methods
        /// <summary>
        /// Sets the full paths for the three log files: <see cref="LogFileN"/> (Normal), <see cref="LogFileE"/> (Error), and <see cref="LogFileX"/> (Emergency).
        /// File names are dynamically generated based on the current UTC date and time to ensure uniqueness and proper daily rotation.
        /// </summary>
        /// <remarks>
        /// This method includes logic to handle transitions near midnight, ensuring that new log files are created for a new day.
        /// It also checks for existing files to avoid conflicts if the application restarts very quickly on the same day.
        /// </remarks>
        private static void SetLogFiles()
        {
            var utcNow = System.DateTime.UtcNow;
            int milliseconds = 100;
            var utcNowPlus = utcNow.AddMilliseconds(milliseconds);

            // Check if the current UTC day is different from the UTC day after a small delay.
            // This helps in detecting midnight rollovers to ensure new log files are created for the new day.
            if (utcNow.Day != utcNowPlus.Day)
            {
                // If a day rollover is imminent, pause briefly and re-evaluate to ensure the new day's file name is picked up.
                System.Threading.Thread.Sleep(2 * milliseconds);
                SetLogFiles(); // Recursive call to re-check time after the delay.
            }
            else
            {
                var subDirectory = "Logs"; 
                Func<string, LogType, string> getLogFile = (path, logType) =>
                {
                    string fileName = $"{ToYyyyMMdd(utcNow)}.{utcNow:HH-mm-ss.fff}.{logType}.log";
                    path = System.IO.Path.Combine(path, subDirectory);

                    // Ensure the directory exists, creating it if necessary.
                    path.ToDirectory().Create(); 

                    return System.IO.Path.Combine(path, fileName);
                };


                var baseDirectory = Tiger.BaseDirectory;

                // Ensure the base directory exists, creating it if necessary.
                // Generate base file name for the current UTC date
                string logFileN = getLogFile(baseDirectory, LogType.N);
                string logFileE = getLogFile(baseDirectory, LogType.E);
                string logFileX = getLogFile(baseDirectory, LogType.X);

                bool LogFileNExists = System.IO.File.Exists(logFileN);
                bool LogFileEExists = System.IO.File.Exists(logFileE);
                bool LogFileXExists = System.IO.File.Exists(logFileX);

                // If any log file for the generated timestamp already exists,
                // it implies a very rapid restart or a rare timing conflict.
                // Loop briefly until a new, unique timestamp is available.
                if (LogFileNExists || LogFileEExists || LogFileXExists)
                {
                    // This loop ensures unique file names if multiple instances try to start at the exact same millisecond.
                    // A minimal sleep is used to wait for the system clock to advance.
                    do { System.Threading.Thread.Sleep(1); }
                    while (System.DateTime.UtcNow > utcNow); // Continues as long as current UTC time hasn't advanced past the initial 'utcNow'
                    Tiger.SetLogFiles(); // Recursive call to generate new file names based on the advanced time.
                }
                else
                {
                    // Assign the newly generated, unique log file paths.
                    Tiger.LogFileN = logFileN;
                    Tiger.LogFileE = logFileE;
                    Tiger.LogFileX = logFileX;

                    // ---------------------------------------------------------- //
                    // ATTENTION: Be aware this is the exit point of this method.
                    // ---------------------------------------------------------- //
                }
            }
        }

        /// <summary>
        /// Clears log files older than the specified age in days. 
        /// multiple calls in the same day.
        /// </summary>
        /// <param name="age">The age in days that sets the length of the queue.</param>
        public static void ClearLogs(uint age = 30)
        {

            var now = DateTime.Now;

            // Ensure age is at least 1 day to avoid
            // accidental deletion of today logs
            age = Math.Max(age, 1);

            //// ensures this operation is performed only once a day
            //var totalDays = (now - Tiger.LastCleaning).TotalDays;
            //if (totalDays > 0)
            //{
            var directoryName = System.IO.Path.GetDirectoryName(Tiger.LogFileN).ToDirectory();
            if (System.IO.Directory.Exists(directoryName))
            {
                var files = System.IO.Directory.GetFiles(directoryName);
                var cutoffDate = now.AddDays(-age);
                var oldFiles = files.Where(file => System.IO.File.GetLastWriteTime(file) < cutoffDate);

                foreach (string file in oldFiles)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        // Log the error if file deletion fails
                        Write(Vi.Logger.Levels.ERROR, $"Failed to delete file '{file}': {ex.Message}");
                    }
                }

            }
            //    Tiger.LastCleaning = now;
            //}
        }

        /// <summary>
        /// Converts a <see cref="DateTime"/> object to a string in the "yyyy-MM-dd" format.
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> object to convert.</param>
        /// <returns>A string representing the provided date in "yyyy-MM-dd" format.</returns>
        private static string ToYyyyMMdd(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Appends a collection of log lines to the specified log file.
        /// Includes robust error handling for common file I/O issues, redirecting to emergency logging if needed.
        /// </summary>
        /// <param name="logFilePath">The full path to the log file where lines should be appended.</param>
        /// <param name="lines">A <see cref="List{T}"/> of strings, where each string is a log entry to be written.</param>
        private static void AppendAllLines(string logFilePath, List<string> lines)
        {
            if (lines.Count > 0)
            {
                try
                {
                    // File.AppendAllLines ensures the file is opened, all lines are written, and then closed efficiently.
                    System.IO.File.AppendAllLines(logFilePath, lines);
                }
                catch (System.IO.IOException ioEx)
                {
                    _OnLogError(string.Join(Environment.NewLine, lines), $"I/O error {ioEx.Message} on '{logFilePath}'");
                }
                catch (System.UnauthorizedAccessException uaEx)
                {
                    _OnLogError(string.Join(Environment.NewLine, lines), $"Access denied for '{logFilePath}'");
                }
                catch (System.Exception ex)
                {
                    _OnLogError(string.Join(Environment.NewLine, lines), $"Unexpected exception: {ex.Message} writing on '{logFilePath}'");
                }
                finally
                {
                    // Always clear the buffer after attempting to write, regardless of success or failure.
                    lines.Clear();
                }
            }
        }

        /// <summary>
        /// Checks if a given file name (representing a date) is the same as the date of a provided <see cref="DateTime"/> object.
        /// This is used for daily log file rotation.
        /// </summary>
        /// <param name="fullFileName">The file name string to compare, typically in "yyyy-MM-dd" format (e.g., from <see cref="Tiger.FileName"/>).</param>
        /// <param name="date">The <see cref="DateTime"/> object whose date component will be compared.</param>
        /// <returns>
        /// <see langword="true"/> if the date extracted from <paramref name="fullFileName"/> matches the date of <paramref name="date2"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsSameDay(string fullFileName, DateTime date)
        {
            var fileName = System.IO.Path.GetFileName(fullFileName);
            var isSameDay = fileName.Contains(Tiger.ToYyyyMMdd(date.Date));
            return isSameDay;
        }
        #endregion

        #region   Write Methods
        /// <summary>
        /// Writes a general log message with a specified indentation and severity level.
        /// This is the primary method for adding standard log entries to the normal log file (<see cref="LogFileN"/>).
        /// </summary>
        /// <param name="indentation">The visual indentation level for the log message (e.g., 0 for no indent, 1 for 3 spaces).</param>
        /// <param name="level">The severity level of the log message (<see cref="Levels"/> enum).</param>
        /// <param name="message">The main text content of the log entry.</param>
        /// <param name="line">Automatically populated with the line number in the source file where this method is called.</param>
        /// <param name="member">Automatically populated with the name of the calling member (method, property, etc.).</param>
        /// <param name="file">Automatically populated with the full path of the source file where this method is called.</param>
        public static void Write(byte indentation, Vi.Logger.Levels level, string message, [CallerLineNumber] int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?")
        {
            int _counterN;
            int _count;


            lock (Tiger.LockObject)
            {
                _counterN = Interlocked.Increment(ref Tiger.CounterN); // thread-safe increment
                _count = Tiger.LogQueueN.Count; // thread-safe read of the queue count
            }

            Tiger.LogQueueN.Add(new LogEntryN(indentation, level, _counterN, _count, message, line, member, file));
        }

        /// <summary>
        /// Writes a general log message with a specified indentation and severity level.
        /// This is the primary method for adding standard log entries to the normal log file (<see cref="LogFileN"/>).
        /// </summary>
        /// <param name="level">The severity level of the log message (<see cref="Levels"/> enum).</param>
        /// <param name="message">The main text content of the log entry.</param>
        /// <param name="line">Automatically populated with the line number in the source file where this method is called.</param>
        /// <param name="member">Automatically populated with the name of the calling member (method, property, etc.).</param>
        /// <param name="file">Automatically populated with the full path of the source file where this method is called.</param>
        public static void Write(Vi.Logger.Levels level, string message, [CallerLineNumber] int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?")
            => Tiger.Write(indentation: 0, level, message, line, member, file);

        /// <summary>
        /// Writes an exception log entry to both the normal log file (<see cref="LogFileN"/>) (as a message)
        /// and the dedicated error log file (<see cref="LogFileE"/>) with full exception details.
        /// </summary>
        /// <param name="indentation">The visual indentation level for the log message.</param>
        /// <param name="se">The <see cref="System.Exception"/> object to be logged. Its message and stack trace will be captured.</param>
        /// <param name="line">Automatically populated with the line number in the source file where this method is called.</param>
        /// <param name="member">Automatically populated with the name of the calling member.</param>
        /// <param name="file">Automatically populated with the full path of the source file.</param>
        public static void Write(byte indentation, System.Exception se, [CallerLineNumber] int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?")
        {
            int _counterE;
            int _count;

            lock (Tiger.LockObject) // Acquisisci il lock sull'oggetto condiviso
            {
                _counterE = Interlocked.Increment(ref Tiger.CounterE); // Incrementa in modo thread-safe
                _count = Tiger.LogQueueE.Count; // Leggi il conteggio della coda in modo protetto
            }

            // Log to error queue with full exception details
            Tiger.LogQueueE.Add(new LogEntryE(indentation, _counterE, _count, se, line, member, file));

            Tiger.Write(indentation, Vi.Logger.Levels.EXCEPTION, se.Message, line, member, file);
        }

        /// <summary>
        /// Writes an exception log entry to both the normal log file (<see cref="LogFileN"/>) (as a message)
        /// and the dedicated error log file (<see cref="LogFileE"/>) with full exception details.
        /// </summary>
        /// <param name="se">The <see cref="System.Exception"/> object to be logged. Its message and stack trace will be captured.</param>
        /// <param name="line">Automatically populated with the line number in the source file where this method is called.</param>
        /// <param name="member">Automatically populated with the name of the calling member.</param>
        /// <param name="file">Automatically populated with the full path of the source file.</param>
        public static void Write(System.Exception se, [CallerLineNumber] int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?")
            => Tiger.Write(indentation: 0, se, line, member, file);

        #endregion
    }
}