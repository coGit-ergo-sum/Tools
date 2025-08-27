using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Vi.Extensions.Exception
{
    /// <summary>
    /// Collection of 'extension methods' for Exception
    /// </summary>
	public static partial class Methods
	{
        /// <summary>
        /// Creates a new instance of 'Vi.Types.Error' having the original 'Syste.Exception' assigned to the 'InnerException' property.
        /// </summary>
        /// <param name="se">The original Exception</param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        /// <returns></returns>
        public static Vi.Types.Error ToError(this System.Exception se, int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?")
        {
            return new Vi.Types.Error(se.Message, se, line, member, file);
        }


        /// <summary>
        /// Creates a new instance of 'Vi.Types.Error' having the original 'Syste.Exception' assigned to the 'InnerException' property.
        /// </summary>
        /// <param name="se">The original Exception</param>
        /// <param name="message">A message to show in the field 'Message'</param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        public static Vi.Types.Error ToError(this System.Exception se, string message, int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?")
        {
            return new Vi.Types.Error(message, se, line, member, file);
        }

        /// <summary>
        /// Calls 'System.Diagnostics.Trace.TraceError': Writes an error message to the trace listeners in the System.Diagnostics.Trace.Listeners collection using the specified message.
        /// </summary>
        /// <param name="se">The instance of the exception.</param>
        public static void Trace(this System.Exception se)
        {
            //Vi.Log.Error(se, line, member, file);
            System.Diagnostics.Trace.TraceError(se.Message);
        }

        /// <summary>
        /// Logs the error calling 'Vi.Log.Exception(se);'
        /// There will be two different logs on two different files 
        /// A 'N' (normal) log and an 'E' (Exception) log.
        /// The 'N' log will contain only the message of the exception, 
        /// while the 'E' log will contain a full set of info about the exception.
        /// </summary>
        /// <param name="se">The system.Exception to log.</param>
        public static void Log(this System.Exception se)
        {
            Vi.Logger.Exception(se);
        }

        /// <summary>
        /// Logs the error calling 'Vi.Logger.Write(se);'
        /// </summary>
        /// <param name="se">The instance of the exception.</param>
        /// <param name="line">The line number in the source file where the exception occurred. Default is 0.</param>
        /// <param name="member">The name of the member where the exception occurred. Default is "?"</param>
        /// <param name="file">The full path of the source file where the exception occurred. Default is "?"</param>
        public static void Log(this System.Exception se, [CallerLineNumber] int line = 0, [CallerMemberName] string member = "?", [CallerFilePath] string file = "?")
        {
            //Vi.Log.Error(se);
            Vi.Logger.Write(se, line, member, file);
        }
        /*
        /// <summary>
        /// Visualizza la 'MessageBox' standard di Microsoft già configurata per visualizzare un messaggio d'errore. La 'Caption' è il nome della applicazione in cui gira questa extension.
        /// </summary>
        /// <param name="se">L'exception alla quale aggiungere questo extension method.</param>
        /// <returns>MessageBoxButtons.OK (se viene premuto il tasto 'OK'; MessageBoxButtons.None Altrimenti.</returns>
        public static System.Windows.Forms.DialogResult Show(this System.Exception se)
        {
            //var caption = AppDomain.CurrentDomain.DomainManager.EntryAssembly.GetName().Name;
           // return System.Windows.Forms.MessageBox.Show(se.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error); /// se.Show(caption);
           var caption = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            return se.Show(caption);
        }

        /// <summary>
        /// Visualizza la 'MessageBox' standard di Microsoft già configurata per visualizzare un messaggio d'errore.
        /// </summary>
        /// <param name="se">L'exception alla quale aggiungere questo extension method.</param>
        /// <param name="caption">La caption della message box. (Il titolo.)</param>
        /// <returns>MessageBoxButtons.OK (if 'OK' is clicked; MessageBoxButtons.None othrwise.</returns>
        public static System.Windows.Forms.DialogResult Show(this System.Exception se, string caption)
        {
            try
            {
                return System.Windows.Forms.MessageBox.Show(se.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                return System.Windows.Forms.DialogResult.None;
            }
        }
        */
    }
}
