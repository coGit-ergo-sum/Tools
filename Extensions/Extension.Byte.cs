using System;

namespace Vi.Extensions.Byte
{
    /// <summary>
    /// Collection of 'extension methods' for byte
    /// </summary>
    public static partial class Methods
	{
        /// <summary>
        /// Check if the number is even (divisible by 2).
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>Returns '(value AND 1) == 0'.</returns>
        public static bool IsEven(this byte value)
        {
            return (value & 1) == 0;
        }

        /// <summary>
        /// Check if the number is Odd (NOT divisible by 2).
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>Returns '(value AND 1) == 1'.</returns>
        public static bool IsOdd(this byte value)
        {
            return (value & 1) == 1;
        }

        /// <summary>
        /// Checks if a number is between min and max (included)
        /// </summary>
        /// <param name="value">The number to check.</param>
        /// <param name="min">Minimum value allowed.</param>
        /// <param name="max">Maximun value allowed.</param>
        /// <returns>((value &gt;= min) AND (value &lt;= max)</returns>
        public static bool IsBetween(this byte value, byte min, byte max)
        {
            return (value >= min) && (value <= max);
        }

        /// <summary>
        /// Forces the value between 'min and 'max' (included)
        /// </summary>
        /// <param name="value">The number to limit.</param>
        /// <param name="min">Minimum value allowed.</param>
        /// <param name="max">Maximun value allowed.</param>
        /// <returns>Math.Min(Math.Max(value, min), max);</returns>
        public static byte Between(this byte value, byte min, byte max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

    }
}
