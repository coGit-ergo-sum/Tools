using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Vi.API
{
    public static partial class Wtsapi32
    {
        /// <summary>
        /// Contains values that indicate the type of session information to retrieve in a call to the WTSQuerySessionInformation function.
        /// </summary>
        public enum WTS_INFO_CLASS:int
        {
            /// <summary>
            /// A null-terminated string that contains the name of the initial program that Remote Desktop Services runs when the user logs on.
            /// </summary>
            WTSInitialProgram,

            /// <summary>
            /// A null-terminated string that contains the published name of the application that the session is running. 
            /// Windows Server 2008 R2, Windows 7, Windows Server 2008 and Windows Vista:  This value is not supported
            /// </summary>
            WTSApplicationName,
            WTSWorkingDirectory,
            WTSOEMId,
            WTSSessionId,

            /// <summary>
            /// A null-terminated string that contains the name of the user associated with the session.
            /// </summary>
            WTSUserName,

            /// <summary>
            /// A null-terminated string that contains the name of the Remote Desktop Services session.
            /// </summary>
            // Note  Despite its name, specifying this type does not return the window station name. Rather, it returns the
            // name of the Remote Desktop Services session. Each Remote Desktop Services session is associated with an interactive window 
            // station. Because the only supported window station name for an interactive window station is "WinSta0", each session is 
            // associated with its own "WinSta0" window station. 
            // For more information, see Window Stations.
            WTSWinStationName,
            WTSDomainName,
            WTSConnectState,
            WTSClientBuildNumber,
            WTSClientName,
            WTSClientDirectory,
            WTSClientProductId,
            WTSClientHardwareId,
            WTSClientAddress,
            WTSClientDisplay,
            WTSClientProtocolType,
            WTSIdleTime,
            WTSLogonTime,
            WTSIncomingBytes,
            WTSOutgoingBytes,
            WTSIncomingFrames,
            WTSOutgoingFrames,
            WTSClientInfo,
            WTSSessionInfo,
            WTSSessionInfoEx,
            WTSConfigInfo,
            WTSValidationInfo,
            WTSSessionAddressV4,
            WTSIsRemoteSession
        }
    }
}
