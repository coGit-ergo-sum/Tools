using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vi
{
    public class SQLLogger //////////////:Vi.ILog
    {
        public readonly string  ConnectionString;
        public SQLLogger(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        /// <summary>
        /// This is the most verbose logging level (maximum volume setting). Debug should be out-of-bounds for a production system and used only for development and testing.
        /// </summary>
        /// <param name="text">The text to log.</param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        /// <include file='Logger/XMLs/ILog.xml' path='Docs/method[@name="Debug"]/*' />
        #region Debug
        public void Debug(string text, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            this.Write(text, line, member, file);
        }
        #endregion

        /// <summary>
        /// Fatal is reserved for special exceptions/conditions where it is imperative that you can quickly pick out these events. 
        /// </summary>
        /// <param name="text">The text to log.</param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        /// <include file='Logger/XMLs/ILog.xml' path='Docs/method[@name="Fatal"]/*' />
        #region Fatal
        public void Fatal(string text, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            this.Write(text, line, member, file);
        }
        #endregion

        /// <summary>
        /// The 'Info' level is typically used to output information that is useful to the running and management of your system (production). 
        /// </summary>
        /// <param name="text">The text to log.</param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        /// <include file='Logger/XMLs/ILog.xml' path='Docs/method[@name="Info"]/*' />
        #region Info
        public void Info(string text, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            this.Write(text, line, member, file);
        }
        #endregion

        /// <summary>
        /// Warning is often used for handled 'exceptions' or other important log events.
        /// </summary>
        /// <param name="text">The text to log.</param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        /// <include file='Logger/XMLs/ILog.xml' path='Docs/method[@name="Warn"]/*' />
        #region Warn
        public void Warn(string text, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            this.Write(text, line, member, file);
        }
        #endregion

        /// <summary>
        /// Error is used to log all unhandled exceptions. 
        /// </summary>
        /// <param name="se">The exception to log.</param>
        /// <param name="line">The line from where this method was called.</param>
        /// <param name="member">The member from where this method was called.</param>
        /// <param name="file">The file from where this method was called.</param>
        /// <include file='Logger/XMLs/ILog.xml' path='Docs/method[@name="Error"]/*' />
        #region Error
        public void Error(System.Exception se, [CallerLineNumber] int line = 0, [CallerMemberName] System.String member = "?", [CallerFilePath] System.String file = "?")
        {
            this.Write(se.Message, line, member, file);
        }
        #endregion

        private void Write(string text, int line , System.String member, System.String file, [CallerMemberName] System.String livello = "?")
        {
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(this.ConnectionString);
                var command = new System.Data.SqlClient.SqlCommand("SP_Log", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@livello", livello));
                command.Parameters.Add(new SqlParameter("@Text", text));
                command.Parameters.Add(new SqlParameter("@Line", line));
                command.Parameters.Add(new SqlParameter("@Member", member));
                command.Parameters.Add(new SqlParameter("@File", file));
                command.Parameters.Add(new SqlParameter("@Application", System.AppDomain.CurrentDomain.FriendlyName));
                command.Parameters.Add(new SqlParameter("@Utente", "Vi"));


                connection.Open();


                command.ExecuteNonQuery();

            }
            catch (System.Exception se)
            {
                throw se;
            }
            finally
            {
                if ((connection != null) && (connection.State == ConnectionState.Open)) { connection.Close(); }
            }
        }
        
    }
}
