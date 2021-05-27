using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Vi.API.CS
{
    /// <summary>
    /// La versione 'C#' delle rispettive funzione API.
    /// </summary>
    public static partial class Wtsapi32
    {
        // https://docs.microsoft.com/en-us/windows/win32/api/wtsapi32/nf-wtsapi32-wtsquerysessioninformationw
        /// <summary>
        /// Retrieves session information for the specified session on the specified Remote Desktop Session Host (RD Session Host) server. It can be used to query session information on local and remote RD Session Host servers.
        /// </summary>
        /// <param name="hServer">A handle to an RD Session Host server. Specify a handle opened by the WTSOpenServer function, or specify WTS_CURRENT_SERVER_HANDLE to indicate the RD Session Host server on which your application is running.</param>
        /// <param name="sessionId">A Remote Desktop Services session identifier. To indicate the session in which the calling application is running (or the current session) specify WTS_CURRENT_SESSION. Only specify WTS_CURRENT_SESSION when obtaining session information on the local server. If WTS_CURRENT_SESSION is specified when querying session information on a remote server, the returned session information will be inconsistent. Do not use the returned data.</param>
        /// <param name="WTSInfoClass">A value of the WTS_INFO_CLASS enumeration that indicates the type of session information to retrieve in a call to the WTSQuerySessionInformation function.</param>
        /// <returns></returns>
        public static string WTSQuerySessionInformationW(IntPtr hServer, int sessionId, Vi.API.Wtsapi32.WTS_INFO_CLASS WTSInfoClass)
        {
            IntPtr cAnswerBytes;
            IntPtr cAnswerCount;
            var result = Vi.API.Wtsapi32.WTSQuerySessionInformationW(hServer, sessionId, WTSInfoClass, out cAnswerBytes, out cAnswerCount);
            
            if (result)
            {
                var sessionInformation = System.Runtime.InteropServices.Marshal.PtrToStringUni(cAnswerBytes);
                return sessionInformation;
            }
            else
            {
                int lastWin32Error = Marshal.GetLastWin32Error();
                var win32Error = new Win32Exception(lastWin32Error);
                string message = win32Error.Message;
                throw new System.Exception(win32Error.Message);
            }
        }

    }
}
