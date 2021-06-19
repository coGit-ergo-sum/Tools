using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vi.Tools.Extensions.Object;


/// <include file='help/XMLs/Extensions/Extension.string.xml' path='Docs/namespace[@name="IsNull"]/*' />
namespace Vi.Tools.Extensions.String
{
    /// <summary>
    /// Collection of extention methods for 'string'
    /// </summary>
    public static partial class Methods
    {

        #region ToXyz

        /// <summary>
        /// Applies 'int.TryParse'
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="default">The result if 'tryParse' fails.</param>
        /// <returns>The integer associated with the 'value', default otherwise.</returns>
        public static int? ToInt(this string value, int? @default)
        {
            int result = 0;
            var parseOk = int.TryParse(value, out result);
            return parseOk ? result : @default;
        }

        /// <summary>
        /// Converts a string to 'float'
        /// </summary>
        /// <param name="value">the value to convert.</param>
        /// <param name="default">The default value in case conversion fails.</param>
        /// <returns>The float representation of the 'value' (if any). default otherwise.</returns>
        public static float ToFloat(this string value, float @default)
        {
            float result = 0;
            var parseOk = float.TryParse(value, out result);
            return parseOk ? result : @default;
        }

        /// <summary>
        /// Converts a string to 'decimal'
        /// </summary>
        /// <param name="value">the value to convert.</param>
        /// <param name="default">The default value in case conversion fails.</param>
        /// <returns>The float representation of the 'value' (if any). default otherwise.</returns>
        public static decimal ToDecimal(this string value, decimal @default)
        {
            decimal result = 0;
            var parseOk = decimal.TryParse(value, out result);
            return parseOk ? result : @default;
        }

        /// <summary>
        /// Applies 'int.TryParse'
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="default">The result if 'tryParse' fails.</param>
        /// <returns>The int associated with the 'value', default otherwise.</returns>
        public static int ToInt(this string value, int @default)
        {
            value = value.Remove(",", ".");
            int result = 0;
            var parseOk = int.TryParse(value, out result);
            return parseOk ? result : @default;
        }

        /// <summary>
        /// Applies 'byte.TryParse'
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="default">The result if 'tryParse' fails.</param>
        /// <returns>The int associated with the 'value', default otherwise.</returns>
        public static byte ToByte(this string value, byte @default)
        {
            value = value.Remove(",", ".");
            byte result = 0;
            var parseOk = byte.TryParse(value, out result);
            return parseOk ? result : @default;
        }

        /// <summary>
        /// Applies 'long.TryParse'
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="default">The result if 'tryParse' fails.</param>
        /// <returns>The long associated with the 'value', default otherwise.</returns>
        public static long ToLong(this string value, long @default)
        {
            long result = 0;
            var parseOk = long.TryParse(value, out result);
            return parseOk ? result : @default;
        }




        /// <summary>
        /// Applies 'bool.TryParse'
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="default">The result if 'tryParse' fails.</param>
        /// <returns>The boolean associated with the 'value', default otherwise.</returns>
        public static bool ToBool(this string value, bool @default)
        {
            bool result = @default;
            var parseOk = bool.TryParse(value, out result);
            return parseOk ? result : @default;
        }

        /// <summary>
        /// Applies 'Vi.Types.Percentage.TryParse'
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="default">The result if 'tryParse' fails.</param>
        /// <returns>The percentage associated with the 'value', default otherwise.</returns>
        public static Vi.Types.Percentage ToPercentage(this string value, Vi.Types.Percentage @default)
        {
            Vi.Types.Percentage result = 0;
            var parseOk = Vi.Types.Percentage.TryParse(value, out result);
            return parseOk ? result : @default;
        }
        #endregion

