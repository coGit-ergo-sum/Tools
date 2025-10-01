using System;

namespace SD.MySQL.Extensions.ColumnTypes
{
    /// <summary>
    /// Collection of 'extension methods' for byte
    /// </summary>
    public static partial class Methods
	{
        /// <summary>
        /// Check if the number is even (divisible by 2).
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>Returns '(value AND 1) == 0'.</returns>
        public static bool IsDECIMAL(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.DECIMAL);
        }


        public static bool IsTINYINT(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.TINYINT);
        }

        public static bool IsSMALLINT(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.SMALLINT);
        }


        public static bool IsINTEGER(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.INTEGER);
        }

        public static bool IsFLOAT(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.FLOAT);
        }

        public static bool IsDOUBLE(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.DOUBLE);
        }

        public static bool IsBIGINT(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.BIGINT);
        }

        public static bool IsMEDIUMINT(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.MEDIUMINT);
        }

        public static bool IsTIME(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.TIME);
        }

        public static bool IsDATE(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.DATE);
        }

        public static bool IsDATETIME(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.DATETIME);
        }

        public static bool IsYEAR(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.YEAR);
        }

        public static bool IsTINYBLOB(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.TINYBLOB);
        }

        public static bool IsMEDIUMBLOB(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.MEDIUMBLOB);
        }

        public static bool IsLONGBLOB(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.LONGBLOB);
        }

        public static bool IsBLOB(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.BLOB);
        }

        public static bool IsVARCHAR(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.VARCHAR);
        }

        public static bool IsTINYTEXT(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.TINYTEXT);
        }

        public static bool IsMEDIUMTEXT(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.MEDIUMTEXT);
        }

        public static bool IsLONGTEXT(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.LONGTEXT);
        }

        public static bool IsTEXT(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.TEXT);
        }

        public static bool IsVARBINARY(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.VARBINARY);
        }

        public static bool IsBINARY(this Vi.MySQL.Types.Column.Types? type)
        {
            return (type == Vi.MySQL.Types.Column.Types.BINARY);
        }



    }
}
/*
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
*/