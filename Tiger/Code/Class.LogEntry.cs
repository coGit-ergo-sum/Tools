using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Vi.Extensions.Double;

namespace Vi.Statics
{
    /// <summary>
    /// Tiger provides high performances logging capabilities.
    /// (About 100K log entries per second on a standard machine, added to the queue and 8K message sent to the file per second)
    /// </summary>
    public static partial class Tiger
    {
        /// <summary>
        /// Represents a base class for log entries, encapsulating common log attributes and formatting logic.
        /// </summary>
        private abstract partial class LogEntry
        {
            private string HHmmssfff = "HH:mm:ss.fff";

            /// <summary>
            /// Gets or sets the sequential counter for the log entry.
            /// </summary>
            public int Counter { get; set; }

            /// <summary>
            /// The number of itemd still in the Queue.
            /// </summary>
            public int Queued { get; set; }

            /// <summary>
            /// Gets or sets the creation time for the log entry.
            /// </summary>
            public DateTime Now { get; set; }

            /// <summary>
            /// Gets or sets the UTC creation timestamp for the log entry.
            /// </summary>
            public DateTime UtcNow { get; set; }

            ///
            /// <summary>
            /// Gets or sets the severity level of the log message.
            /// </summary>
            public Vi.Logger.Levels Level { get; set; }

            /// <summary>
            /// Gets or sets the line number in the source file where the log method was called.
            /// </summary>
            public int Line { get; set; }

            /// <summary>
            /// Gets or sets the name of the calling member (method, property, etc.).
            /// </summary>
            public string Member { get; set; }

            /// <summary>
            /// Gets or sets the full path of the source file where the log method was called.
            /// </summary>
            public string File { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="LogEntry"/> class.
            /// </summary>
            /// <param name="indentation">The visual indentation level for the log message.</param>
            /// <param name="level">The severity level of the log message.</param>
            /// <param name="counter">The sequential counter for the log entry.</param>
            /// <param name="queued"> The number of items still in the queue at the time of logging.</param>
            /// <param name="line">The line number in the source file (auto-populated by CallerLineNumber).</param>
            /// <param name="member">The name of the calling member (auto-populated by CallerMemberName).</param>
            /// <param name="file">The full path of the source file (auto-populated by CallerFilePath).</param>
            public LogEntry(byte indentation, Vi.Logger.Levels level, int counter, int queued, [CallerLineNumber] int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?")
            {
                this.Counter = counter;
                this.Queued = queued;
                this.Now = DateTime.Now;
                this.UtcNow = DateTime.UtcNow;
                this.Level = level;
                this.Line = line;
                this.Member = member;
                this.File = file;
            }


            /// <summary>
            /// Returns a string representation of the log data, formatted with default indentation.
            /// </summary>
            /// <param name="count">Should be the number of log entries still in the Queue.</param>
            /// <returns>A formatted string representing the log entry's common data.</returns>
            public virtual string ToReport(uint count)
            {
                return this.ToReport(indentation: 0, count);
            }

            /// <summary>
            /// Returns a string representation of the log data, formatted with a specified indentation level.
            /// </summary>
            /// <param name="indentation">The desired indentation level for this specific log entry.</param>
            /// <param name="count"> The number of log entries still in the queue at the time of logging.</param>
            /// <returns>A formatted string containing common log data fields, suitable for prefixing a message.</returns>
            /// <remarks>
            /// This method formats the common log attributes (level, timestamps, counter, call info)
            /// into a comma-separated string. Derived classes are expected to append their specific
            /// message content to this string. Commas are used as separators as the base information
            /// is guaranteed not to contain them, ensuring safe parsing of the standard fields.
            /// </remarks>
            public string ToReport(byte indentation, uint count)
            {
                string level = this.Level.ToString().Substring(0, 2);
                string indent = string.Empty.PadLeft(indentation * 3);

                string hhmmssfff0 = this.UtcNow.ToString(this.HHmmssfff).PadLeft(12);
                string hhmmssfff1 = this.Now.ToString(this.HHmmssfff).PadLeft(12);

                double elapsed = (double)(System.DateTime.UtcNow - this.UtcNow).TotalMilliseconds;

                var report = $"{level},{indent}{hhmmssfff0},{hhmmssfff1},{elapsed.ToText()},{this.Counter},{this.Queued},{count},{Line},{Member},{File}";

                return report;
            }
        }
    }

}