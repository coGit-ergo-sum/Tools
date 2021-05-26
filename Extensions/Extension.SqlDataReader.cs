using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vi.Tools.Extensions.String;

namespace Vi.Tools.Extensions.SqlDataReader
{

    /// <summary>
    /// Collection of extention methods for the SqlDataReader
    /// </summary>
    public static partial class Methods
    {

        /// <summary>
        /// Converts value in column 'name' to 'Int32?'.
        /// </summary>
        /// <param name="dataReader">The 'DataReader' containing the data.</param>
        /// <param name="name">The name of the column to convert to 'Int32'.</param>
        /// <returns>dataReader.GetInt32(name) if possible, 'null' otherwise ('System.Exception' included).</returns>
        public static Int32? GetInt32(this System.Data.SqlClient.SqlDataReader dataReader, string name)
        {
            try
            {
                var ordinal = dataReader.GetOrdinal(name);
                return dataReader.IsDBNull(ordinal) ? (Int32?)null : (Int32?)dataReader.GetInt32(ordinal);
            }
            catch (System.Exception)
            {
                return (Int32?)null;
            }
        }

        /// <summary>
        /// Converts value in column 'name' to 'Int32?'. 
        /// </summary>
        /// <param name="dataReader">The 'DataReader' containing the data.</param>
        /// <param name="name">The name of the column to convert to 'Int32'.</param>
        /// <param name="default">The returning valu is when the cast in not possible.</param>
        /// <returns>dataReader.GetInt32(name) if possible, 'default' otherwise ('System.Exception' included).</returns>
        public static Int32 GetInt32(this System.Data.SqlClient.SqlDataReader dataReader, string name, Int32 @default)
        {
            var value = dataReader.GetInt32(name);
            return value == null ? @default : (Int32)value;
        }

        /// <summary>
        /// Converts value in column 'name' to 'Int32?'. 
        /// </summary>
        /// <param name="dataReader">The 'DataReader' containing the data.</param>
        /// <param name="name">The name of the column to convert to 'Int32'.</param>
        /// <param name="default">The returning valu is when the cast in not possible.</param>
        /// <returns>dataReader.GetInt32(name) if possible, 'default' otherwise ('System.Exception' included).</returns>
        public static Int32? GetInt32(this System.Data.SqlClient.SqlDataReader dataReader, string name, Int32? @default)
        {
            /////var x = dataReader[name];
            var value = dataReader.GetInt32(name);
            return value == null ? @default : (Int32)value;
        }

        /// <summary>
        /// Converts value in column 'name' to 'string?'.
        /// </summary>
        /// <param name="dataReader">The 'DataReader' containing the data.</param>
        /// <param name="name">The name of the column to convert to 'string'.</param>
        /// <returns>dataReader.GetString(name) if possible, 'null' otherwise ('System.Exception' included).</returns>
        public static string GetString(this System.Data.SqlClient.SqlDataReader dataReader, string name)
        {
            var ordinal = dataReader.GetOrdinal(name);
            return (dataReader.IsDBNull(ordinal)) ? null : dataReader.GetString(ordinal);
        }

        /// <summary>
        /// Converts value in column 'name' to 'string?'. 
        /// </summary>
        /// <param name="dataReader">The 'DataReader' containing the data.</param>
        /// <param name="name">The name of the column to convert to 'string'.</param>
        /// <param name="default">The returning value default when the cast in not possible.</param>
        /// <returns>dataReader.GetString(name) if possible, 'default' otherwise ('System.Exception' included).</returns>
        public static string GetString(this System.Data.SqlClient.SqlDataReader dataReader, string name, string @default)
        {
            var value = dataReader.GetString(name);
            return value == null ? @default : value;
        }

        /// <summary>
        /// Converts value in column 'name' to 'DateTime?'.
        /// </summary>
        /// <param name="dataReader">The 'DataReader' containing the data.</param>
        /// <param name="name">The name of the column to convert to 'string'.</param>
        /// <param name="default">The returning value when the cast in not possible.</param>
        /// <returns>dataReader.GetDateTime(name) if possible, 'null' otherwise ('System.Exception' included).</returns>
        public static System.DateTime GetDateTime(this System.Data.SqlClient.SqlDataReader dataReader, string name, System.DateTime @default)
        {
            try
            {
                var ordinal = dataReader.GetOrdinal(name);
                var value = dataReader.GetDateTime(ordinal);
                return value == null ? @default : value;
            }
            catch (System.Exception)
            {
                return @default;
            }
        }



    }

}


