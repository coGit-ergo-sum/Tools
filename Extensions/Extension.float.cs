using System;

namespace Vi.Tools.Extensions.Float
{
	public static partial class Methods
	{

		/// <summary>
		/// Checks if a number is between min and max (included)
		/// </summary>
		/// <param name="value">The number to check.</param>
		/// <param name="min">Minimum value allowed.</param>
		/// <param name="max">Maximun value allowed.</param>
		/// <returns>((value &gt;= min) && (value &lt;= max)</returns>
		public static bool IsBetween(this float value, float min, float max)
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
		public static float Between(this float value, float min, float max)
		{
			return Math.Min(Math.Max(value, min), max);
		}


		public static string ToPercent2(this float value) { return value.ToPercent(2); }
		public static string ToPercent3(this float value) { return value.ToPercent(3); }
		public static string ToPercent(this float value, sbyte decimals)
		{
			string format = "P" + decimals.ToString();
			return value.ToString(format).Replace("%", "");
		}

		public static string ToText(this float value, byte decimals)
		{
			var format = "#,##0" + ((decimals == 0) ? "" : "." + new string('0', decimals));
			return value.ToString(format); 
		}

	}
}
