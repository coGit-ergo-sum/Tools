using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vi.Tools.Extensions.Array
{
    /// <summary>
    /// Collection of Utility Extention methods
    /// </summary>
    public static partial class Methods
    {

        /// <summary>
        /// Provides the max index for the array. Max index is (Length - 1) for zero based array.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>value.Length - 1.</returns>
        public static int MaxIndex(this System.Array value)
        {
            return (value.Length - 1);
        }

        /// <summary>
        /// Return the last item of the array
        /// </summary>
        /// <typeparam name="T">The generic type of the array.</typeparam>
        /// <param name="value">The array object.</param>
        /// <returns>value[value.Length - 1];.</returns>
        public static T Last<T>(this T[] value)
        {
            return value[value.Length - 1];
        }

        /// <summary>
        /// Returns the first item of the array.
        /// </summary>
        /// <typeparam name="T">The generic type of the array.</typeparam>
        /// <param name="value">The array object.</param>
        /// <returns>value[0].</returns>
        public static T First<T>(this T[] value)
        {
            return value[0];
        }

        /// <summary>
        /// Returns the first item of the array (Overload for 'int').
        /// </summary>
        /// <param name="value">The array object.</param>
        /// <returns>value[0].</returns>
        public static int First(this int[] value)
        {
            return value.First<int>();
        }

        /// <summary>
        /// Appends an item at the end of the array.
        /// </summary>
        /// <typeparam name="T">The generic type of the array.</typeparam>
        /// <param name="value">The array object.</param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static T[] Append<T>(this T[] value, T item)
        {

            System.Array.Resize(ref value, value.Length + 1);
            value[value.Length - 1] = item;
            return value;
        }


        /// <summary>
        /// Gets the array slice.
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="source">The source array (the array from which 'cut the slice'.</param>
        /// <param name="start">The stating index (inclusive).</param>
        /// <param name="length">The number of item in the new array (the slice).</param>
        /// <returns>A new array made of 'length' elements extracted from 'value' starting at 'start' (included).</returns>
        public static T[] Slice<T>(this T[] source, int start, int length)
        {
            start = Math.Max(start, 0);
            start = Math.Min(start, source.Length);

            length = Math.Max(length, 0);
            length = Math.Min(length, source.Length);

            int iMax = Math.Min(length, source.Length - start);

            T[] result = new T[length];

            for (int i = 0; i < iMax; i++)
            {
                result[i] = source[start + i];
            }
            return result;
        }

        /// <summary>
        /// Gets the ramaining array starting from the index 'Start' (included).
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="source">The source array (the array from which 'cut the slice'.</param>
        /// <param name="start">The stating index (inclusive).</param>
        /// <returns>A new array made of the remaining elements extracted from 'value' starting at 'start' (included).</returns>
        public static T[] Slice<T>(this T[] source, int start)
        {
            int length = source.Length - (start - 1);
            return Slice<T>(source, start, length);
        }



        /// <summary>
        /// Defined for debug/Log pourposes. Joins the array using the provided separator
        /// </summary>
        /// <param name="value">The array to convert to string.</param>
        /// <param name="separator">The separator used to join the array.</param>
        /// <returns>Null if null; empty if length = 0. Otherwise the array {1, 2, 3 4} become the string "1; 2; 3, 4".</returns>
        public static string ToMessage(this System.Array value, string separator)
        {
            string result = "Unknown error.";

            try
            {
                if (value == null)
                {
                    result = "null";
                }
                else if (value.Length == 0)
                {
                    result = "Empty";
                }
                else
                {
                    result = System.String.Join(separator, value);
                }
            }
            catch (System.Exception se)
            {
                result = "Exception: " + se.Message;
            }

            return result;
        }


    }
}
