using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Vi.Types
{
    
    /// <summary>
    /// Provides utility methods for common operations.
    /// </summary>
    public class Utilities
    {
        /// <summary>
        /// Computes the SHA-256 checksum of the provided string.
        /// </summary>
        /// <param name="data">The input string to compute the checksum for.</param>
        /// <returns>A hexadecimal string representing the computed checksum.</returns>
        public static string ComputeChecksum(string data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Converti in formato esadecimale
                }
                return builder.ToString();
            }
        }
    }
}
