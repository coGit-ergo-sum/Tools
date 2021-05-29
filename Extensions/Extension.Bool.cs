using System;

namespace Vi.Tools.Extensions.Bool
{
	public static partial class Methods
	{
		/// <summary>
		/// checks the variable and returns one of the two provided parameters.
		/// </summary>
		/// <param name="value">The value to check.</param>
		/// <param name="onTrue">The parameter to return if value is true.</param>
		/// <param name="onFalse">The parameter to return if value is false.</param>
		/// <returns>The parameter 'onTrue' if value is true.  'onFalse' otherwise.</returns>
		public static string ToConditional(this bool value, string onTrue, string onFalse)
		{
			return value ? onTrue : onFalse;
		}

		/// <summary>
		/// checks the variable and returns one of the two provided callback ('Action')
		/// </summary>
		/// <param name="value">The value to check.</param>
		/// <param name="onTrue">The action to be executed if value is true.</param>
		/// <param name="onFalse">The action to be executed if value is false.</param>
		/// <returns></returns>
		public static void ToConditional(this bool value, Action onTrue, Action onFalse)
		{
			if (value) { onTrue(); } else { onFalse(); };
		}
	}
}
