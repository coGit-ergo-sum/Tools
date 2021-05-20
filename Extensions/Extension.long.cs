using System;

namespace Vi.Tools.Extensions.Long
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
		public static bool IsBetween(this long value, long min, long max)
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
		public static long Between(this long value, long min, long max)
		{
			return Math.Min(Math.Max(value, min), max);
		}

		/// <summary>
		/// Converts 'DateTime.Ticks' to 'DateTime'.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="default">Makes this method exception resislient.</param>
		/// <returns>The DateTime associated to the value, @default otherwise.</returns>
		public static System.DateTime ToDateTime(this long value, System.DateTime @default)
		{
			try
			{
				return value.IsBetween(System.DateTime.MinValue.Ticks, System.DateTime.MaxValue.Ticks) ? new System.DateTime(value) : @default;
			}
			catch 
			{
				return @default;
			}
		}
	}
}
