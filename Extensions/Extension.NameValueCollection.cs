using System;
using System.Runtime.CompilerServices;

using Vi.Tools.Extensions.Object;
using Vi.Tools.Extensions.String;


namespace Vi.Tools.Extensions.NameValueCollection
{

    /// <summary>
    /// Collection of 'extension methods' for NameValueCollection
    /// </summary>
    public static class Methods
    {

        /// <summary>
        /// Reads the value of the itmem with the provided 'name' in the 'NameValueCollection'.
        /// </summary>
        /// <param name="nvc">The 'NameValueCollection'.</param>
        /// <param name="name">The name of the item in the collection.</param>
        /// <param name="default">The value to return if the reading fails.</param>
        /// <returns>The value casted as 'int', of the item with the provided 'name' (nvc[name]). @default otherwise.</returns>
        public static int ToInt(this System.Collections.Specialized.NameValueCollection nvc, string name, int @default)
        {
            return nvc.GetString(name, null).ToInt(@default);
        }

        /// <summary>
        /// Reads the value of the itmem with the provided 'name' in the 'NameValueCollection'.
        /// </summary>
        /// <param name="nvc">The 'NameValueCollection'.</param>
        /// <param name="name">The name of the item in the collection.</param>
        /// <param name="default">The value to return if the reading fails.</param>
        /// <returns>The value of the item with the provided 'name' (nvc[name]). @default otherwise.</returns>
        public static string GetString(this System.Collections.Specialized.NameValueCollection nvc, string name, string @default)
        {
            try
            {
                return (nvc.IsNull() || name.IsNullOrSpaces() || nvc[name].IsNull()) ? @default : nvc[name];
            }
            catch (System.Exception)
            {
                return @default;
            }
        }

        /*
        public static string GetString<E>(this System.Collections.Specialized.NameValueCollection nvc, string name) where E : System.Exception, new()
        {
            if (!nvc.IsNull() && !name.IsNullOrEmptyOrSpaces() && !nvc[name].IsNull()) return nvc[name];
            else throw (E)(new System.Exception("Impossible to cast the provided value."));
        }
        */
    }
}




