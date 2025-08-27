using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vi.API
{
    /// <summary>
    /// Provides access to the Windows Terminal Services API, specifically the WTSAPI32 functions.
    /// This class allows for the retrieval of session information from Remote Desktop Session Host servers.
    /// </summary>
    public static partial class Wtsapi32
    {
        /// <summary>
        /// WTS_CURRENT_SERVER_HANDLE: to indicate the RD Session Host server on which your application is running
        /// </summary>
        public static IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;

        /// <summary>
        /// Retrieves session information for the specified session on the specified Remote Desktop Session Host (RD Session Host) server. 
        /// It can be used to query session information on local and remote RD Session Host servers.
        /// </summary>
        /// <param name="hServer">A handle to an RD (Remote Desktop) Session Host server. Specify a handle opened by the WTSOpenServer function, 
        /// or specify WTS_CURRENT_SERVER_HANDLE to indicate the RD Session Host server on which your application is running.</param>
        /// <param name="SessionId">A Remote Desktop Services session identifier. To indicate the session in which the calling application is 
        /// running (or the current session) specify WTS_CURRENT_SESSION. Only specify WTS_CURRENT_SESSION when obtaining session information on 
        /// the local server. If WTS_CURRENT_SESSION is specified when querying session information on a remote server, the returned session 
        /// information will be inconsistent. Do not use the returned data.</param>
        /// <param name="WTSInfoClass">A value of the WTS_INFO_CLASS enumeration that indicates the type of session information to retrieve in 
        /// a call to the WTSQuerySessionInformation function.</param>
        /// <param name="ppBuffer">A pointer to a variable that receives a pointer to the requested information. The format and contents of the 
        /// data depend on the information class specified in the WTSInfoClass parameter. To free the returned buffer, call the WTSFreeMemory 
        /// function.</param>
        /// <param name="pBytesReturned">A pointer to a variable that receives the size, in bytes, of the data returned in ppBuffer.</param>
        /// <returns>If the function succeeds, the return value is a nonzero value. If the function fails, the return value is zero.
        /// To get extended error information, call GetLastError.</returns>
        // The main problem is the garbage collector. If it runs between the call of SetVolumeLabel and the call of GetLastError then you will  
        // receive the wrong value, because the GC has surely overwritten the last result.
        [System.Runtime.InteropServices.DllImport("Wtsapi32.dll", SetLastError = true)]
        public static extern bool WTSQuerySessionInformationW(IntPtr hServer, int SessionId, Vi.API.Wtsapi32.WTS_INFO_CLASS WTSInfoClass, out IntPtr ppBuffer, out IntPtr pBytesReturned);

    }
}
