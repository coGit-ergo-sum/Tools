using System;

namespace Vi.MySQL.Types
{

    public class Column
    {

        /*
        /// <summary>
        /// Casts 'value' to the correct type.
        /// </summary>
        /// <param name="value">The value to cast</param>
        /// <returns></returns>
        public T Parse<T>(string value)
        {
            return SD.MySQL.Types.Column.Parse<T>(value, this.Type);
        }
        */
        /*
        /// <summary>
        /// Casts 'value' to the correct type.
        /// </summary>
        /// <param name="value">The value to cast</param>
        /// <returns></returns>
        public static T Parse<T>(string value, Column.Types type)
        {

            return type switch
            {
                Column.Types.DECIMAL => decimal.Parse(value),
                Column.Types.TINYINT => byte.Parse(value),
                Column.Types.SMALLINT => Int16.Parse(value),
                Column.Types.INTEGER => int.Parse(value),
                Column.Types.FLOAT => float.Parse(value),
                Column.Types.DOUBLE => double.Parse(value),
                // Column.Types.TIMESTAMP => Timestamp.Parse(value),
                Column.Types.BIGINT => Int32.Parse(value),
                Column.Types.MEDIUMINT => Int32.Parse(value),
                //Column.Types.DATE => Date.Parse(value),

                Column.Types.TIME => TimeSpan.Parse(value),
                Column.Types.DATETIME => DateTime.Parse(value),
                Column.Types.YEAR => int.Parse(value),

                Column.Types.TINYBLOB => System.Convert.FromBase64String(value),
                Column.Types.MEDIUMBLOB => System.Convert.FromBase64String(value),
                Column.Types.LONGBLOB => System.Convert.FromBase64String(value),
                Column.Types.BLOB => System.Convert.FromBase64String(value),
                Column.Types.VARCHAR => value,
                Column.Types.TINYTEXT => value,
                Column.Types.MEDIUMTEXT => value,

                Column.Types.LONGTEXT => value,
                Column.Types.TEXT => value,
                Column.Types.VARBINARY => System.Convert.FromBase64String(value),
                Column.Types.BINARY => System.Convert.FromBase64String(value)
                //default => throw new InvalidOperationException("Unsupported column type.")

            };
        }
        */

        public static System.Type ToCSType(Vi.MySQL.Types.Column.Types columnType)
        {

            switch (columnType)
            {
                case Column.Types.DECIMAL: return typeof(decimal);
                case Column.Types.TINYINT: return typeof(byte);
                case Column.Types.SMALLINT: return typeof(short);
                case Column.Types.INTEGER: return typeof(int);
                case Column.Types.FLOAT: return typeof(float);
                case Column.Types.DOUBLE: return typeof(double);
                case Column.Types.BIGINT: return typeof(long);
                case Column.Types.MEDIUMINT: return typeof(int);
                case Column.Types.TIME: return typeof(TimeSpan);
                case Column.Types.DATETIME: return typeof(DateTime);
                case Column.Types.YEAR: return typeof(int);
                case Column.Types.TINYBLOB:
                case Column.Types.MEDIUMBLOB:
                case Column.Types.LONGBLOB:
                case Column.Types.BLOB:
                case Column.Types.VARBINARY:
                case Column.Types.BINARY: return typeof(byte[]);
                case Column.Types.VARCHAR:
                case Column.Types.TINYTEXT:
                case Column.Types.MEDIUMTEXT:
                case Column.Types.LONGTEXT:
                case Column.Types.TEXT: return typeof(string);
                default: throw new InvalidOperationException("Unsupported column type.");
            }

            /*
            return columnType switch
            {
                Column.Types.DECIMAL => typeof(decimal),
                Column.Types.TINYINT => typeof(byte),
                Column.Types.SMALLINT => typeof(short),
                Column.Types.INTEGER => typeof(int),
                Column.Types.FLOAT => typeof(float),
                Column.Types.DOUBLE => typeof(double),
                Column.Types.BIGINT => typeof(long),
                Column.Types.MEDIUMINT => typeof(int),
                Column.Types.TIME => typeof(TimeSpan),
                Column.Types.DATETIME => typeof(DateTime),
                Column.Types.YEAR => typeof(int),
                Column.Types.TINYBLOB => typeof(byte[]),
                Column.Types.MEDIUMBLOB => typeof(byte[]),
                Column.Types.LONGBLOB => typeof(byte[]),
                Column.Types.BLOB => typeof(byte[]),
                Column.Types.VARCHAR => typeof(string),
                Column.Types.TINYTEXT => typeof(string),
                Column.Types.MEDIUMTEXT => typeof(string),
                Column.Types.LONGTEXT => typeof(string),
                Column.Types.TEXT => typeof(string),
                Column.Types.VARBINARY => typeof(byte[]),
                Column.Types.BINARY => typeof(byte[]),
                _ => throw new InvalidOperationException("Unsupported column type.")
            };
            */
        }



