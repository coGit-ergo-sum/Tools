using System;

namespace Vi.Extensions.Bool
{
	/// <summary>
	/// Collection of 'extension methods' for boolean
	/// </summary>
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
		/// Return "Y" or "N";
		/// </summary>
		/// <param name="value">The value to check.</param>
		/// <returns>'Y' on true. 'N' otherwise.</returns>
		public static string ToYN(this bool value)
		{
			return value.ToConditional("Y", "N");
		}

		/// <summary>
		/// Return "Yes" or "No";
		/// </summary>
		/// <param name="value">The value to check.</param>
		/// <returns>'Yes' on true. 'No' otherwise.</returns>
		public static string ToYesNo(this bool value)
		{
			return value.ToConditional("Yes", "No");
		}

		/// <summary>
		/// Return "T" or "F";
		/// </summary>
		/// <param name="value">The value to check.</param>
		/// <returns>'T' on true. 'F' otherwise.</returns>
		public static string ToTF(this bool value)
		{
			return value.ToConditional("T", "F");
		}

		/// <summary>
		/// Return the string: "True" or "False";
		/// </summary>
		/// <param name="value">The value to check.</param>
		/// <returns>'True' on true. 'False' otherwise.</returns>
		public static string ToTrueFalse(this bool value)
		{
			return value.ToConditional("True", "False");
		}

		/// <summary>
		/// checks the variable and executes one of the two provided callback ('Action')
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
