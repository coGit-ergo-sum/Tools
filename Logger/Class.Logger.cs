using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using static System.Net.Mime.MediaTypeNames;

namespace Viaaaaa
{
    /// <summary>
    /// Exposes  methods  to log and format messages: Debug; Info; Warn; Error; Fatal; Format.    
    /// </summary>    
    /// <include file='Logger/XMLs/Logger.xml' path='Docs/type[@name="Logger"]/*' />
    public static class Logger  // Interface inheritance is not allowed in static class and in this case is not needed: Vi.Shared.ILog
    {
        // This page is only Infrastructural. Is made to present the Log object in the most structured way.
        #region Infrastructure
        #endregion

        /// <summary>
        /// The delegate that is used to log messages.
        /// </summary>
        /// <param name="indentation"></param>
        /// <param name="level">Specifies if the message is a DEBUG; INFO,...</param> 
        /// <param name="text">The text to log.</param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        public delegate void LogNDelegate(int indentation, Vi.Logger.Levels level, string text, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?");

        /// <summary>
        /// The delegate that is used to log exception.
        /// </summary>
        /// <param name="se">The instance of the exception.</param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        public delegate void LogEDelegate(System.Exception se, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?");

        /// <summary>
        /// Event triggered when a log message is written.
        /// </summary>
        public static event Vi.Logger.LogNDelegate OnLogN;

        /// Event triggered when a log message is written.
        /// </summary>
        public static event Vi.Logger.LogEDelegate OnLogE;


        /// <summary>
        /// Enumeration of the possible types of log (method)
        /// </summary>
        public enum Levels : byte
        {
            /// <summary>
            /// This is the most verbose logging level (maximum volume setting). Debug should be out-of-bounds for a production system and used only for development and testing.
            /// </summary>
            DEBUG,

            /// <summary>
            /// The 'Info' level is typically used to output information that is useful to the running and management of your system (production). 'Info' would also be the level used to log Entry and Exit points in key areas of your application. However, you may choose to add more entry and exit points at Debug level for more granularity during development and testing.
            /// </summary>
            INFO,

            /// <summary>
            /// Warning is often used for handled 'exceptions' or other important log events. For example, if your application requires a configuration setting but has a default in case the setting is missing, then the Warning level should be used to log the missing configuration setting.
            /// </summary>
            WARN,

            /// <summary>
            /// Error is used to log all Errors. This is typically logged inside a catch block at the boundary of your application.
            /// </summary>
            ERROR,

            /// <summary>
            /// Exception is used to log all exceptions. This is typically logged inside a catch block at the boundary of your application.
            /// </summary>
            EXCEPTION,

            ///// <summary>
            ///// Fatal is reserved for special exceptions/conditions where it is imperative that you can quickly pick out these events. Fatal should to be used early in an application's development. It's usually only with experience it is possible identify situations worthy of the FATAL moniker experience do specific events become worth of promotion to Fatal. After all, an error's an error.
            ///// </summary>
            //FATAL,

        }