        #region toT<T>
        /// <summary>
        /// Applies 'int.TryParse to a 'separator' separated values'.
        /// </summary>
        /// <param name="value">The list of values to convert. Values are separated by the caracter in the parameter 'separator'.</param>
        /// <param name="separator">The characters used as separator.</param>
        /// <param name="default">The result if the parse fails.</param>
        /// <returns>An array of integer associated with the 'value', default otherwise.</returns>
        public static int[] ToInt(this string value, char separator, int[] @default)
        {
            return value.ToT<int>(separator, @default, (v, @d) => v.ToInt(@d));
        }

        /// <summary>
        /// Applies 'int.TryParse to a ';' separated values'.
        /// </summary>
        /// <param name="value">The list of values to convert. Values are separated by the caracter in the parameter 'separator'.</param>
        /// <param name="default">The result if the parse fails.</param>
        /// <returns>An array of integer associated with the 'value', default otherwise.</returns>
        public static int[] ToInt(this string value, int[] @default)
        {
            return value.ToInt(';', @default);
        }

        /// <summary>
        /// Applies 'long.TryParse to a 'separator' separated values'.
        /// </summary>
        /// <param name="value">The list of values to convert. Values are separated by the caracter in the parameter 'separator'.</param>
        /// <param name="separator">The characters used as separator.</param>
        /// <param name="default">The result if the parse fails.</param>
        /// <returns>An array of long associated with the 'value', default otherwise.</returns>
        public static long[] ToLong(this string value, char separator, long[] @default)
        {
            return value.ToT<long>(separator, @default, (v, @d) => v.ToLong(@d));
        }

        /// <summary>
        /// Applies 'long.TryParse to a ';' separated values'.
        /// </summary>
        /// <param name="value">The list of values to convert. Values are separated by the caracter in the parameter 'separator'.</param>
        /// <param name="default">The result if the parse fails.</param>
        /// <returns>An array of long associated with the 'value', default otherwise.</returns>
        public static long[] ToLong(this string value, long[] @default)
        {
            return value.ToLong(';', @default);
        }

        /// <summary>
        /// Applies 'bool.TryParse to a 'separator' separated values'.
        /// </summary>
        /// <param name="value">The list of values to convert. Values are separated by the caracter in the parameter 'separator'.</param>
        /// <param name="separator">The characters used as separator.</param>
        /// <param name="default">The result if the parse fails.</param>
        /// <returns>An array of long associated with the 'value', default otherwise.</returns>
        public static bool[] ToBool(this string value, char separator, bool[] @default)
        {
            return value.ToT<bool>(separator, @default, (v, @d) => v.ToBool(@d));
        }

        /// <summary>
        /// Applies 'bool.TryParse to a ';' separated values'.
        /// </summary>
        /// <param name="value">The list of values to convert. Values are separated by the caracter in the parameter 'separator'.</param>
        /// <param name="default">The result if the parse fails.</param>
        /// <returns>An array of long associated with the 'value', default otherwise.</returns>
        public static bool[] ToBool(this string value, bool[] @default)
        {
            return value.ToBool(';', @default);
        }


