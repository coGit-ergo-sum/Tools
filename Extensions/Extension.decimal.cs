using System;

namespace Vi.Tools.Extensions.Decimal
{

	/// <summary>
	/// Collection of 'extension methods' for decimal
	/// </summary>
	public static partial class Methods
	{

		/// <summary>
		/// Checks if a number is between min and max (included)
		/// </summary>
		/// <param name="value">The number to check.</param>
		/// <param name="min">Minimum value allowed.</param>
		/// <param name="max">Maximun value allowed.</param>
		/// <returns>((value &gt;= min) && (value &lt;= max)</returns>
		public static bool IsBetween(this decimal value, decimal min, decimal max)
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
		public static decimal Between(this decimal value, decimal min, decimal max)
		{
			return Math.Min(Math.Max(value, min), max);
		}


		/// <summary>
		/// Converts a decimal (Percentage) in a string with a fixed number of decimals.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="decimals">The number of decimals.</param>
		/// <returns>Text representation of a Percentage with fixed number of decimal figures.</returns>
		public static string ToPercent(this decimal value, sbyte decimals)
		{
			string format = "P" + decimals.ToString();
			return value.ToString(format).Replace("%", "");
		}

		/// <summary>
		/// Converts a decimal in a string with a fixed number of decimals.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="decimals">The number of decimals.</param>
		/// <returns>text representation of a decimal with fixed number of decimal figures.</returns>
		public static string ToText(this decimal value, byte decimals)
		{
			var format = "#,##0" + ((decimals == 0) ? "" : "." + new string('0', decimals));
			return value.ToString(format); 
		}

	}
}
