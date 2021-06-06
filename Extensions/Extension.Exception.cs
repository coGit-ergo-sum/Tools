using System;
using System.Windows.Forms;

namespace Vi.Tools.Extensions.Exception
{
    /// <summary>
    /// Collection of 'extension methods' for Exception
    /// </summary>
	public static partial class Methods
	{
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
        /// Logs the error calling 'Vi.Tools.Log.Error(se);'
        /// </summary>
        /// <param name="se">The system.Exception</param>
        public static void Log(this System.Exception se)
        {
            Vi.Tools.Log.Error(se);
        }

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
    }
}
