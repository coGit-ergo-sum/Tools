using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vi.Tools.Extensions.Object;
using Vi.Tools.Extensions.TimeSpan;
using Vi.Tools.Extensions.SqlDataReader;
using Vi.Tools.Extensions.SqlCommand;
using Vi.Tools.Extensions.Exception;

namespace Vi.Tools
{


    public class Utilities
    {

        /// <summary>
        /// Una variante del Metodo 'String.Join' Converte il parametro 'value' in una stringa di valori separati da ';'  
        /// /// </summary>
        /// <param name="values">Lista di oggetti da mettere in sequenza. Di ognuno viene convertito in stringa e vengono rimosse le enetuali occorrenze di ';'. Si esegue  'value[x].ToString().Replace(";", " ").</param>
        /// <returns>Una sequenza di stringhe separate da ' ; ' (compresi gli spazi). 'null' viene convertito in 'Empty'.</returns>
        public static string Join(params object[] values)
        {
            var strings = new string[values.Length];
            for (int index = 0; index < values.Length; index++)
            {
                var value = values[index];

                var item =
                    (value.IsNull()) ? String.Empty :
                    (value is DateTime) ? ((DateTime)value).ToString("dd/MM/yyyy") :
                    (value is TimeSpan) ? ((TimeSpan)value).ToHHMM() :
                    String.Empty + value.ToString();

                strings[index] = item.Trim().Replace(";", " ");
            }

            return String.Join("; ", strings);

        }


        public static void ExecuteReader(string connectionString, string sql, Func<DbDataReader, bool> row, Action<DbDataReader> header, Action<System.Exception> onException)
        {
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //connection.ConnectionString = connectionString;
                    //var connectionTimeout = connection.ConnectionTimeout;

                    connection.Open();

                    DbCommand command = connection.CreateCommand();
                    command.CommandText = sql;
                    command.CommandTimeout = 3;// secondi.

                    using (DbDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (header != null) { header(reader); }
                            if (row != null)
                            {
                                var continua = true;
                                while (continua && reader.Read())
                                {
                                    continua = row(reader);
                                }
                            }
                        }
                        else
                        {
                            onException(new System.Exception("Nessun dato da visualizzare."));
                        }
                    }
                }
            }
            catch (System.Exception se)
            {
                if (onException != null) { onException(se); }
            }
        }

        public static async Task ExecuteReaderAsync(string connectionString, string sql, Func<DbDataReader, bool> row, Action<DbDataReader> header, Action<System.Exception> onException)
        {
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //connection.ConnectionString = connectionString;
                    //var connectionTimeout = connection.ConnectionTimeout;

                    await connection.OpenAsync();

                    DbCommand command = connection.CreateCommand();
                    command.CommandText = sql;
                    command.CommandTimeout = 3;// secondi.

                    using (DbDataReader reader = await command.ExecuteReaderAsync(System.Data.CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (header != null) { header(reader); }
                            if (row != null)
                            {
                                var continua = true;
                                while (continua && await reader.ReadAsync())
                                {
                                    continua = row(reader);
                                }
                            }
                        }
                        else
                        {
                            onException(new System.Exception("Nessun dato da visualizzare."));
                        }
                    }
                }
            }
            catch(System.Exception se)
            {
                if (onException != null) { onException(se); }
            }
        }
        public static async Task FillAsync(string connectionString, string sql, Action<DataTable> onSuccess, Action<System.Exception> onException)
        {
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //connection.ConnectionString = connectionString;
                    //var connectionTimeout = connection.ConnectionTimeout;

                    await connection.OpenAsync();

                    DbCommand command = connection.CreateCommand();
                    command.CommandText = sql;
                    command.CommandTimeout = 3;// secondi.

                    using (var da = new System.Data.SqlClient.SqlDataAdapter(sql, connection))
                    {
                        // 3
                        // Use DataAdapter to fill DataTable
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        // 4
                        // Render data onto the screen
                        //return dt;

                        if(onSuccess != null) { onSuccess(dt); }
                    }
                }
            }
            catch(System.Exception se)
            {
                if (onException != null) { onException(se); }
               //return new DataTable();
            }
        }
    }
}