        #region FakeLog
        /// <summary>
        /// This is an "empty" class: all the methods are without implementation. 
        /// </summary>
        /// <include file='Logger/XMLs/FakeLog.xml' path='Docs/type[@name="FakeLog"]/*' />
        public class FakeLog : Vi.ILog
        {
            /// <summary>
            /// This Method is Without implementation
            /// </summary>
            /// <param name="text">The text to log.</param>
            /// <param name="line">The Line number in the file where this method is called.</param>
            /// <param name="member">The name of the member from which the log comes.</param>
            /// <param name="file">The name of the file from where this method is called.</param>
            public void Debug(string text, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?") { }

            /// <summary>
            /// This Method is Without implementation
            /// </summary>
            /// <param name="text">The text to log.</param>
            /// <param name="line">The Line number in the file where this method is called.</param>
            /// <param name="member">The name of the member from which the log comes.</param>
            /// <param name="file">The name of the file from where this method is called.</param>
            public void Info(string text, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?") { }

            /// <summary>
            /// This Method is Without implementation
            /// </summary>
            /// <param name="text">The text to log.</param>
            /// <param name="line">The Line number in the file where this method is called.</param>
            /// <param name="member">The name of the member from which the log comes.</param>
            /// <param name="file">The name of the file from where this method is called.</param>
            public void Warn(string text, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?") { }

            /// <summary>
            /// This Method is Without implementation
            /// </summary>
            /// <param name="se">The exception to log.</param>
            /// <param name="line">The Line number in the file where this method is called.</param>
            /// <param name="member">The name of the member from which the log comes.</param>
            /// <param name="file">The name of the file from where this method is called.</param>
            public void Error(Exception se, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?") { }

            /// <summary>
            /// This Method is Without implementation
            /// </summary>
            /// <param name="text">The text to log.</param>
            /// <param name="line">The Line number in the file where this method is called.</param>
            /// <param name="member">The name of the member from which the log comes.</param>
            /// <param name="file">The name of the file from where this method is called.</param>
            public void Fatal(string text, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?") { }


            /// <summary>
            /// This Method is Without implementation
            /// </summary>
            /// <param name="ve">The text to log.</param>
            /// <param name="line">The Line number in the file where this method is called.</param>
            /// <param name="member">The name of the member from which the log comes.</param>
            /// <param name="file">The name of the file from where this method is called.</param>
            public void Error(Vi.Types.Error ve, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?") { }


            /// <summary>
            /// This Method is Without implementation
            /// </summary>
            /// <param name="se">The text to log.</param>
            /// <param name="line">The Line number in the file where this method is called.</param>
            /// <param name="member">The name of the member from which the log comes.</param>
            /// <param name="file">The name of the file from where this method is called.</param>
            public void Exception(System.Exception se, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?") { }
        } 
        #endregion

        #region FormatClass
        /// <summary>
        /// Defined only for the class 'FormatClass'
        /// </summary>
        /// <param name="text">The text to log.</param>
        /// <param name="line">The Line number in the file where this method is called.</param>
        /// <param name="member">The name of the member from which the log comes.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        public delegate void FormatDelegate(string text, int line, string member, string file);


        /// <summary>
        /// This class is a 'trick' necessary because is not possible have optional parameter 
        /// (file, member and line) and a param array. this are necessary for the 'Format' method.
        /// </summary>
        /// <include file='Logger/XMLs/FormatClass.xml' path='Docs/type[@name="FormatClass"]/*' />
        public class FormatClass
        {
            /// <summary>
            /// The callback used to log the message.
            /// </summary>
            FormatDelegate CallBack = null;

            /// <summary>
            /// The name of the file from where this method is called.
            /// </summary>
            string File { get; set; }

            /// <summary>
            /// The name of the member where this method is called.
            /// </summary>
            string Member { get; set; }

            /// <summary>
            /// The Line of the file where this method is called.
            /// </summary>
            int Line { get; set; }

            #region CTors


            /// <summary>
            /// CTor. Assign the parameter to the inner fields.
            /// </summary>
            /// <param name="callBack">The callback function used to log one of {Debug; Info; Warn; Fatal}</param>
            /// <param name="file">The name of the file from where this method is called.</param>
            /// <param name="member">The name of the member where this method is called.</param>
            /// <param name="line">The Line of the file where this method is called.</param>
            public FormatClass(FormatDelegate callBack, int line, string member, string file)
            {
                this.CallBack = callBack;
                this.File = file;
                this.Member = member;
                this.Line = line;
            }

            #endregion

            /// <summary>
            /// Logs the message formatting the text exactly as 'String.Format'.
            /// </summary>
            /// <param name="format">A composite format string.</param>
            /// <param name="args">An object array that contains zero or more objects to format.</param>
            public void Format(string format, params object[] args)
            {
                this.CallBack(System.String.Format(format, args), this.Line, this.Member, this.File);
            }

        } 
        #endregion

        /// <summary>
        /// If true 'Debug' will not be traced.
        /// </summary>
        public static bool SkipDebug = false;

        // Be private is a MUST for this field.
        // The logger must be assigned only by the method 'SetLogger' and not by any other way.
        // AND MUST NOT BE ACCESSIBLE FROM THE OUTSIDE OF THIS CLASS.
        // This is to avoid misuse like: Logger._Implementation.Debug(...).
        /// <summary>
        /// The object effectively used to execute the log. By default is a 'Fake' log (an empty class that does not writes anything). To assigne a true Log object use the method 'SetLogger'. 
        /// It is not public to avoid misuse like: Logger._Implementation.Debug(...).
        /// </summary>
        private static Vi.ILog _Implementation = new FakeLog();

        /// <summary>
        /// Assign the provided logger to this class (Vi.Logger). By default this class uses a 'fake' logger that does logs nothing.
        /// After this assignment, every log will be managed by the provided class. This class in made with Log4Net in mind, but any other way to log can used.
        /// </summary>
        /// <param name="logger">Any kind of logger that inherits from 'Vi.Shared.ILog'.</param>
        #region SetLogger
        public static void SetImplementation(Vi.ILog Implementation)
        {
            Vi.Logger._Implementation = Implementation ?? new FakeLog();
        } 
        #endregion

        /// <summary>
        /// Gives back the 'Logger' currently used. This method is defined to avoid direct access to this 'logger'
        /// </summary>
        /// <returns>The Logger object currently used.</returns>
        public static Vi.ILog GetImplementation()
        {
            return Logger._Implementation;
        }

        #region Log Methods

        /// <summary>
        /// Writes one of the log method based on level.
        /// </summary>
        /// <param name="text">The text to log.</param>
        /// <param name="level">Specifies which kind of log {Debug; Warn; ...}</param>
        /// <param name="line">The Line number in the file where this method is called.</param>
        /// <param name="member">The name of the member from which the log comes.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        // // [DebuggerStepThrough]
        public static void Write(string text, Levels level, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            Vi.Logger.Write(text, level.ToString(), line, member, file);    
        }

        /// <summary>
        /// Writes a log message with the specified text and level.
        /// </summary>
        /// <param name="text">The text to log.</param>
        /// <param name="level">The log level as a string (e.g., DEBUG, INFO, WARN).</param>
        /// <param name="line">The line number in the file where this method is called.</param>
        /// <param name="member">The name of the member from which the log originates.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        // // [DebuggerStepThrough]
        public static void Write(string text, string level, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            // 'level' must be the 'ToString' of the enum 'Levels'. So it cannot be null 
            // or empty or any other value diffrent from the one of the enum 'Levels'.
            level = ("" + level).Trim().ToUpper();

            switch (level)
            {
                case "":
                    Logger.Warn("Level could not be null or empty", line, member, file);
                    Logger.Warn(text, line, member, file);
                    break;

                case "DEBUG":
                    Logger.Debug(text, line, member, file);
                    break;

                case "FATAL":
                    //Logger.Fatal(text, line, member, file);
                    Logger.Write($" 'level': {level} shouldn't be called.", Vi.Logger.Levels.ERROR, line, member, file);
                    Logger.Write(text, Vi.Logger.Levels.ERROR, line, member, file);
                    break;

                case "INFO":
                    Logger.Info(text, line, member, file);
                    break;

                case "WARN":
                    Logger.Warn(text, line, member, file);
                    break;

                case "ERROR":
                    // Logger.Error(new Vi.Types.Error(text), line, member, file);
                    Logger.Error(text, line, member, file);
                    break;

                case "EXCEPTION":
                    Logger.Exception(new System.Exception(text), line, member, file);
                    break;

                default:
                    Logger.Write($"Value unexpected for 'level': {level}", Vi.Logger.Levels.ERROR, line, member, file);
                    Logger.Write(text, Vi.Logger.Levels.ERROR, line, member, file);
                    break;
            }

            //Logger.on
        }


        /// <summary>
        /// Writes one of the log method based on level.
        /// </summary>
        /// <param name="level">Specifies which kind of log {Debug; Warn; ...}</param>
        /// <param name="line">The Line number in the file where this method is called.</param>
        /// <param name="member">The name of the member from which the log comes.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        /// <returns>An instance of 'FormatClass' with the method 'Format' used to compose the text to log like the 'String.Format'</returns>
        // // [DebuggerStepThrough]
        public static FormatClass Write(Levels level, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            Action<string, int, string, string> write = (_text, _line, _member, _file) =>
            {
                Logger.Write(level, line, member, file);
            };
            return new FormatClass(new FormatDelegate(write), line, member, file);
        }


        /// <summary>
        /// Writes an 'Error' message in the log file.
        /// </summary>
        /// <param name="ve">The exception to log.</param>
        /// <param name="line">The Line number in the file where this method is called.</param>
        /// <param name="member">The name of the member from which the log comes.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        public static void Write(Vi.Types.Error ve, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            //if (!Logger.SkipDebug)
            //{
            if (Vi.Logger._Implementation != null)
            {
                //Vi.Logger._Implementation.Error(ve, line, member, file);
                Vi.Logger.OnLogN?.Invoke(indentation: 0, Levels.ERROR, ve.Message, line, member, file);
            }
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="se">The System.Exception instance</param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        public static void Write(System.Exception se, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            //if (!Logger.SkipDebug)
            //{
            if (Vi.Logger._Implementation != null)
            {
                Vi.Logger._Implementation.Exception(se, line, member, file);
                Vi.Logger.OnLogE?.Invoke(se, line, member, file);
            }
            //}
        }
        #endregion


        #region Debug
        /// <summary>
        /// This is the most verbose logging level (maximum volume setting). Debug should be out-of-bounds for a production system and used only for development and testing.
        /// Logs a 'Debug in the log file if skepDebug (in config file) is false.
        /// </summary>
        /// <param name="indentation">Set the messagge as belonging to a parent 'message'</param>
        /// <param name="text">The text to log.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        /// <param name="member">The name of the member where this method is called.</param>
        /// <param name="line">The Line of the file where this method is called.</param>
        // // [DebuggerStepThrough]
        public static void Debug(int indentation, string text, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            if (!Logger.SkipDebug)
            {
                if (Vi.Logger._Implementation != null)
                {
                    Vi.Logger._Implementation.Debug(text, line, member, file);
                    Vi.Logger.OnLogN?.Invoke(indentation, Levels.DEBUG, text, line, member, file);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">The message to log</param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        public static void Debug(string text, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            Vi.Logger.Debug(0, text, line, member, file);
        }


        /// <summary>
        /// Call this method to reach the Format method 'Debug().Format(...)';
        /// </summary>
        /// <param name="file">The name of the file from where this method is called.</param>
        /// <param name="member">The name of the member where this method is called.</param>
        /// <param name="line">The Line of the file where this method is called.</param>
        /// <returns>An instance of 'FormatClass' with the method 'Format' used to compose the text to log like the 'String.Format'</returns>
        // // [DebuggerStepThrough]
        public static FormatClass Debug([CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            return new FormatClass(Logger.Debug, line, member, file);
        }
        #endregion

        #region Info
        /// <summary>
        /// The 'Info' level is typically used to output information that is useful to the running and management of your system (production). 'Info' would also be the level used to log Entry and Exit points in key areas of your application. However, you may choose to add more entry and exit points at Debug level for more granularity during development and testing.
        /// </summary>
        /// <param name="text">The text to log.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        /// <param name="member">The name of the member where this method is called.</param>
        /// <param name="line">The Line of the file where this method is called.</param>
        /// <include file='Logger/XMLs/Logger.xml' path='Docs/method[@name="Info"]/*' />
        // // [DebuggerStepThrough]
        public static void Info(string text, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            Vi.Logger.Info(0, text, line, member, file);
        }

        /// <summary>
        /// Writes an 'INFO log' message.
        /// </summary>
        /// <param name="indentation">makes the messages hierartically structured.</param>
        /// <param name="text">The text to show</param>
        /// <param name="line">The Line of the file where this method is called.</param>
        /// <param name="member">Used for debug pourposes: the name of the member from which the log comes.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        // // [DebuggerStepThrough]
        public static void Info(int indentation, string text, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            Vi.Logger._Implementation.Info(text, line, member, file);
            Vi.Logger.OnLogN?.Invoke(indentation, Levels.INFO, text, line, member, file);
        }

        /// <summary>
        /// Use this overload to 'Format' a message like the method 'System.String.Format'. The sintax is: Info().Format(string format, params object[] args).
        /// </summary>
        /// <param name="line">The Line of the file where this method is called.</param>
        /// <param name="member">Used for debug pourposes: the name of the member from which the log comes.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        /// <returns>An instance of 'FormatClass' with the method 'Format' used to compose the text to log like the 'String.Format'</returns>
        // // [DebuggerStepThrough]
        public static FormatClass Info([CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            return new FormatClass(Logger.Info, line, member, file);
        }
        #endregion

        #region Warn
        /// <summary>
        /// Warning is often used for handled 'exceptions' or other important log events. For example, if your application requires a configuration setting but has a default in case the setting is missing, then the Warning level should be used to log the missing configuration setting.
        /// </summary>
        /// <param name="text">The text to log.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        /// <param name="member">The name of the member where this method is called.</param>
        /// <param name="line">The Line of the file where this method is called.</param>
        // // [DebuggerStepThrough]
        public static void Warn(string text, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            Vi.Logger.Warn(0, text, line, member, file);
        }

        /// <summary>
        /// Warning is often used for handled 'exceptions' or other important log events. For example, if your application requires a configuration setting but has a default in case the setting is missing, then the Warning level should be used to log the missing configuration setting.
        /// </summary>
        /// <param name="indentation">The text indentation</param>
        /// <param name="text">The text to log.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        /// <param name="member">The name of the member where this method is called.</param>
        /// <param name="line">The Line of the file where this method is called.</param>
        // // [DebuggerStepThrough]
        public static void Warn(int indentation, string text, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            Vi.Logger._Implementation.Warn(text, line, member, file);
            Vi.Logger.OnLogN?.Invoke(indentation, Levels.WARN, text, line, member, file);
        }

        /// <summary>
        /// Warning is often used for handled 'exceptions' or other important log events. For example, if your application requires a configuration setting but has a default in case the setting is missing, then the Warning level should be used to log the missing configuration setting.
        /// </summary>
        /// <param name="file">The name of the file from where this method is called.</param>
        /// <param name="member">The name of the member where this method is called.</param>
        /// <param name="line">The Line of the file where this method is called.</param>
        /// <returns>An instance of 'FormatClass' with the method 'Format' used to compose the text to log like the 'String.Format'</returns>
        // // [DebuggerStepThrough]
        public static FormatClass Warn([CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            return new FormatClass(Vi.Logger.Warn, line, member, file);
        }
        #endregion

        #region Error
        /// <summary>
        /// Error is used to log all unhandled exceptions. This is typically logged inside a catch block at the boundary of your application.
        /// </summary>
        /// <param name="ve">The current error instance.</param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        // // [DebuggerStepThrough]
        public static void Error(Vi.Types.Error ve)
        {
            Vi.Logger.Error(indentation: 0, ve, ve.Line, ve.Member, ve.File);
        }

        private static int _line(Vi.Types.Error ve)
        {
            return ve.Line;
        } 

        //////////public static void Error(Vi.Types.Error ve, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        //////////{
        //////////    Vi.Logger.Error(indentation: 0, ve, ve.Line, ve.Member, ve.File);
        //////////}

        /// <summary>
        /// Logs an error message with the specified indentation and error details.
        /// </summary>
        /// <param name="indentation">The indentation level for the log message.</param>
        /// <param name="ve">The error object containing details of the error.</param>
        /// <param name="line">The line number in the file where this method is called.</param>
        /// <param name="member">The name of the member from which the log originates.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        // // [DebuggerStepThrough]
        public static void Error(int indentation, Vi.Types.Error ve, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            Vi.Logger._Implementation.Error(ve, line, member, file);
            Vi.Logger.OnLogN?.Invoke(indentation, Levels.ERROR, ve.Message, line, member, file);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        /// <param name="line">The line number in the file where this method is called.</param>
        /// <param name="member">The name of the member from which the log originates.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        // // [DebuggerStepThrough]
        public static void Error(string message, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            Vi.Logger.Error(indentation: 0, new Types.Error(message), line, member, file);
        }

        /// <summary>
        /// Logs an error message with the specified indentation.
        /// </summary>
        /// <param name="indentation">The indentation level for the log message.</param>
        /// <param name="message">The error message to log.</param>
        /// <param name="line">The line number in the file where this method is called.</param>
        /// <param name="member">The name of the member from which the log originates.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        // // [DebuggerStepThrough]
        public static void Error(int indentation, string message, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            Vi.Logger.Error(indentation, new Types.Error(message), line, member, file);
        }

        #region Exception
        /// <summary>
        /// Error is used to log all unhandled exceptions. This is typically logged inside a catch block at the boundary of your application.
        /// </summary>
        /// <param name="se">The exception to log.</param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        // // [DebuggerStepThrough]
        public static void Exception(System.Exception se, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            Vi.Logger.Exception(indentation: 0, se, line, member, file);
        }

        /// <summary>
        /// Logs an exception with the specified indentation level.
        /// </summary>
        /// <param name="indentation">The indentation level for the log message.</param>
        /// <param name="se">The exception to log.</param>
        /// <param name="line">The line number in the file where this method is called.</param>
        /// <param name="member">The name of the member from which the log originates.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        // // [DebuggerStepThrough]
        public static void Exception(int indentation, System.Exception se, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            Vi.Logger._Implementation.Exception(se, line, member, file);
            Vi.Logger.OnLogE?.Invoke(se, line, member, file);
            Vi.Logger.OnLogN?.Invoke(indentation, Vi.Logger.Levels.EXCEPTION, se.Message, line, member, file);
        }
        #endregion

        #endregion


    }
}



//#region Fatal
/////// <summary>
/////// Fatal is reserved for special exceptions/conditions where it is imperative that you can quickly pick out these events. Fatal should to be used early in an application's development. It's usually only with experience it is possible identify situations worthy of the FATAL moniker experience do specific events become worth of promotion to Fatal. After all, an error's an error.
/////// </summary>
/////// <param name="text">The text to log.</param>
/////// <param name="file">The name of the file from where this method is called.</param>
/////// <param name="member">The name of the member where this method is called.</param>
/////// <param name="line">The Line of the file where this method is called.</param>
////// // [DebuggerStepThrough]
////public static void Fatal(string text, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
////{
////    Vi.Logger.Fatal(indentation: 0, text, line, member, file);
////}

/////// <summary>
/////// Fatal is reserved for special exceptions/conditions where it is imperative that you can quickly pick out these events. 
/////// Fatal should be used early in an application's development. It's usually only with experience it is possible to identify 
/////// situations worthy of the FATAL moniker. After all, an error's an error.
/////// </summary>
/////// <param name="indentation">The indentation level for the log message.</param>
/////// <param name="text">The text to log.</param>
/////// <param name="line">The line number in the file where this method is called.</param>
/////// <param name="member">The name of the member from which the log originates.</param>
/////// <param name="file">The name of the file from where this method is called.</param>
////// // [DebuggerStepThrough]
////public static void Fatal(int indentation, string text, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
////{
////    Vi.Logger._Implementation.Fatal(text, line, member, file);
////    Vi.Logger.OnLogN?.Invoke(indentation, Levels.FATAL, text, line, member, file);
////}

/////// <summary>
/////// Fatal is reserved for special exceptions/conditions where it is imperative that you can quickly pick out these events. Fatal should to be used early in an application's development. It's usually only with experience it is possible identify situations worthy of the FATAL moniker experience do specific events become worth of promotion to Fatal. After all, an error's an error.
/////// </summary>
/////// <param name="file">The name of the file from where this method is called.</param>
/////// <param name="member">The name of the member where this method is called.</param>
/////// <param name="line">The Line of the file where this method is called.</param>
/////// <returns>An instance of 'FormatClass' with the method 'Format' used to compose the text to log like the 'String.Format'</returns>
////// // [DebuggerStepThrough]
////public static FormatClass Fatal([CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
////{
////    return new FormatClass(Logger.Fatal, line, member, file);
////}
//#endregion


///////////////// <summary>
///////////////// Writes the message on the Console.
///////////////// </summary>
///////////////// <param name="level">Specifies which kind of log {Debug; Warn; ...}</param>
///////////////// <param name="text">The text to log.</param>
///////////////// <include file='Logger/XMLs/List4Log.xml' path='Docs/Member[@name="AppendItem"]/*' />
///////////////// <include file='Logger/XMLs/List4Log.xml' path='Docs/Member[@name="AppendItemPublic"]/*' />
//////////////// // [DebuggerStepThrough]
//////////////public static void WriteLine(Vi.Logger.Levels level, string text)
//////////////{
//////////////    Vi.Logger._Implementation.WriteLine(level.ToString(), text);
//////////////}

///////////////// <summary>
///////////////// Very simple 'extention' of the method 'WriteLine'. that sets 
///////////////// different colors for the text based on the level.
///////////////// </summary>
///////////////// <param name="level">The specification of the message level.</param>
///////////////// <param name="text">The text of the message.</param>
//////////////// // [DebuggerStepThrough]
//////////////public static void WriteLine(string level, string text)
//////////////{
//////////////    level = ("" + level).Trim().ToUpper();
//////////////    var backgroundColor = System.Console.BackgroundColor;
//////////////    switch (level)
//////////////    {
//////////////        case "DEBUG":
//////////////            System.Console.BackgroundColor = ConsoleColor.Black;
//////////////            System.Console.ForegroundColor = ConsoleColor.White;
//////////////            break;
//////////////        case "INFO":
//////////////            System.Console.BackgroundColor = ConsoleColor.Black;
//////////////            System.Console.ForegroundColor = ConsoleColor.Green;
//////////////            break;
//////////////        case "WARN":
//////////////            System.Console.BackgroundColor = ConsoleColor.Black;
//////////////            System.Console.ForegroundColor = ConsoleColor.Yellow;
//////////////            break;

//////////////        case "ERROR":
//////////////            System.Console.BackgroundColor = ConsoleColor.Black;
//////////////            System.Console.ForegroundColor = ConsoleColor.Red;
//////////////            break;

//////////////        case "EXCEPTION":
//////////////            System.Console.BackgroundColor = ConsoleColor.Red;
//////////////            System.Console.ForegroundColor = ConsoleColor.White;
//////////////            break;

//////////////        case "FATAL":
//////////////            System.Console.BackgroundColor = ConsoleColor.Red;
//////////////            System.Console.ForegroundColor = ConsoleColor.Green;
//////////////            break;

//////////////        case "":
//////////////            Vi.Logger.WriteLine(Vi.Logger.Levels.WARN, $"The provided level is the empty string.");
//////////////            break;

//////////////        default:
//////////////            Vi.Logger.WriteLine(Vi.Logger.Levels.WARN, $"The provided level does not exists: {level}");
//////////////            break;
//////////////    }

//////////////    System.Console.WriteLine(text);
//////////////    System.Console.BackgroundColor = backgroundColor;
//////////////}
