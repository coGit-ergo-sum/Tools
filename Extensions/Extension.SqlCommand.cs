using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Vi.Tools.Extensions.Object;

namespace Vi.Tools.Extensions.SqlCommand
{
    /// <summary>
    /// Collection of 'extension methods' for 'SqlClient.SqlCommand'
    /// </summary>
    public static partial class Methods
    {
        /// <summary>
        /// Esegue il command ottenendo un 'DataReader'. Se per il 'DataReader', 'HasRows = true' e 'dr.Read = true' cicla sul 'DataReader' ed evoca la 'callBack' ad ogni iterazione.
        /// </summary>
        /// <param name="command">Il command da eseguire.</param>
        /// <param name="row">La callback da eseguire su ogni singola riga dati del 'DataReader'.</param>
        public static void ExecuteReader(this System.Data.SqlClient.SqlCommand command, Func<System.Data.SqlClient.SqlDataReader, bool> row)
        {
            Methods.ExecuteReader(command, null, row);
        }

        /// <summary>
        /// Esegue il command ottenendo un 'DataReader'. Se per il 'DataReader', 'HasRows = true' e 'dr.Read = true' cicla sul 'DataReader' ed evoca la 'callBack' ad ogni iterazione.
        /// </summary>
        /// <param name="command">Il command da eseguire.</param>
        /// <param name="header">La callback da eseguire per realizzare una intestazione di tabelle dal 'DataReader'.</param>
        /// <param name="row">La callback da eseguire su ogni singola riga dati del 'DataReader'.</param>
        public static void ExecuteReader(this System.Data.SqlClient.SqlCommand command, Action<System.Data.SqlClient.SqlDataReader> header, Func<System.Data.SqlClient.SqlDataReader, bool> row)
        {
            var dr = command.ExecuteReader();

            if (header.IsNotNull()) header(dr);

            if (dr.HasRows)
            {
                bool continua = true;
                while (dr.Read() && continua)
                {
                    continua = row(dr);
                }
            }

            dr.Close();

        }

        /// <summary>
        /// Implementazione asincrona (semplificata) del metodo executeReader
        /// </summary>
        /// <param name="command">L'oggetto con le 'istruzioni' per interrogare il DB.</param>
        /// <param name="header">CallBack da eseguire non appena ricevuto il dataReader. Dovrebbe contenere i comandi per manipolare le colonne.</param>
        /// <param name="row">CallBack che viene chiamata iterativamente (ad ogni esecuzione di dr.HasRows). Per terminare il loop sul DataReader è sufficiente che il return sia false.</param>
        async public static void ExecuteReaderAsync(this System.Data.SqlClient.SqlCommand command, Action<System.Data.SqlClient.SqlDataReader> header, Func<System.Data.SqlClient.SqlDataReader, bool> row)
        {
            var dr = await command.ExecuteReaderAsync();

            if (header.IsNotNull()) header(dr);

            if (dr.HasRows)
            {
                bool continua = true;
                while (dr.Read() && continua)
                {
                    continua = row(dr);
                }
            }

            dr.Close();

        }
        
    }

}