        //public static T Parse<T>(string name)
        //{
        //    var resultOk = System.Enum.TryParse<Column.Types>(name, out Column.Types value);
        //    return resultOk ? value : null;
        //}

        /// <summary>
        /// List of native types defined in MySQL.
        /// The values assigned to the items are the same as those assigned to the MySqlTypes elements, making it easy and straightforward to cast between the two types.
        /// </summary>
        public enum Types
        {

            // ------------------------------------------------------------------------------ //
            // 1. Tipi di Dati Numerici
            // Integers:

            /// <summary>
            /// (NUMERIC) Decimal number with exact precision (specify precision and scale, e.g. DECIMAL(10,2)).
            /// </summary>
            DECIMAL = 0, // Decimal

            /// <summary>
            /// Very small integer (from -128 to 127 or from 0 to 255).
            /// </summary>
            TINYINT = 1,  // TINYINT

            /// <summary>
            /// Small integer (from -32,768 to 32,767 or from 0 to 65,535).
            /// </summary>
            SMALLINT = 2, // Int16

            /// <summary>
            ///  (INT) Standard integer (from -2,147,483,648 to 2,147,483,647 or from 0 to 4,294,967,295).
            /// </summary>
            INTEGER = 3, // Int32



            /// <summary>
            /// Floating-point number (approximate precision).
            /// </summary>
            FLOAT = 4, // Float

            /// <summary>
            /// Double-precision floating-point number (approximate precision).
            /// </summary>
            DOUBLE = 5, // Double

            /*
            /// <summary>
            /// Timestamp (date and time, with time zone support).
            /// </summary>
            TIMESTAMP = 7,  // Timestamp
            */


            /// <summary>
            /// Large integer (from -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807 or from 0 to 18,446,744,073,709,551,615).
            /// </summary>
            BIGINT = 8,  // Int64


            /// <summary>
            /// Medium integer (from -8,388,608 to 8,388,607 or from 0 to 16,777,215).
            /// </summary>
            MEDIUMINT = 9, // Int24


            /// <summary>
            /// Date (format YYYY-MM-DD).
            /// Date The supported range is '1000-01-01' to '9999-12-31'.
            /// </summary>
            DATE = 10,  // Date


            /// <summary>
            /// Time (format HH:MM:SS).
            /// The range is '-838:59:59' to '838:59:59'.
            /// </summary>
            TIME = 11, // Time

            /// <summary>
            /// Date and time (format YYYY-MM-DD HH:MM:SS).
            /// DateTime The supported range is '1000-01-01 00:00:00' to '9999-12-31 23:59:59'.
            /// </summary>
            DATETIME = 12, // DateTime

            /// <summary>
            /// Year (format YYYY).
            /// </summary>
            YEAR = 13, // Year

            //// -------------------------------
            ////
            ///// <summary>
            /////  Obsolete Use Datetime or Date type
            ///// </summary>
            //Newdate = 14,
            ////
            //// -------------------------------

            /*
            //// -------------------------------
            // Corrispondence NOT found
            /// <summary>
            /// Fixed-length string (n characters).
            /// </summary>
            CHAR = 11,
            //// -------------------------------
            */


            /// <summary>
            /// Very small binary blob (up to 255 bytes).
            /// </summary>
            TINYBLOB = 249, // TinyBlob

