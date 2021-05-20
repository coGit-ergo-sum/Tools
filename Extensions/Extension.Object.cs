using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Vi.Tools.Extensions.String;

namespace Vi.Tools.Extensions.Object
{
    public static partial class Methods
    {
        /// <summary>
        /// Checks if an instance of an object is null.
        /// </summary>
        /// <param name="value">The to check.</param>
        /// <returns>Returns value == null;.</returns>
        [DebuggerStepThrough]
        public static bool IsNull(this object value)
        {
            return value == null;
        }

        /// <summary>
        /// Checks if an instance of an object is NOT null.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="default">Makes this method exception resislient.</param>
        /// <returns>Returns value != null;.</returns>
        [DebuggerStepThrough]
        public static bool IsNotNull(this object value)
        {
            return value != null;
        }

        ////////public static bool ToBool(this object value, bool @default)
        ////////{
        ////////    return value == null ? @default : value.ToString().ToBool(@default);
        ////////}


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
