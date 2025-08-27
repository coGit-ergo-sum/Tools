using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vi.Extensions.DateTime
{
    /// <summary>
    /// Collection of 'extension methods' for DateTime
    /// </summary>
    public static partial class Methods
	{
        /// <summary>
        /// Converts a DateTime in the format 'yyyy-MM-dd hh:mm:ss fff'
        /// </summary>
        /// <param name="value">The datetime to convert.</param>
        /// <returns>the datetime in the format 'yyyy-MM-dd hh:mm:ss fff'</returns>
        public static string ToYyyyMMddhhmmssfff(this System.DateTime value) { return value.ToString("yyyy-MM-dd hh:mm:ss fff"); }
        public static string ToYyyyMMdd(this System.DateTime value) { return value.ToString("yyyy-MM-dd"); }


        public static string ToHhmmss(this System.DateTime value) { return value.ToString("hh:mm:ss"); }

        /// <summary>
        /// Converts a DateTime in the format 'hh:mm:ss fff'
        /// </summary>
        /// <param name="value">The datetime to convert.</param>
        /// <returns>the datetime in the format 'hh:mm:ss fff'</returns>
        public static string ToHhmmssfff(this System.DateTime value) { return value.ToString("hh:mm:ss fff"); }

        //public static string ToMySQLDMAhms(this System.DateTime? value, System.DateTime @default)
        //{
        //    try
        //    {
        //        return value.HasValue ? (System.DateTime)value.Value : @default;
        //    }
        //    catch (System.Exception)
        //    {
        //        return @default;
        //    }
        //}


        /*
        /// <summary>
        /// Converts a long into a 'DateTime'
        /// </summary>
        /// <param name="value">The value to convert (It is regarded as a 'tick' number.)</param>
        /// <param name="default">The value to return if anything goes wrong.</param>
        /// <returns>A DateTime resulting from the long value, interpreted as a 'tick' value. '@default if anything goes wrong.</returns>
        public static System.DateTime ToDateTime(this System.DateTime? value, System.DateTime @default)
        {
            try
            {
                return value.HasValue ? (System.DateTime)value.Value : @default;
            }
            catch (System.Exception)
            {
                return @default;
            }
        }
        */
        /*
        /// <summary>
        /// Tries to cast a long as 'DateTime' if fails an exception of type 'E' is thrown.
        /// </summary>
        /// <typeparam name="E">The type of the exception to raise if the cast went wrong.</typeparam>
        /// <param name="value">The value to cast.</param>
        /// <returns>A 'dateTime' if the cast i successfull.</returns>
        /// <exception cref="E">if the  casting fails/exception>
        public static System.DateTime ToDateTime<E>(this System.DateTime? value) where E : System.Exception, new()
        {
            if (value.HasValue)
            {
                return (System.DateTime)value;
            }
            else
            {
                throw (E)(new System.Exception("Impossible to cast the provided value."));
            }
        }
        */

    }
}
