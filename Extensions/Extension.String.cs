using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vi.Tools.Extensions.Object;

namespace Vi.Tools.Extensions.String
{
    public static partial class Methods
    {

        /// <summary>
        /// Like the 'IN' clausole in SQL. 
        /// </summary>
        /// <param name="value">The string to compare.</param>
        /// <param name="values">The reference array.</param>
        /// <returns>True if 'value' exists in 'values'. False otherwise.</returns>
        public static bool In(this string value, params string[] values)
        {
            return values.Any(v => v.Equals(value));
        }


        #region ToXyz

        /// <summary>
        /// Applies 'int.TryParse'
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="default">The result if 'tryParse' fails.</param>
        /// <returns>The integer associated with the 'value', @default otherwise.</returns>
        public static int? ToInt(this string value, int? @default)
        {
            int result = 0;
            var parseOk = int.TryParse(value, out result);
            return parseOk ? result : @default;
        }

        public static float ToFloat(this string value, float @default)
        {
            float result = 0;
            var parseOk = float.TryParse(value, out result);
            return parseOk ? result : @default;
        }

        /// <summary>
        /// Applies 'int.TryParse'
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="default">The result if 'tryParse' fails.</param>
        /// <returns>The integer associated with the 'value', @default otherwise.</returns>
        public static int ToInt(this string value, int @default)
        {
            value = value.Remove(",", ".");
            int result = 0;
            var parseOk = int.TryParse(value, out result);
            return parseOk ? result : @default;
        }

        /// <summary>
        /// Applies 'long.TryParse'
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="default">The result if 'tryParse' fails.</param>
        /// <returns>The long associated with the 'value', @default otherwise.</returns>
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
        /// <returns>The boolean associated with the 'value', @default otherwise.</returns>
        public static bool ToBool(this string value, bool @default)
        {
            bool result = @default;
            var parseOk = bool.TryParse(value, out result);
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
        /// <returns>An array of integer associated with the 'value', @default otherwise.</returns>
        public static int[] ToInt(this string value, char separator, int[] @default)
        {
            return value.ToT<int>(separator, @default, (v, @d) => v.ToInt(@d));
        }

        /// <summary>
        /// Applies 'int.TryParse to a ';' separated values'.
        /// </summary>
        /// <param name="value">The list of values to convert. Values are separated by the caracter in the parameter 'separator'.</param>
        /// <param name="default">The result if the parse fails.</param>
        /// <returns>An array of integer associated with the 'value', @default otherwise.</returns>
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
        /// <returns>An array of long associated with the 'value', @default otherwise.</returns>
        public static long[] ToLong(this string value, char separator, long[] @default)
        {
            return value.ToT<long>(separator, @default, (v, @d) => v.ToLong(@d));
        }

        /// <summary>
        /// Applies 'long.TryParse to a ';' separated values'.
        /// </summary>
        /// <param name="value">The list of values to convert. Values are separated by the caracter in the parameter 'separator'.</param>
        /// <param name="separator">The characters used as separator.</param>
        /// <param name="default">The result if the parse fails.</param>
        /// <returns>An array of long associated with the 'value', @default otherwise.</returns>
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
        /// <returns>An array of long associated with the 'value', @default otherwise.</returns>
        public static bool[] ToBool(this string value, char separator, bool[] @default)
        {
            return value.ToT<bool>(separator, @default, (v, @d) => v.ToBool(@d));
        }

        /// <summary>
        /// Applies 'bool.TryParse to a ';' separated values'.
        /// </summary>
        /// <param name="value">The list of values to convert. Values are separated by the caracter in the parameter 'separator'.</param>
        /// <param name="separator">The characters used as separator.</param>
        /// <param name="default">The result if the parse fails.</param>
        /// <returns>An array of long associated with the 'value', @default otherwise.</returns>
        public static bool[] ToBool(this string value, bool[] @default)
        {
            return value.ToBool(';', @default);
        }


        /// <summary>
        /// Applies 'T.ToXyz' to a 'separator' separated values. E.g. if T is bool applies ToBool to each value in the list of values.
        /// ATTENTION: each 'ToXyz' shold never raise an exception. ToT<T> could.
        /// </summary>
        /// <typeparam name="T">The destination type for the parse.</typeparam>
        /// <param name="value">The list of values to convert. Values are separated by the caracter in the parameter 'separator'.</param>
        /// <param name="separator">The characters used as separator.</param>
        /// <param name="default">The result if the parse fails.</param>
        /// <param name="toT">The lambda expression to parse eachitem. E.g. if 'T' is 'bool' then 'toT = (v, @d) => v.ToBool(@d)'.</param>
        /// <returns>An array of 'T' associated with the 'value', @default otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">If @default is 'null'.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">If @default is shorter than ''value.Split(separator).Length'.</exception>
        public static T[] ToT<T>(this string value, char separator, T[] @default, Func<string, T, T> toT)
        {

            if (@default.IsNull())
            {
                // The idea behind this set of extension methods is to resolve the intrinsic uncertainty of the 'TryParse' methods
                // by providing a @default value for a failed parse. (when 'TryParse' returns 'false').
                // That means the parameter '@default' MUST exist: it can be an array of null but cannot be null.
                // In other words: having a @default value is the reason (then the constraint) for the existence of the extension methods 'ToXyz'.
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
                    // For the same reason as before the array '@default' cannot be shorter than the array 'values'
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
        /// <param name="value"></param>
        /// <returns>'value == String.Empty'</returns>
        public static bool IsEmpty(this string value)
        {
            return value == System.String.Empty;
        }


        /// <summary>
        /// Checks if the string is null
        /// </summary>
        /// <param name="value"></param>
        /// <returns>'value == String.Empty'</returns>
        public static bool IsNull(this string value)
        {
            return value == null;
        }

        /// <summary>
        /// Checks if the string is NOT null
        /// </summary>
        /// <param name="value"></param>
        /// <returns>'value == String.Empty'</returns>
        public static bool IsNotNull(this string value)
        {
            return !value.IsNull();
        }



        /// <summary>
        /// Check if the string is made of spaces.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>value.Trim().IsEmpty()</returns>
        public static bool IsSpaces(this string value)
        {
            return value.Trim().IsEmpty();
        }

        /// <summary>
        /// Check if the string is 'empty' or made of spaces.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>return value.IsEmpty() || value.IsSpaces()</returns>
        public static bool IsEmptyOrSpaces(this string value)
        {
            return value.IsEmpty() || value.IsSpaces();
        }

        /// <summary>
        /// Check if the string is 'null', 'empty' or made of spaces.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>return value.IsEmpty() || value.IsSpaces()</returns>
        public static bool IsNullOrEmptyOrSpaces(this string value)
        {
            return value.IsNull() || value.IsEmpty() || value.IsSpaces();
        }


        /// <summary>
        /// Checks if the string is a UInt64 number (postive 64 bit number).
        /// </summary>
        /// <param name="value"></param>
        /// <returns>'value == String.Empty'.</returns>
        public static bool IsNumber(this string value)
        {
            UInt64 x = 0;
            //return global::System.lon.TryParse(value, out x);
            return System.UInt64.TryParse(value, out x);
        }

        /// <summary>
        /// Removes all the occurrences of specified 'items'.
        /// </summary>
        /// <param name="value">The string with the substring to remove.</param>
        /// <param name="items">The list of item to remove from.</param>
        /// <returns></returns>
        public static string Remove(this string value, params string[] items)
        {
            foreach (string item in items)
            {
                value = value.Replace(item, global::System.String.Empty);
            }
            return value;
        }
        #endregion
    }

}


