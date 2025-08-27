using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vi.Extensions.Enums
{
    /// <summary>
    /// Collection of 'extension methods' for Enums
    /// </summary>
    public static class Methods
    {
        /// <summary>
        /// Casts the value 'source' into an integer.
        /// </summary>
        /// <typeparam name="T">Any type derived from 'IConvertible'.</typeparam>
        /// <param name="source">The type we want to convert into aint value..</param>
        /// <param name="default">The int obtained casting value. @default otherwise.</param>
        /// <returns></returns>
        public static int ToInt<T>(this T source, int @default) where T : IConvertible //enum
        {
            //if (!typeof(T).IsEnum)throw new ArgumentException("T must be an enumerated type");
            try
            {
                return (int)(IConvertible)source;
            }
            catch (System.Exception)
            {
                return @default;
            }
        }

        /// <summary>
        /// Counts the number of element contained in any type derived from 'IConvertible'.
        /// </summary>
        /// <typeparam name="T">Any type derived from 'IConvertible'.</typeparam>
        /// <param name="source">The type we want count how many elements it has.</param>
        /// <returns>The number of elements the type has.</returns>
        //ShawnFeatherly funtion (above answer) but as extention method
        public static int Count<T>(this T source) where T : IConvertible //enum
        {
            // if (!typeof(T).IsEnum)throw new ArgumentException("T must be an enumerated type");
            return Enum.GetNames(typeof(T)).Length;
        }

        /// <summary>
        /// Gets the description (from the decorator) of the value.
        /// </summary>
        /// <typeparam name="T">Any type derived from 'IConvertible'.</typeparam>
        /// <param name="value">The value from which we want to get its description (if any).</param>
        /// <returns></returns>
        public static string Description<T>(this T value) where T : IConvertible //enum
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])value
               .GetType()
               .GetField(value.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : "N.D.";
        }

    }

}