            /// <summary>
            /// Medium-sized binary blob (up to 16,777,215 bytes).
            /// </summary>
            MEDIUMBLOB = 250, // MediumBlob

            /// <summary>
            /// Very large binary blob (up to 4,294,967,295 bytes).
            /// </summary>
            LONGBLOB = 251,  // LongBlob


            /// <summary>
            /// Binary blob (up to 65,535 bytes).
            /// </summary>
            BLOB = 252,  // Blob

            /// <summary>
            /// Variable-length string (up to n characters).
            /// </summary>
            VARCHAR = 253, //  VarChar

            /// <summary>
            /// Very small text string (up to 255 characters).
            /// </summary>
            TINYTEXT = 749, //  TinyText

            /// <summary>
            /// Medium-sized text string (up to 16,777,215 characters).
            /// </summary>
            MEDIUMTEXT = 750, // MediumText

            /// <summary>
            /// Very large text string (up to 4,294,967,295 characters).
            /// </summary>
            LONGTEXT = 751,  // LongText

            /// <summary>
            /// Text string (up to 65,535 characters).
            /// </summary>
            TEXT = 752, // Text 


            /// <summary>
            /// Variable-length binary string (up to n bytes).
            /// </summary>
            VARBINARY = 753,  // VarBinary


            // These are binary values as the BLOB. the difference is that 
            // the length of the BINARY is fixed 
            /// <summary>
            /// Fixed-length binary string (n bytes).
            /// </summary>
            BINARY = 754 // Binary



            // ------------------------------------------------------------------------------ //
        }

        public string Name { get; set; }
        public string ParameterName { get { return "@" + this.Name; } }


        //public string Value { get; private set; }
        public Column.Types? Type { get; set; }

        public readonly bool IsOptional;



        public static Vi.MySQL.Types.Column.Types? TryParse(string type)
        {

            if (type.ToUpper()  == "INT") { type = "INTEGER"; }

            var resultOk = System.Enum.TryParse<Column.Types>(type, out Column.Types value);
            if (!resultOk)
            {
                Vi.Logger.Warn($"Unknown type: {type}");
                return null;
            }
            return value;
        }
        /*
        public static SD.MySQL.Types.Column.Types? TryParse(string type)
        {

            var resultOk = System.Enum.TryParse<Column.Types>(type, out Column.Types value);
            if (!resultOk) { Vi.Logger.Warn($"Unknown type: {type}"); }
            return resultOk ? value : null;
        }
        */

        public Column(string name, string type) : this(name, Vi.MySQL.Types.Column.TryParse(type)) { }

        public Column(string name, Column.Types? type) : this(name) { this.Type = type; }


        public Column(string name)
        {
            name = ("" + name).Trim();
            this.IsOptional = name.StartsWith("[") && name.EndsWith("]");
            this.Name = name.Trim('[').Trim(']').Trim();
        }

        /// <summary>
        /// Casts 'value' to the correct type.
        /// </summary>
        /// <param name="value">The value to cast</param>
        /// <returns></returns>
        public object Parse(string value)
        { 
            return Vi.MySQL.Types.Column.Parse(value, this.Type);
        }

        /// <summary>
        /// Casts 'value' to the correct type.
        /// </summary>
        /// <param name="value">The value to cast</param>
        /// <returns></returns>
        public static object Parse(string value, Column.Types? type)
        {
            /*
            return type switch
            {
                Column.Types.DECIMAL => decimal.Parse(value),
                Column.Types.TINYINT => byte.Parse(value),
                Column.Types.SMALLINT => Int16.Parse(value),
                Column.Types.INTEGER => int.Parse(value),
                Column.Types.FLOAT => float.Parse(value),
                Column.Types.DOUBLE => double.Parse(value),
                // Column.Types.TIMESTAMP => Timestamp.Parse(value),
                Column.Types.BIGINT => Int32.Parse(value),
                Column.Types.MEDIUMINT => Int32.Parse(value),
                //Column.Types.DATE => Date.Parse(value),

                Column.Types.TIME => TimeSpan.Parse(value),
                Column.Types.DATETIME => DateTime.Parse(value),
                Column.Types.YEAR => int.Parse(value),

                Column.Types.TINYBLOB => System.Convert.FromBase64String(value),
                Column.Types.MEDIUMBLOB => System.Convert.FromBase64String(value),
                Column.Types.LONGBLOB => System.Convert.FromBase64String(value),
                Column.Types.BLOB => System.Convert.FromBase64String(value),
                Column.Types.VARCHAR => value,
                Column.Types.TINYTEXT => value,
                Column.Types.MEDIUMTEXT => value,

                Column.Types.LONGTEXT => value,
                Column.Types.TEXT => value,
                Column.Types.VARBINARY => System.Convert.FromBase64String(value),
                Column.Types.BINARY => System.Convert.FromBase64String(value),
                _ => value

            };
            */
            if (type == null) { return value; }

