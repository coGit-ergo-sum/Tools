using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vi
{
    /// <summary>
    /// Implements some utility methods for the console.
    /// </summary>
    public static class Console
    {
        /// <summary>
        /// Very simple 'extention' of the method 'WriteLine'. that sets 
        /// different colors for the text based on the level.
        /// </summary>
        /// <param name="level">The specification of the message level.</param>
        /// <param name="message">The text of the message.</param>
        // [DebuggerStepThrough]
        public static void WriteLine(string level, string message)
        {
            level = ("" + level).Trim().ToUpper();
            var backgroundColor = System.Console.BackgroundColor;
            switch (level)
            {
                case "DEBUG":
                    System.Console.BackgroundColor = ConsoleColor.Black;
                    System.Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "INFO":
                    System.Console.BackgroundColor = ConsoleColor.Black;
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "WARN":
                    System.Console.BackgroundColor = ConsoleColor.Black;
                    System.Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case "ERROR":
                    System.Console.BackgroundColor = ConsoleColor.Black;
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case "EXCEPTION":
                    System.Console.BackgroundColor = ConsoleColor.Red;
                    System.Console.ForegroundColor = ConsoleColor.White;
                    break;

                case "FATAL":
                    System.Console.BackgroundColor = ConsoleColor.Red;
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    break;

                case "":
                    Vi.Console.WriteLine("WARN", $"The provided level is the empty string.");
                    Vi.Console.WriteLine("WARN", $"The with an empty type was: {message}.");
                    break;

                default:
                    Vi.Console.WriteLine("WARN", $"The provided level is not recognized: {level}");
                    Vi.Console.WriteLine("WARN", $"The with a not recognized type was: {message}.");
                    break;
            }

            System.Console.WriteLine(message);
            System.Console.BackgroundColor = backgroundColor;
        }
    }
}
