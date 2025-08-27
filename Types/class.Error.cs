using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vi.Types
{
    /// <summary>
    /// Light version of the Exception should (must) be used each time the flow must be interrupted
    /// Very usefull to break nested control
    /// </summary>
    public class Error : System.Exception
    {


        /// <summary>
        /// the line number where the Error arosen
        /// </summary>
        public readonly int Line;

        /// <summary>
        /// The member from where the Error arosen
        /// </summary>
        public readonly string Member;

        /// <summary>
        /// The file where the Error arosen
        /// </summary>
        public readonly string File;

        /// <summary>
        /// Customer should never be promped with the Exception Message. This is the default message to the customer.
        /// </summary>
        public string CustomMessage = "";

        /// <summary>
        /// Main CTor. Assigns the messages to the base Exception
        /// </summary>
        /// <param name="message">This message will be assigned to the base Exception.Message</param>
        //public Error(string message, int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?"): base(message)
        //{
        //    this.Line = line;
        //    this.Member = member;
        //    this.File = file;
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class with a default message.
        /// </summary>
        public Error() : base("Unexpected error.")
        {
            this.CustomMessage = base.Message;
        }


        /// <summary>
        /// Create a new instance of the Error object with a message for the customer
        /// </summary>
        /// <param name="message">This message will be assigned to the base Exception.Message</param>
        /// <param name="customMessage">The message for the customer.</param>
        public Error(string message, string customMessage, [CallerLineNumber] int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?") : base(message)
        {
            this.Line = line;
            this.Member = member;
            this.File = file;
            this.CustomMessage = customMessage;
        }

        public Error(string message, [CallerLineNumber] int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?") : base(message)
        {
            this.CustomMessage = "Not specified error";
            this.Line = line;
            this.Member = member;
            this.File = file;
        }

        /// <summary>
        /// Create a new instance of 'Error' from a previous instance of System.Exception that is passed to the 'InnerException' parameter.
        /// </summary>
        /// <param name="se"></param>
        /// <param name="line"></param>
        /// <param name="member"></param>
        /// <param name="file"></param>
        public Error(System.Exception se, int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?") : base(se.Message, se)
        {

            this.CustomMessage = "Not specified error";
            this.Line = line;
            this.Member = member;
            this.File = file;
        }

        /// <summary>
        /// Create a new instance of 'Error' from a previous instance of System.Exception that is passed to the 'InnerException' parameter.
        /// </summary>
        /// <param name="message">the message to show for this instance</param>
        /// <param name="se"></param>
        /// <param name="line"></param>
        /// <param name="member"></param>
        /// <param name="file"></param>
        public Error(string message, System.Exception se, int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?") : base(se.Message, se)
        {

            this.CustomMessage = "Not specified error";
            this.Line = line;
            this.Member = member;
            this.File = file;
        }

        /// <summary>
        /// Trows an Error  adding Line, member and file where the request was made;
        /// </summary>
        /// <param name="message"></param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        /// <exception cref="Error"></exception>
        public static void Throw(string message, [CallerLineNumber] int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?")
        {
            throw new Error(message, line, member, file);
        }

        /// <summary>
        /// Throws an error if the condition is true.
        /// </summary>
        /// <param name="condition">The contition to check.</param>
        /// <param name="message"></param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        /// <exception cref="Error">The message to sent to the user.</exception>
        public static void IfTrue(bool condition, string message, [CallerLineNumber] int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?")
        {
            if (condition) { Vi.Types.Error.Throw(message, line, member, file); }
        }

        /// <summary>
        /// Throws an error if and only if the string is the string.Empty.
        /// </summary>
        /// <param name="text">The text to check</param>
        /// <param name="message">The text to send.</param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        /// <exception cref="Error"></exception>
        public static void IfEmpty(string text, string message, [CallerLineNumber] int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?")
        {
            if ((text != null) && (text == string.Empty)) { Vi.Types.Error.Throw(message, line, member, file); }
        }

        /// <summary>
        /// Throws an error if and only if the string is the made of spaces (blanks).
        /// </summary>
        /// <param name="text">The text to check</param>
        /// <param name="message">The text to send.</param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        /// <exception cref="Error"></exception>
        public static void IfSpaces(string text, string message, [CallerLineNumber] int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?")
        {
            if ((text != null) && (text.Length > 0) && (text.Trim() == string.Empty)) { Vi.Types.Error.Throw(message, line, member, file); }
        }

        /// <summary>
        /// Throws an error if the condition is false.
        /// </summary>
        /// <param name="condition">The contition to check.</param>
        /// <param name="message"></param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        /// <exception cref="Error">The message to sent to the user.</exception>
        public static void IfFalse(bool condition, string message, [CallerLineNumber] int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?")
        {
            Vi.Types.Error.IfTrue(!condition, message, line, member, file);
        }


        /// <summary>
        /// Throws an error if the item is null. Uses the method ErrorIfTrue where condition is (item is null).
        /// </summary>
        /// <param name="entity">The entity to check</param>
        /// <param name="message">The message to sent to the user.</param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        public static void IfNull(object entity, string message, [CallerLineNumber] int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?")
        {
            Vi.Types.Error.IfTrue(entity is null, message, line, member, file);
        }

        /// <summary>
        /// Defined for debug pourposes.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Message;
        }

        /// <summary>
        /// Logs the current error instance using the Vi.Logger.
        /// </summary>
        public void Log([CallerLineNumber] int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?")
        {
            Vi.Logger.Error(
                this.Message,
                this.Line,
                this.Member,
                this.File);

            // Old implementation
            //Vi.Logger.Error(
            //    this.Message,
            //    ((line == 0) ? this.Line : line),
            //    ((member == "?") ? this.Member : member),
            //    ((file == "?") ? this.File : file)

        }
    }
}
