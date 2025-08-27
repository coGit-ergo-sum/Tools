using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vi
{
    /// <summary>
    /// Sends the message to the console window
    /// </summary>
    ///  <include file='Logger/XMLs/Console4Log.xml' path='Docs/type[@name="Console4Log"]/*' />
    public static class Console4Log
    {

        /// <summary>
        /// Wraps the provided logger with the class 'Wrapper' 
        /// After this assignment, this object will intercept and send on the screen every message sent to the file.
        /// </summary>
        /// <param name="logger">Any kind of 'logger' that inherits from 'Vi.Shared.ILog'.</param>
        public static void SetLogger(Vi.ILog logger)
        {
            var wrapper = new Wrapper(logger);
            wrapper.OnAppend += new Wrapper.OnAppendHandler(Write);

            Vi.Logger.SetImplementation(wrapper);

        }

        /// <summary>
        /// Writes the message on the Console.
        /// </summary>
        /// <param name="text">The text to log.</param>
        /// <param name="file">The name of the file from where this method is called.</param>
        /// <param name="member">The name of the member where this method is called.</param>
        /// <param name="line">The Line of the file where this method is called.</param>
        /// <param name="level">Specifies which kind of log {Debug; Warn; ...}</param>
        /// <include file='Logger/XMLs/List4Log.xml' path='Docs/Member[@name="AppendItem"]/*' />
        /// <include file='Logger/XMLs/List4Log.xml' path='Docs/Member[@name="AppendItemPublic"]/*' />
        #region AppendItem
        public static void Write(string text, int line, string member, string file, Vi.Logger.Levels level)
        {
            Vi.Console.WriteLine(level.ToString(), text);
            ////////switch (level)
            ////////{
            ////////    case Logger.Levels.DEBUG:
            ////////        System.Console.BackgroundColor = ConsoleColor.Black;
            ////////        System.Console.ForegroundColor = ConsoleColor.White;
            ////////        break;
            ////////    case Logger.Levels.INFO:
            ////////        System.Console.BackgroundColor = ConsoleColor.Black;
            ////////        System.Console.ForegroundColor = ConsoleColor.Green;
            ////////        break;
            ////////    case Logger.Levels.WARN:
            ////////        System.Console.BackgroundColor = ConsoleColor.Black;
            ////////        System.Console.ForegroundColor = ConsoleColor.Yellow;
            ////////        break;

            ////////    case Logger.Levels.ERROR:
            ////////        System.Console.BackgroundColor = ConsoleColor.Black;
            ////////        System.Console.ForegroundColor = ConsoleColor.Red;
            ////////        break;

            ////////    case Logger.Levels.EXCEPTION:
            ////////        System.Console.BackgroundColor = ConsoleColor.Red;
            ////////        System.Console.ForegroundColor = ConsoleColor.White;
            ////////        break;

            ////////    case Logger.Levels.FATAL:
            ////////        System.Console.BackgroundColor = ConsoleColor.Red;
            ////////        System.Console.ForegroundColor = ConsoleColor.Green;
            ////////        break;
            ////////}

            ////////System.Console.WriteLine(text);
            ////////System.Console.ResetColor();
        } 
        #endregion
 
    }
}