            switch (type)
            {
                case Column.Types.DECIMAL: return decimal.Parse(value);
                case Column.Types.TINYINT: return byte.Parse(value);
                case Column.Types.SMALLINT: return Int16.Parse(value);
                case Column.Types.INTEGER: return int.Parse(value);
                case Column.Types.FLOAT: return float.Parse(value);
                case Column.Types.DOUBLE: return double.Parse(value);
                case Column.Types.BIGINT: return Int32.Parse(value);
                case Column.Types.MEDIUMINT: return Int32.Parse(value);
                case Column.Types.TIME: return TimeSpan.Parse(value);
                case Column.Types.DATETIME: return DateTime.Parse(value);
                case Column.Types.YEAR: return int.Parse(value);
                case Column.Types.TINYBLOB:
                case Column.Types.MEDIUMBLOB:
                case Column.Types.LONGBLOB:
                case Column.Types.BLOB:
                case Column.Types.VARBINARY:
                case Column.Types.BINARY: return System.Convert.FromBase64String(value);
                case Column.Types.VARCHAR:
                case Column.Types.TINYTEXT:
                case Column.Types.MEDIUMTEXT:
                case Column.Types.LONGTEXT:
                case Column.Types.TEXT: return value;
                default: return value;
            }
        }


        public override string ToString()
        {
            return this.Name + " - " + this.Type.ToString();
        }
    }





}



public enum MySqlTypes
{
    //
    // Summary:
    //     MySql.Data.MySqlClient.MySqlDbType.Decimal
    //     A fixed precision and scale numeric value between -1038 -1 and 10 38 -1.
    Decimal = 0, // DECIMAL

    //
    // Summary:
    //     MySql.Data.MySqlClient.MySqlDbType.Byte
    //     The signed range is -128 to 127. The unsigned range is 0 to 255.
    Byte = 1, // TINYINT

    //
    // Summary:
    //     MySql.Data.MySqlClient.MySqlDbType.Int16
    //     A 16-bit signed integer. The signed range is -32768 to 32767. The unsigned range
    //     is 0 to 65535
    Int16 = 2, // SMALLINT

    //
    // Summary:
    //     Specifies a 24 (3 byte) signed or unsigned value.
    Int24 = 9, // MEDIUMINT 

    //
    // Summary:
    //     MySql.Data.MySqlClient.MySqlDbType.Int32
    //     A 32-bit signed integer
    Int32 = 3, // INTEGER

    //
    // Summary:
    //     MySql.Data.MySqlClient.MySqlDbType.Int64
    //     A 64-bit signed integer.
    Int64 = 8, // BIGINT

    //
    // Summary:
    //     System.Single
    //     A small (single-precision) floating-point number. Allowable values are -3.402823466E+38
    //     to -1.175494351E-38, 0, and 1.175494351E-38 to 3.402823466E+38.
    Float = 4, // FLOAT

    //
    // Summary:
    //     MySql.Data.MySqlClient.MySqlDbType.Double
    //     A normal-size (double-precision) floating-point number. Allowable values are
    //     -1.7976931348623157E+308 to -2.2250738585072014E-308, 0, and 2.2250738585072014E-308
    //     to 1.7976931348623157E+308.
    Double = 5, // DOUBLE

    //
    // Summary:
    //     A timestamp. The range is '1970-01-01 00:00:00' to sometime in the year 2037
    Timestamp = 7, // TIMESTAMP

