using System;
using System.Collections;
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
        /// Represents a log entry specifically for exceptions, encapsulating exception details and formatting logic.
        /// </summary>
        private partial class LogEntryE : Vi.Statics.Tiger.LogEntry
        {
            /// <summary>
            /// Gets or sets the <see cref="System.Exception"/> object associated with this log entry.
            /// </summary>
            public System.Exception SE { get; set; }

            public Vi.Types.HResult HResult { get; set; }

            /// <summary>
            /// Serializes an Exception object into a custom key-value string format for high-performance logging.
            /// </summary>
            /// <param name="count">This variable is intended to record the number of queued items at the
            /// moment of writing (there should be anotter record of the number of queued items at the moment the item  is added to the queue. Both are used for statistical pourposes.</param>
            /// <returns>A string representing the exception's data in the custom format.</returns>
            public override string ToReport(uint count)
            {
                return this.ToReport(count, this.SE); 
            }

            /// <summary>
            /// Converts an <see cref="System.Exception"/> object into a detailed, formatted string report.
            /// This method is intended for generating comprehensive log entries for exceptions.
            /// </summary>
            /// <param name="se">The <see cref="System.Exception"/> object to report. This can be <see langword="null"/>,
            /// in which case the method will generate a report indicating no exception was provided.</param>
            /// <param name="count">The number of items currently in the queue at the moment this report is generated.
            /// This value, along with the count at the time the item was added to the queue,
            /// is used for statistical purposes (e.g., latency analysis).</param>
            /// <returns>A multi-line string containing a structured report of the exception,
            /// including its type, message, stack trace, and any associated data.</returns>
            private string ToReport(uint count, System.Exception se)
            {
                if (se == null) { return "Exception is Null"; }
                var type = se.GetType()?.FullName ?? "Exception type is unknown.";
                var stackTrace = se.StackTrace ?? "No stack trace available.";
                var source = se.Source ?? "No source available.";
                var targetSite = se.TargetSite != null ? $"{se.TargetSite.DeclaringType?.FullName}.{se.TargetSite.Name}" : "No target site available.";
                var helpLink = se.HelpLink ?? "No help link available.";

                var sb = new StringBuilder();
                sb.AppendLine(base.ToReport(count));
                sb.AppendLine($"message: {se.Message}");
                sb.AppendLine($"HResult: {this.HResult.Description}");
                sb.AppendLine($"Exception Type: {type}");
                sb.AppendLine($"StackTrace: {stackTrace}");
                sb.AppendLine($"Source: {source}");
                sb.AppendLine($"TargetSite: {targetSite}");
                sb.AppendLine($"HelpLink: {helpLink}");
                sb.AppendLine($"Data: {LogEntryE.ToReport(se.Data)}");
                sb.AppendLine($"InnerException: {this.ToReport(count, se.InnerException)}");

                return sb.ToString();
            }


            /// <summary>
            /// Initializes a new instance of the <see cref="LogDataE"/> class.
            /// </summary>
            /// <param name="indentation">The visual indentation level.</param>
            /// <param name="counter">The sequential counter for the log entry.</param>
            /// <param name="queued"> The number of items still in the queue at the time of logging.</param>
            /// <param name="se">The <see cref="System.Exception"/> to be logged.</param>
            /// <param name="line">The line number in the source file.</param>
            /// <param name="member">The name of the calling member.</param>
            /// <param name="file">The full path of the source file.</param>
            public LogEntryE(byte indentation, int counter, int queued, System.Exception se, [CallerLineNumber] int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?")
                : base(indentation, Vi.Logger.Levels.EXCEPTION, counter, queued, line, member, file)
            {
                this.SE = se;
                this.HResult = new Vi.Types.HResult(se.HResult);
            }

            /// <summary>
            /// Formats the content of the Exception.Data property into a readable string.
            /// </summary>
            /// <param name="exceptionData">The IDictionary returned by Exception.Data.</param>
            /// <returns>A formatted string containing all key-value pairs, or a message if no data is present.</returns>
            public static string ToReport(IDictionary exceptionData)
            {
                if (exceptionData == null || exceptionData.Count == 0)
                {
                    return "(No additional data in Exception.Data)";
                }

                StringBuilder report = new StringBuilder();
                string indentString = new string(' ', 0);

                report.AppendLine($"{indentString}Exception Data:");

                foreach (System.Collections.DictionaryEntry de in exceptionData)
                {
                    // Handle null values or complex types for better readability
                    string key = de.Key?.ToString() ?? "[NULL Key]";
                    string text;

                    if (de.Value == null)
                    {
                        text = "NULL";
                    }
                    else if (de.Value is string s)
                    {
                        text = $"\"{s}\""; // Enclose strings in quotes
                    }
                    else if (de.Value is IEnumerable enumerable && !(de.Value is string))
                    {
                        // Join collection elements into a single string
                        StringBuilder sb = new StringBuilder();
                        bool first = true;
                        foreach (var item in enumerable)
                        {
                            if (!first) sb.Append(", ");
                            sb.Append(item?.ToString() ?? "NULL");
                            first = false;
                        }

                        text = $"[{sb}]";
                    }
                    else
                    {
                        text = de.Value.ToString() ?? "[Unknown Value]";
                    }

                    report.AppendLine($"{indentString}  - {key}: {text}");
                }

                // Removes the trailing newline if not needed
                return report.ToString().TrimEnd(); 
            }


        }
    }
}
