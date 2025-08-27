using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vi.Statics
{

    public static partial class Tiger
    {
        /// <summary>
        /// Represents a normal log entry with a message.
        /// </summary>
        private partial class LogEntryN : Vi.Statics.Tiger.LogEntry
        {
            /// <summary>
            /// Gets or sets the primary message content of the log entry.
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// Initializes a new instance of the <see cref="LogEntryE"/> class.
            /// </summary>
            /// <param name="indentation">The visual indentation level.</param>
            /// <param name="level">The severity level of the message.</param>
            /// <param name="counter">The sequential counter for the log entry.</param>
            /// <param name="queued"> The number of items still in the queue at the time of logging.</param>
            /// <param name="message">The text message for the log entry.</param>
            /// <param name="line">The line number in the source file.</param>
            /// <param name="member">The name of the calling member.</param>
            /// <param name="file">The full path of the source file.</param>
            public LogEntryN(byte indentation, Vi.Logger.Levels level, int counter, int queued, string message, [CallerLineNumber] int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?")
                : base(indentation, level, counter, queued, line, member, file)
            {
                this.Message = message;
            }

            /// <summary>
            /// Returns a formatted string representation of the normal log entry, including the base log data and the specific message.
            /// </summary>
            /// <param name="count"> The number of log entries still in the queue at the time of logging.</param>
            /// <returns>A complete, comma-separated string for the normal log entry.</returns>
            public override string ToReport(uint count)
            {
                return $"{base.ToReport(count)},{this.Message}";
            }
        }
    }
}