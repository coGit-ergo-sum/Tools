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

            /// <summary>
            /// A null-terminated string that contains the default directory used when launching the initial program.
            /// </summary>
            WTSWorkingDirectory,

            /// <summary>
            /// This value is not used.
            /// </summary>
            WTSOEMId,

            /// <summary>
            /// A ULONG value that contains the session identifier.
            /// </summary>
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

            /// <summary>
            /// A null-terminated string that contains the name of the domain to which the logged-on user belongs.
            /// </summary>
            WTSDomainName,

            /// <summary>
            /// The session's current connection state. For more information, see
            /// </summary>
            WTSConnectState,

            /// <summary>
            /// A ULONG value that contains the build number of the client.
            /// </summary>
            WTSClientBuildNumber,

            /// <summary>
            /// A null-terminated string that contains the name of the client.
            /// </summary>
            WTSClientName,

            /// <summary>
            /// A null-terminated string that contains the directory in which the client is installed.
            /// </summary>
            WTSClientDirectory,

            /// <summary>
            /// A USHORT client-specific product identifier.
            /// </summary>
            WTSClientProductId,

            /// <summary>
            /// A ULONG value that contains a client-specific hardware identifier. This option is reserved for future use.
            /// </summary>
            WTSClientHardwareId,

            /// <summary>
            /// The network type and network address of the client. For more information, see WTS_CLIENT_ADDRESS.
            /// The IP address is offset by two bytes from the start of the Address member of the WTS_CLIENT_ADDRESS structure.
            /// </summary>
            WTSClientAddress,

            /// <summary>
            /// Information about the display resolution of the client. For more information, see
            /// </summary>
            WTSClientDisplay,

            /// <summary>
            /// A USHORT value that specifies information about the protocol type for the session. This is one of the following values.
            /// can be: {0: The console session, 1: This value is retained for legacy purposes, 2: The RDP protocol.}
        /// </summary>
        WTSClientProtocolType,

            /// <summary>
            /// This value returns FALSE. If you call GetLastError to get extended error information, GetLastError returns ERROR_NOT_SUPPORTED.
            /// </summary>
            WTSIdleTime,

            /// <summary>
            /// This value returns FALSE. If you call GetLastError to get extended error information, GetLastError returns ERROR_NOT_SUPPORTED.
            /// </summary>
            WTSLogonTime,

            /// <summary>
            /// This value returns FALSE. If you call GetLastError to get extended error information, GetLastError returns ERROR_NOT_SUPPORTED.
            /// </summary>
            WTSIncomingBytes,

            /// <summary>
            /// This value returns FALSE. If you call GetLastError to get extended error information, GetLastError returns ERROR_NOT_SUPPORTED.
            /// </summary>
            WTSOutgoingBytes,

            /// <summary>
            /// This value returns FALSE. If you call GetLastError to get extended error information, GetLastError returns ERROR_NOT_SUPPORTED.
            /// </summary>
            WTSIncomingFrames,

            /// <summary>
            /// This value returns FALSE. If you call GetLastError to get extended error information, GetLastError returns ERROR_NOT_SUPPORTED.
            /// </summary>
            WTSOutgoingFrames,

            /// <summary>
            /// Information about a Remote Desktop Connection (RDC) client. For more information, see WTSCLIENT.
            /// </summary>
            WTSClientInfo,

            /// <summary>
            /// Information about a client session on a RD Session Host server. For more information, see WTSINFO.
            /// </summary>
            WTSSessionInfo,

            /// <summary>
            /// Extended information about a session on a RD Session Host server. For more information, see WTSINFOEX.
            /// </summary>
            WTSSessionInfoEx,

            /// <summary>
            /// A WTSCONFIGINFO structure that contains information about the configuration of a RD Session Host server.
            /// </summary>
            WTSConfigInfo,

            /// <summary>
            /// This value is not supported.
            /// </summary>
            WTSValidationInfo,

            /// <summary>
            /// A WTS_SESSION_ADDRESS structure that contains the IPv4 address assigned to the session.
            /// If the session does not have a virtual IP address, the WTSQuerySessionInformation function returns ERROR_NOT_SUPPORTED.
            /// </summary>
            WTSSessionAddressV4,

            /// <summary>
            /// Determines whether the current session is a remote session. The WTSQuerySessionInformation function 
            /// returns a value of TRUE to indicate that the current session is a remote session, and FALSE to indicate 
            /// that the current session is a local session. This value can only be used for the local machine, so 
            /// the hServer parameter of the WTSQuerySessionInformation function must contain WTS_CURRENT_SERVER_HANDLE.
            /// </summary>
            WTSIsRemoteSession
        }
    }
}
