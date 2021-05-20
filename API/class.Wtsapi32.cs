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