        /// <summary>
        /// Applies 'T.ToXyz' to a 'separator' separated values. E.g. if T is bool applies ToBool to each value in the list of values.
        /// ATTENTION: each 'ToXyz' shold never raise an exception. ToT&lt;T&gt; could.
        /// </summary>
        /// <typeparam name="T">The destination type for the parse.</typeparam>
        /// <param name="value">The list of values to convert. Values are separated by the caracter in the parameter 'separator'.</param>
        /// <param name="separator">The characters used as separator.</param>
        /// <param name="default">The result if the parse fails.</param>
        /// <param name="toT">The lambda expression to parse eachitem. E.g. if 'T' is 'bool' then 'toT = (v, @d) => v.ToBool(@d)'.</param>
        /// <returns>An array of 'T' associated with the 'value', default otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">If default is 'null'.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">If default is shorter than ''value.Split(separator).Length'.</exception>
        public static T[] ToT<T>(this string value, char separator, T[] @default, Func<string, T, T> toT)
        {

            if (@default.IsNull())
            {
                // The idea behind this set of extension methods is to resolve the intrinsic uncertainty of the 'TryParse' methods
                // by providing a @default value for a failed parse. (when 'TryParse' returns 'false').
                // That means the parameter 'default' MUST exist: it can be an array of null but cannot be null.
                // In other words: having a default value is the reason (then the constraint) for the existence of the extension methods 'ToXyz'.
                throw new System.ArgumentNullException("@default");
            }
            else if (value.IsNull())
            {
                return @default;
            }
            else
            {
                var values = value.Split(separator);
                if (@default.Length < values.Length)
                {
                    // For the same reason as before the array 'default' cannot be shorter than the array 'values'
                    // (Every value in 'values' MUST have its own default value.)
                    throw new ArgumentOutOfRangeException("@default.Length is shorter than the length of the array 'value.Split(separator)'.");
                }
                else
                {
                    try
                    {
                        var xMax = Math.Min(values.Length, @default.Length);
                        for (int x = 0; x < xMax; x++) @default[x] = toT(values[x], @default[x]);
                        return @default;
                    }
                    catch (System.Exception se)
                    {
                        return @default;
                    }
                }
            }
        }
        #endregion

        #region Is....
        /// <summary>
        /// Checks if the string is Empty
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>'value == String.Empty'</returns>
        public static bool IsEmpty(this string value)
        {
            return value == System.String.Empty;
        }


        /// <summary>
        /// Checks if the string is null.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>The result of this check: (value == null).</returns>
        /// <include file='help/XMLs/Extensions/Extension.string.xml' path='Docs/method[@name="IsNull"]/*' />
        public static bool IsNull(this string value)
        {
            return value == null;
        }

        /// <summary>
        /// Checks if the string is NOT null
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>the return value is !value.isNull</returns>
        public static bool IsNotNull(this string value)
        {
            return !value.IsNull();
        }



        /// <summary>
        /// Check if the string is made of zero or more spaces.
        /// (Usually an input is not valid if made of any number of spaces, included 
        /// the empty string. That's why this function is true also for the empty string.)
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>value.Trim().IsEmpty() (When value is not null). False otherwise.</returns>
        public static bool IsSpaces(this string value)
        {
            return value.IsNotNull() && value.Trim().IsEmpty();
        }

        /// <summary>
        /// Check if the string is 'null', 'empty' or made of spaces.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>value.IsNull() || value.IsSpaces()</returns>
        public static bool IsNullOrSpaces(this string value)
        {
            return value.IsNull() || value.IsSpaces();
        }


        /// <summary>
        /// Checks if the string can be converted to double (the numeric type with the wider range).
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>System.Double.TryParse(value, out _)</returns>
        public static bool IsNumber(this string value)
        {
            return System.Double.TryParse(value, out _);
        }

        /// <summary>
        /// Removes from the string the occourrences of the items in 'oldValues'. Is the same of Replace(oldValues, String.Empty).
        /// </summary>
        /// <param name="value">The string with the substrings to remove. (Runs: Replace(oldValues, String.Empty);)</param>
        /// <param name="oldValues">The list of item to remove from the string.</param>
        /// <returns>The original string purged from the substring in 'oldValues'</returns>
        public static string Remove(this string value, params string[] oldValues)
        {
            return value.Replace(oldValues, global::System.String.Empty);
        }

        /// <summary>
        /// Replaces each value in 'oldValues' with 'newValue'. (if newValue == String.Empty then use Replace(newValues);
        /// </summary>
        /// <param name="value">The string with the substrings to remove.</param>
        /// <param name="oldValues">The list of item to replace from the string.</param>
        /// <param name="newValue">The new value for all the items in 'oldValues'.</param>
        /// <returns></returns>
        public static string Replace(this string value, string[] oldValues, string newValue)
        {
            foreach (string oldValue in oldValues)
            {
                value = value.Replace(oldValue, global::System.String.Empty);
            }
            return value;
        }

        #endregion
    }

}


