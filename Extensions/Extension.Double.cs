using System;

namespace Vi.Extensions.Double
{

	/// <summary>
	/// Collection of 'extension methods' for decimal
	/// </summary>
	public static partial class Methods
	{

		/// <summary>
		/// Converts a decimal in a string with a fixed number of decimals.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>text representation of a decimal with fixed number of decimal figures.</returns>
        public static string ToText(this double value)
        {
            return value.ToString("#,##0");
        }

    }
}
