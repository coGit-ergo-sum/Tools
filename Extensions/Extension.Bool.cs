using System;

namespace Vi.Tools.Extensions.Bool
{
	public static partial class Methods
	{
		/// <summary>
		/// Converts 'DateTime.Ticks' to 'DateTime'.
		/// </summary>
		/// <param name="value">The vaalue to convert.</param>
		/// <param name="default">Makes this method exception resislient.</param>
		/// <returns>The DateTime associated to the value, @default otherwise.</returns>
		public static string ToConditional(this bool value, string onTrue, string onFalse)
		{
			return value ? onTrue : onFalse;
		}
	}
}
