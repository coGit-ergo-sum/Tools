using System;
using System.Runtime.CompilerServices;

using Vi.Tools.Extensions.Object;
using Vi.Tools.Extensions.String;


namespace Vi.Tools.Extensions.NameValueCollection
{
    /// <summary>
    /// Descrizione di riepilogo per Extentions
    /// </summary>
    public static class Methods
    {


        public static int ToInt(this System.Collections.Specialized.NameValueCollection nvc, string name, int @default)
        {
            return nvc.ToString(name, null).ToInt(@default);
        }

        public static string ToString(this System.Collections.Specialized.NameValueCollection nvc, string name, string @default)
        {
            return (nvc.IsNull() || name.IsNullOrEmptyOrSpaces() || nvc[name].IsNull()) ? @default : nvc[name];
        }


        public static string ToString<E>(this System.Collections.Specialized.NameValueCollection nvc, string name) where E : System.Exception, new()
        {
            if (!nvc.IsNull() && !name.IsNullOrEmptyOrSpaces() && !nvc[name].IsNull()) return nvc[name];
            else throw (E)(new System.Exception("Impossible to cast the provided value."));
        }

    }
}