    //
    // Summary:
    //     Date The supported range is '1000-01-01' to '9999-12-31'.
    Date = 10, // DATE

    //
    // Summary:
    //     Time
    //     The range is '-838:59:59' to '838:59:59'.
    Time = 11, // TIME

    //
    // Summary:
    //     DateTime The supported range is '1000-01-01 00:00:00' to '9999-12-31 23:59:59'.
    DateTime = 12, // DATETIME

    //
    // Summary:
    //     Datetime The supported range is '1000-01-01 00:00:00' to '9999-12-31 23:59:59'.
    [Obsolete("The Datetime enum value is obsolete.  Please use DateTime.")]
    Datetime = 12,  // DATETIME

    //
    // Summary:
    //     A year in 2- or 4-digit format (default is 4-digit). The allowable values are
    //     1901 to 2155, 0000 in the 4-digit year format, and 1970-2069 if you use the 2-digit
    //     format (70-69).
    Year = 13, //  YEAR



    //
    // Summary:
    //     Obsolete Use Datetime or Date type
    Newdate = 14,

    //
    // Summary:
    //     A variable-length string containing 0 to 65535 characters
    VarString = 0xF, // TEXT

    //
    // Summary:
    //     Bit-field data type
    Bit = 0x10,

    //
    // Summary:
    //     Vector type
    Vector = 242,

    //
    // Summary:
    //     JSON
    JSON = 245,

    //
    // Summary:
    //     New Decimal
    NewDecimal = 246,

    //
    // Summary:
    //     An enumeration. A string object that can have only one value, chosen from the
    //     list of values 'value1', 'value2', ..., NULL or the special "" error value. An
    //     ENUM can have a maximum of 65535 distinct values
    Enum = 247,

    //
    // Summary:
    //     A set. A string object that can have zero or more values, each of which must
    //     be chosen from the list of values 'value1', 'value2', ... A SET can have a maximum
    //     of 64 members.
    Set = 248,

    //
    // Summary:
    //     A binary column with a maximum length of 255 (2^8 - 1) characters
    TinyBlob = 249, // TINYBLOB

    //
    // Summary:
    //     A binary column with a maximum length of 16777215 (2^24 - 1) bytes.
    MediumBlob = 250, // MediumBlob

    //
    // Summary:
    //     A binary column with a maximum length of 4294967295 or 4G (2^32 - 1) bytes.
    LongBlob = 251,  // LONGBLOB

    //
    // Summary:
    //     A binary column with a maximum length of 65535 (2^16 - 1) bytes.
    Blob = 252, // BLOB

    //
    // Summary:
    //     A variable-length string containing 0 to 255 bytes.
    VarChar = 253, // VARCHAR

    //
    // Summary:
    //     A fixed-length string.
    String = 254,

    //
    // Summary:
    //     Geometric (GIS) data type.
    Geometry = 0xFF,

    //
    // Summary:
    //     Unsigned 8-bit value.
    UByte = 501,

    //
    // Summary:
    //     Unsigned 16-bit value.
    UInt16 = 502,

    //
    // Summary:
    //     Unsigned 24-bit value.
    UInt24 = 509,

    //
    // Summary:
    //     Unsigned 32-bit value.
    UInt32 = 503,

    //
    // Summary:
    //     Unsigned 64-bit value.
    UInt64 = 508,

    //
    // Summary:
    //     Fixed length binary string.
    Binary = 754, // BINARY

    //
    // Summary:
    //     Variable length binary string.
    VarBinary = 753, // VARBINARY

    //
    // Summary:
    //     A text column with a maximum length of 255 (2^8 - 1) characters.
    TinyText = 749, // TINYTEXT

    //
    // Summary:
    //     A text column with a maximum length of 16777215 (2^24 - 1) characters.
    MediumText = 750, // MEDIUMTEXT

    //
    // Summary:
    //     A text column with a maximum length of 4294967295 or 4G (2^32 - 1) characters.
    LongText = 751, // LONGTEXT

    //
    // Summary:
    //     A text column with a maximum length of 65535 (2^16 - 1) characters.
    Text = 752, // TEXT

    //
    // Summary:
    //     A guid column.
    Guid = 854
}
