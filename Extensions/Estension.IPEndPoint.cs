//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CSL.Utilities
//{
//    /// <summary>
//    /// Collection of Utility methods
//    /// </summary>
//    public class IPEndPoint 
//    {
//        /// <summary>
//        /// Parses a couple of strings to get an IPEndpoint (this method is not native in the original 'System.Net.IPEndPoint'
//        /// </summary>
//        /// <param name="ip">The ip address in the format xxx.xxx.xxx.xxx .</param>
//        /// <param name="port">The port number.</param>
//        /// <returns>The IPEndPoint from the ip and port.</returns>
//        public static System.Net.IPEndPoint Parse(string ip, string port)
//        {

//            // -------------------------------------------------------------------- //
//            // Prevents 'Parse' errors.
//            port = port.Replace(" ", System.String.Empty);
//            // -------------------------------------------------------------------- //

//            var portNumber = int.Parse(port);
//            return CSL.Utilities.IPEndPoint.Parse(ip, portNumber);
//        }

//        /// <summary>
//        /// Parses a couple of strings to get an IPEndpoint (this method is not native in the original 'System.Net.IPEndPoint'
//        /// </summary>
//        /// <param name="ip">The ip address in the format xxx.xxx.xxx.xxx .</param>
//        /// <param name="port">The port number.</param>
//        /// <returns>The IPEndPoint from the ip and port.</returns>
//        public static System.Net.IPEndPoint Parse(string ip, int port)
//        {

//            // -------------------------------------------------------------------- //
//            // Prevents 'Parse' errors.
//            ip = ip.Replace(" ", System.String.Empty);
//            // -------------------------------------------------------------------- //

//            var ipAddress = System.Net.IPAddress.Parse(ip);
//            return new System.Net.IPEndPoint(ipAddress, port);
//        }
//    }
//}
