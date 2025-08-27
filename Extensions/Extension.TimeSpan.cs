using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vi.Extensions.TimeSpan
{
    /// <summary>
    /// Collection of extention methods for TimeSpan
    /// </summary>
    public static class Methods
    {
        /// <summary>
        /// Converts a TimeSpan into a string (format HH:MM)
        /// </summary>
        /// <param name="value">The timespan to convert.</param>
        /// <returns>A string in the format of 'HH:MM'.</returns>
        public static string ToHHMM(this System.TimeSpan value)
        {
            return System.String.Format("{0:00}:{1:00}", value.Hours, value.Minutes); 
        }

        /// <summary>
        /// Converts a TimeSpan into a string (format HH:MM)
        /// </summary>
        /// <param name="value">Il timespan da convertire.</param>
        /// <returns>Una stringa nel formato 'HH:MM' se il TimeSpan e not null, empty altrimenti.</returns>
        public static string ToHHMM(this System.TimeSpan? value)
        {
            return value.HasValue ? ((System.TimeSpan)value).ToHHMM() : "";
        }
    }

}
