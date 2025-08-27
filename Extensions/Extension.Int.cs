using System;


namespace Vi.Extensions.Int
{

    /// <summary>
    /// Collection of 'extension methods' for Int
    /// </summary>
    public static partial class Methods
    {

        /// <summary>
        /// Check if the number is even (divisible by 2).
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Returns '(value AND 1) == 0'.</returns>
        public static bool IsEven(this int value)
        {
            return (value & 1) == 0;
        }

        /// <summary>
        /// Check if the number is Odd (NOT divisible by 2).
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Returns '(value AND 1) == 1'.</returns>
        public static bool IsOdd(this int value)
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
        public static bool IsBetween(this int value, int min, int max)
        {
            return (value >= min) && (value <= max);
        }

        /// <summary>
        /// Forces the value between 'min and 'max' (included
        /// </summary>
        /// <param name="value">The number to limit.</param>
        /// <param name="min">Minimum value allowed.</param>
        /// <param name="max">Maximun value allowed.</param>
        /// <returns>Math.Min(Math.Max(value, min), max);</returns>
        public static int Between(this int value, int min, int max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

        /// <summary>
        /// Formats a number  following this pattern: '#,##0'
        /// </summary>
        /// <param name="value">The number to format.</param>
        /// <returns>The number formatted.</returns>
        public static string ToText(this int value)
        {
            return value.ToString("#,##0");
        }

        public static Vi.Types.Facility ToFacility(this int value)
        {
            return Vi.Types.Facility.ToFacility(value);
        }


        public static Vi.Types.Severity ToSeverity(this int value)
        {
            return Vi.Types.Severity.ToSeverity(value);
        }


        public static int ToErrorCode(this int value)
        {
            return Vi.Types.HResult.GetCode(value);
        }

        public static Vi.Types.HResult ToHResult(this int value)
        {
            return new Vi.Types.HResult(value);
        }
    }
}
