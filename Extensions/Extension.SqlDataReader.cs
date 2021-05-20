using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vi.Tools.Extensions.String;

namespace Vi.Tools.Extensions.SqlDataReader
{
    public static partial class Methods
    {

        /// <summary>
        /// Converte il valore della colonna 'name' in 'Int32?'.
        /// </summary>
        /// <param name="dataReader">Il 'DataReader' con i dati da leggere.</param>
        /// <param name="name">Il nome della colonna da cui estrarre il valore in formato 'Int32'.</param>
        /// <returns>Il valore della colonna convertito nel formato 'Int32', se possibile, 'null' altrimenti anche in caso di 'System.Exception'.</returns>
        public static Int32? GetInt32(this System.Data.SqlClient.SqlDataReader dataReader, string name)
        {
            try
            {
                var ordinal = dataReader.GetOrdinal(name);
                return dataReader.IsDBNull(ordinal) ? (Int32?)null : (Int32?)dataReader.GetInt32(ordinal);
            }
            catch (System.Exception se)
            {
                return (Int32?)null;
            }
        }

        /// <summary>
        /// Converte il valore della colonna 'name' in 'Int32'. 
        /// </summary>
        /// <param name="dataReader">Il 'DataReader' con i dati da leggere.</param>
        /// <param name="name">Il nome della colonna da cui estrarre il valore in formato 'Int32'.</param>
        /// <param name="default">Il valore di default, da assegnare se per un qualsiasi motivo la lettura della colonna 'name' non andasse a buon fine.</param>
        /// <returns>Il valore della colonna convertito nel formato 'Int32', se possibile, 'default' altrimenti.</returns>
        public static Int32 GetInt32(this System.Data.SqlClient.SqlDataReader dataReader, string name, Int32 @default)
        {
            var value = dataReader.GetInt32(name);
            return value == null ? @default : (Int32)value;
        }

        public static Int32? GetInt32(this System.Data.SqlClient.SqlDataReader dataReader, string name, Int32? @default)
        {
            var x = dataReader[name];
            var value = dataReader.GetInt32(name);
            return value == null ? @default : (Int32)value;
        }

        /// <summary>
        /// Converte il valore della colonna 'name' in 'string'. 
        /// </summary>
        /// <param name="dataReader">Il 'DataReader' con i dati da leggere.</param>
        /// <param name="name">Il nome della colonna da cui estrarre il valore in formato 'string'.</param>
        /// <returns>Il valore della colonna 'name' convertito nel formato 'string' (può essere 'null').</returns>
        public static string GetString(this System.Data.SqlClient.SqlDataReader dataReader, string name)
        {
            var ordinal = dataReader.GetOrdinal(name);
            return (dataReader.IsDBNull(ordinal)) ? null : dataReader.GetString(ordinal);
        }

        /// <summary>
        /// Converte il valore della colonna 'name' in 'string'. 
        /// </summary>
        /// <param name="dataReader">Il 'DataReader' con i dati da leggere.</param>
        /// <param name="name">Il nome della colonna da cui estrarre il valore in formato 'string'.</param>
        /// <param name="default">Il valore di default, da assegnare se per un qualsiasi motivo la lettura della colonna 'name' non andasse a buon fine.</param>
        /// <returns>Il valore della colonna 'name' convertito nel formato 'string' (può essere 'null') il valore 'String.Empty' viene restituito così comè (non viene sostituito da '@default').</returns>
        public static string GetString(this System.Data.SqlClient.SqlDataReader dataReader, string name, string @default)
        {
            var value = dataReader.GetString(name);
            return value == null ? @default : value;
        }

        /// <summary>
        /// Converte il valore della colonna 'name' in 'DateTime'. 
        /// </summary>
        /// <param name="dataReader">Il 'DataReader' con i dati da leggere.</param>
        /// <param name="name">Il nome della colonna da cui estrarre il valore in formato 'DateTime'.</param>
        /// <returns>Il valore della colonna 'name' convertito nel formato 'DateTime' (può essere 'null').</returns>
        public static System.DateTime? GetDateTime(this System.Data.SqlClient.SqlDataReader dataReader, string name)
        {
            var ordinal = dataReader.GetOrdinal(name);
            var value = dataReader.GetDateTime(ordinal);
            return value;
        }



    }

}


