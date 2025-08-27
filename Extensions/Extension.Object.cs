using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Vi.Extensions.String;

namespace Vi.Extensions.Object
{
    /// <summary>
    /// Collection of 'extension methods' for Int
    /// </summary>
    public static partial class Methods
    {
        /// <summary>
        /// Checks if an instance of an object is null.
        /// </summary>
        /// <param name="value">The to check.</param>
        /// <returns>Returns value == null;.</returns>
        // [DebuggerStepThrough]
        public static bool IsNull(this object value)
        {
            return value == null;
        }

        /// <summary>
        /// Checks if an instance of an object is NOT null.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>Returns value != null;.</returns>
        // [DebuggerStepThrough]
        public static bool IsNotNull(this object value)
        {
            return value != null;
        }

        /// <summary>
        /// Converts an object to a byte array using binary serialization.
        /// </summary>
        /// <param name="value">The object to convert to a byte array.</param>
        /// <returns>A byte array representation of the object, or null if the object is null.</returns>
        public static byte[] ToBytes(this object value)
        {
            byte[] result = null;
            if (value.IsNotNull())
            {
                var bf = new BinaryFormatter();
                using (var ms = new System.IO.MemoryStream())
                {
                    bf.Serialize(ms, value);
                    result = ms.ToArray();
                }
            }
            return result;
        }

        /// <summary>
        /// Deserializes a byte array into an object of type T.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to. Must be a reference type.</typeparam>
        /// <param name="bytes">The byte array to deserialize.</param>
        /// <returns>The deserialized object of type T, or null if the byte array is null.</returns>
        public static T Deserialize<T>(this byte[] bytes) where T : class
        {
            T result = null;
            if (bytes != null)
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    var bf = new BinaryFormatter();
                    ms.Write(bytes, 0, bytes.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    var obj = (T)bf.Deserialize(ms);
                    return obj;
                }
            }
            return result;
        }
    }
}
