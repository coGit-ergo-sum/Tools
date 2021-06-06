using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vi.Tools
{

    /// <summary>
    /// Exposes  methods  to log and format messages: Debug; Info; Warn; Error; Fatal; Format.    
    /// </summary>    
    /// <include file='Logger/XMLs/Logger.xml' path='Docs/type[@name="Logger"]/*' />
    public static partial class Log  // Interface inheritance is not allowed in static class and in this case is not needed: Vi.Shared.ILog
    {
        public static bool SkipDebug = false;
        public static bool SkipInfo = false;
        public static bool SkipWarn = false;
        public static bool SkipError = false; 
        public static bool SkipFatal = false;

		#region Events
		public delegate void  WriteDelegate(string text, Vi.Tools.Log.Levels level, int line, string member, string file);
        public static event Log.WriteDelegate Write;
        private static void OnWrite(string text, Levels level) 
        {
            StackTrace stackTrace = new StackTrace();

            // Get calling method name

            var frame = new StackTrace().GetFrame(2);
            var method = frame.GetMethod();
            var line = frame.GetFileLineNumber();
            var file = frame.GetFileName();
            var name = stackTrace.GetFrame(2).GetMethod().Name;
            var fullName = method.DeclaringType.FullName;
            var member = String.Format(@"{0}{1}", fullName, name);
            //var signature = member.GetParameter();
            Type declaringType = method.DeclaringType;
            var module = declaringType.FullName;

            if (Log.Write != null) { Log.Write(text, level, line, member, file); } 
        }


        private static void OnWrite(string text, Levels level, int line, string member, string file)
        {
            if (Vi.Tools.Log.Write != null) { Log.Write(text, level, line, member, file); }
        }


        public delegate void ExceptionDelegate(System.Exception se);
        public static event Log.ExceptionDelegate Exception;

        private static void OnException(System.Exception se)
        {
            if (Vi.Tools.Log.Write != null) { Log.Exception(se); }
        }

        // This page is only Infrastructural. Is made to present the Log object in the most structured way.
        #endregion

        #region Log Methods

        #region Debug
        /// <summary>
        /// This is the most verbose logging level (maximum volume setting). Debug should be out-of-bounds for a production system and 
        /// used only for development and testing.
        /// Logs a 'Debug in the log file if skepDebug (in config file) is false.
        /// </summary>
        /// <param name="text">The text to log.</param>
        [DebuggerStepThrough]
        public static void Debug(string text)
        {
            if (!Vi.Tools.Log.SkipDebug) { Log.OnWrite(text, Vi.Tools.Log.Levels.DEBUG); }
        }

        /// <summary>
        /// This is the most verbose logging level (maximum volume setting). Debug should be out-of-bounds for a production system and 
        /// used only for development and testing.
        /// Logs a 'Debug in the log file if skepDebug (in config file) is false.
        /// </summary>
        /// <param name="format">the message to log in the format used for 'String.Format()'.</param>
        /// <param name="values">The params array like in 'String.Format()'.</param>
        [DebuggerStepThrough]
        public static void Debug(string format, params string[] values)
        {
            Log.Debug(String.Format(format, values)); 
        }
        #endregion

        #region Info
        /// <summary>
        /// The 'Info' level is typically used to output information useful to the running and management of your system (production). 
        /// 'Info' would also be the level used to log Entry and Exit points in key grids of your application. However, you may choose 
        /// to add more entry and exit points at Debug level for more granularity during development and testing.
        /// </summary>
        /// <param name="text">The text to log.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        /// <param name="member">The name of the member where this method is called.</param>
        /// <param name="line">The Line of the file where this method is called.</param>
        /// <include file='Logger/XMLs/Logger.xml' path='Docs/method[@name="Info"]/*' />
        [DebuggerStepThrough]
        public static void Info(string text, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            //var x = System.Runtime.CompilerServices.CallerMemberNameAttribute.GetCustomAttribute();//   .Runtime.CompilerServices.CallerMemberName
            //[CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?"
            if (!Vi.Tools.Log.SkipInfo) { Log.OnWrite(text, Vi.Tools.Log.Levels.INFO, line, member, file); }
        }

        /// <summary>
        /// The 'Info' level is typically used to output information useful to the running and management of your system (production). 
        /// 'Info' would also be the level used to log Entry and Exit points in key grids of your application. However, you may choose 
        /// to add more entry and exit points at Debug level for more granularity during development and testing.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="values"></param>
        [DebuggerStepThrough]
        public static void Info(string format, params string[] values)
        {
            Log.Info(String.Format(format, values));
        }
        #endregion

        #region Warn
        /// <summary>
        /// Warning is often used for handled 'exceptions' or other important log events. For example, if your application requires a 
        /// configuration setting but has a default in case the setting is missing, then the Warning level should be used to log the 
        /// missing configuration setting.
        /// </summary>
        /// <param name="text">The text to log.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        /// <param name="member">The name of the member where this method is called.</param>
        /// <param name="line">The Line of the file where this method is called.</param>
        [DebuggerStepThrough]
        public static void Warn(string text)
        {
            if (!Vi.Tools.Log.SkipWarn) { Log.OnWrite(text, Vi.Tools.Log.Levels.WARN); }
        }


        /// <summary>
        /// Warning is often used for handled 'exceptions' or other important log events. For example, if your application requires a 
        /// configuration setting but has a default in case the setting is missing, then the Warning level should be used to log the
        /// missing configuration setting.
        /// </summary>
        /// <param name="format">The same as the parameter 'format' in 'string.format'.</param>
        /// <param name="values">the param array with the values for th string 'format'/param>
        [DebuggerStepThrough]
        public static void Warn(string format, params string[] values)
        {
            Log.Warn(String.Format(format, values));

            //     Replaces the format item in a specified string with the string representation
            //     of a corresponding object in a specified array.
            //
            // Parameters:
            //   format:
            //     A composite format string.
            //
            //   args:
            //     An object array that contains zero or more objects to format.

        }
        #endregion

        #region Error
        /// <summary>
        /// Error is used to log all unhandled exceptions. This is typically logged inside a catch block at the boundary of your application.
        /// </summary>
        /// <param name="se">The exception to log.</param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        [DebuggerStepThrough]
        public static void Error(System.Exception se)
        {
            if (!Vi.Tools.Log.SkipError) { Log.OnException(se); }
        }


        #endregion

        #region Fatal
        /// <summary>
        /// Fatal is reserved for special exceptions/conditions where it is imperative that you can quickly pick out these events. Fatal should 
        /// to be used early in an application's development. It's usually only with experience it is possible identify situations worthy of the
        /// FATAL moniker experience do specific events become worth of promotion to Fatal. After all, an error's an error.
        /// </summary>
        /// <param name="text">The text to log.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        /// <param name="member">The name of the member where this method is called.</param>
        /// <param name="line">The Line of the file where this method is called.</param>
        [DebuggerStepThrough]
        public static void Fatal(string text)
        {
            if (!Vi.Tools.Log.SkipFatal) { Log.OnWrite(text, Vi.Tools.Log.Levels.FATAL); }
        }


        /// <summary>
        /// Fatal is reserved for special exceptions/conditions where it is imperative that you can quickly pick out these events. Fatal should to 
        /// be used early in an application's development. It's usually only with experience it is possible identify situations worthy of the FATAL
        /// moniker experience do specific events become worth of promotion to Fatal. After all, an error's an error.
        /// </summary>
        /// <param name="format">Like the 'format' parameter for the function string.format().</param>
        /// <param name="values">The values to replace in the placeholder in the parameter 'format'.</param>
        /// <returns>An instance of 'FormatClass' with the method 'Format' used to compose the text to log like the 'String.Format'</returns>
        [DebuggerStepThrough]
        public static void Fatal(string format, params string[] values)
        {
            Log.Fatal(String.Format(format, values));
        }
        #endregion

        #endregion

    }

}
