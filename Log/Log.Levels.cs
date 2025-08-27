using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vi
{

    /// <summary>
    /// Exposes  methods  to log and format messages: Debug; Info; Warn; Error; Fatal; Format.    
    /// </summary>    
    /// <include file='Logger/XMLs/Logger.xml' path='Docs/type[@name="Logger"]/*' />
    public static partial class Log  
    {
        #region enum Levels
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
            /// The 'Info' level is typically used to output information that is useful to the running and management of your system (production). 'Info' would also be the level used to log Entry and Exit points in key grids of your application. However, you may choose to add more entry and exit points at Debug level for more granularity during development and testing.
            /// </summary>
            INFO,

            /// <summary>
            /// Warning is often used for handled 'exceptions' or other important log events. For example, if your application requires a configuration setting but has a default in case the setting is missing, then the Warning level should be used to log the missing configuration setting.
            /// </summary>
            WARN,

            /// <summary>
            /// Error is used to log all unhandled exceptions. This is typically logged inside a catch block at the boundary of your application.
            /// </summary>
            ERROR,

            /// <summary>
            /// Fatal is reserved for special exceptions/conditions where it is imperative that you can quickly pick out these events. Fatal should to be used early in an application's development. It's usually only with experience it is possible identify situations worthy of the FATAL moniker experience do specific events become worth of promotion to Fatal. After all, an error's an error.
            /// </summary>
            FATAL,

        }
		#endregion
    }

}
