/*
using System;

namespace Vi.Tools.Extensions.Byte
{
    /// <summary>
    /// Collection of 'extension methods' for byte
    /// </summary>
    public static partial class Methods
	{

        /// <summary>
        /// Forces the value between 'min and 'max' (included)
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="value">The value to limit</param>
        /// <param name="min">Minimum value allowed.</param>
        /// <param name="max">Maximun value allowed.</param>
        /// <returns>Math.Min(Math.Max(value, min), max);</returns>
        public static T Between<T>(this T value, T min, T max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

        /// <summary>
        /// Checks if a number is between min and max (included)
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="value">The number to check.</param>
        /// <param name="min">Minimum value allowed.</param>
        /// <param name="max">Maximun value allowed.</param>
        /// <returns>((value &gt;= min) && (value &lt;= max)</returns>
        public static bool IsBetween<T>(this T value, T min, T max) where T: int, Int16, Int32
        {
            return (value >= min) && (value <= max);
        }
    }
}
*/