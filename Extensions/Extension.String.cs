using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Markup;
using Vi.Extensions.Object;


//// <include file='help/XMLs/Extensions/Extension.string.xml' path='Docs/namespace[@name="IsNull"]/*' />
namespace Vi.Extensions.String
{
    /// <summary>
    /// Collection of extention methods for 'string'
    /// </summary>
    public static partial class Methods
    {
        /// <summary>
        /// Performs the callback 'each' for each item in the array 'values'.
        /// </summary>
        /// <param name="values">The array of strings</param>
        /// <param name="each">the action to perform over each item in values</param>
        public static void ForEach(this string[] values, Action<string> each)
        {
            foreach (var value in values) {
                each(value);
            }
            ;
        }
        /// <summary>
        /// Like 'Split' this Extension Method splits a string. 
        /// The difference are first applies trim to the parameter value and second 
        /// returns '@default' if the string to split is null
        /// </summary>
        /// <param name="value">The string to split</param>
        /// <param name="default">The returning value in case the splitting string is null.</param>
        /// <param name="separator">The separator. The default is ';'</param>
        /// <returns>value?.Trim(charArray)?.Split(charArray) ?? @default;</returns>//
        public static string[] ToItems(this string value, string[] @default, char separator = ';')
        {
            //var charArray = separator.ToCharArray();
            value = value?.Trim(separator) ?? string.Empty;

            var values = (value.Length > 0) ? value.Split(separator) : @default;
            return values;
        }


        /// <summary>
        /// Splits a string into an array of strings using the specified separator.
        /// </summary>
        /// <param name="value">The string to split</param>
        /// <param name="separator">The string used to split the main string</param>
        /// <returns>an array made of the parts of the string once removed the separator</returns>
        public static string[] ToItems(this string value, char separator)
        {
            var values = value.ToItems(new string[0], separator);
            return values;
        }

        /// <summary>
        /// Splits a string into an array of strings using the specified separator.
        /// (the built-in method 'Split' splits the string using separator one character at the time)
        /// </summary>
        /// <param name="value">The string to split</param>
        /// <param name="separator">The string used to split the main string</param>
        /// <returns>an array made of the parts of the string once removed the separator</returns>
        public static string[] ToItems(this string value, string separator)
        {
            try
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(separator))
                {
                    return new string[] { value };
                }

                if (separator.Length == 1)
                {
                    return value.ToItems(new string[0], separator[0]);
                }

                // remove leading and trailing separators
                value = value.Trim(separator);

                // 1. Efficiently count occurrences of the separator
                int counter = 0;
                int index = 0;
                while ((index = value.IndexOf(separator, index)) != -1)
                {
                    counter++;
                    index += separator.Length;
                }

                // 2. Dimension the resulting array
                int length = counter + 1;
                string[] result = new string[length];

                // 3. Split the string using IndexOf and Substring
                //int currentIndex = 0;
                int startIndex = 0;
                int separatorLength = separator.Length;

                for (int i = 0; i < counter; i++)
                {
                    index = value.IndexOf(separator, startIndex);
                    result[i] = value.Substring(startIndex, index - startIndex);
                    startIndex = index + separatorLength;
                }

                // Add the last part of the string after the last separator
                result[counter] = value.Substring(startIndex);

                return result;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Error in ToItems: {ex.Message}", ex);
            }
        }
        /// <summary>
        /// Like 'Split' this Extension Method splits a string. 
        /// The difference are first applies trim to the parameter value and second 
        /// returns '@default' if the string to split is null
        /// The default value is the empty array (a dimension 0 array, not null)
        /// the Separator is ';'
        /// </summary>
        /// <param name="value">The string to split</param>
        /// <returns>value?.Trim(charArray)?.Split(charArray) ?? @default;</returns>
        public static string[] ToItems(this string value) 
        {
            var values = value.ToItems(new string[0], ';');
            return values;
        }


        /// <summary>
        /// The built-in trim can trim just one character only. This method trims a substring.
        /// </summary>
        /// <param name="value">The string to trim</param>
        /// <param name="subString">The value to remove</param>
        /// <returns></returns>

        public static string Trim(this string value, string subString)
        {
            // if the value or the subString are null or empty return the value
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(subString))
            {
                return value;
            }

            // if the subString is made of one character use the built-in trim  
            if (subString.Length == 1)
            {
                return value.Trim(subString[0]);
            }



            string result = value;

            if (result.StartsWith(subString))
            {
                result = result.Substring(subString.Length);
            }

            if (result.EndsWith(subString))
            {
                result = result.Substring(0, result.Length - subString.Length);
            }

            return result;
        }

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
        /// Converts a string to a Vi.Types.Directory object.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>A Directory object created from the string.</returns>
        public static Vi.Types.Directory ToDirectory(this string value)
        {
            return new Vi.Types.Directory(value);
        }

        /// <summary>
        /// Converts a string to a Vi.Types.File object.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>A File object created from the string.</returns>
        public static Vi.Types.File ToFile(this string value)
        {
            return new Vi.Types.File(value);
        }

        /// <summary>
        /// Converts a string to a Vi.Types.JSON object.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>A JSON object created from the string.</returns>
        // [DebuggerStepThrough]
        public static Vi.Types.JSON ToJSON(this string value)
        {
            return new Vi.Types.JSON(value);
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
                    catch 
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
        //// <include file='help/XMLs/Extensions/Extension.string.xml' path='Docs/method[@name="IsNull"]/*' />
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

        /// <summary>
        /// Formats a string using the specified format and values.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="values">An array of strings to format.</param>
        /// <returns>A formatted string.</returns>
        public static string Format(this string format, params string[] values)
        {
            return string.Format(format, values);
        }

        /// <summary>
        /// Checks if a string is in the format used by excel to denote a cell, with or without the '$' simbol (E.g.: A1, B2, C3, AA1, AB2, AZS342)
        /// (This is the regex's pattern: ^(\$?[A-Z]{1,3}\$?[0-9]{1,4})$
        /// </summary>
        /// <param name="text">The text to check</param>
        /// <returns>True if the text coul be a valid ecel coordinate. False otherwise.</returns>
        public static bool IsExcelCoordinates(this string text)
        {
            // Defines the regex for the excel coordinates
            string pattern = @"^(\$?[A-Z]{1,3}\$?[0-9]{1,4})$"; // @"^[A-Z]{1,3}[0-9]{1,4}$";

            // creates the regex object
            Regex regex = new Regex(pattern);

            // verifies if the text is a valid excel coordinate
            return regex.IsMatch(text);
        }


        #endregion
    }

}


