using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vi.Delegates
{
	/// <summary>
	/// Da utilizzare nei log destinati ai tecnici.
	/// </summary>
	/// <param name="messaggio">il messaggio da registrare.</param>
	/// <param name="line">La linea del codice dove il metodo Trace è stato chiamato.</param>
	/// <param name="member">Il ni dove il metodo Trace è stato chiamato.</param>
	/// <param name="file"></param>
	public delegate void TraceDelegate(string messaggio, int line = 0, string member = "?", string file = "?");

    /// <summary>
    /// Delegate to handle confirmation requests from the user.
    /// </summary>
    /// <param name="messaggio">The message to display in the confirmation request.</param>
    /// <returns>True if the user confirms, otherwise false.</returns>
    public delegate bool ConfirmDelegate(string messaggio);

    /// <summary>
    /// Delegate to communicate application state information to the user.
    /// </summary>
    /// <param name="messaggio">The message to be communicated.</param>
    public delegate void InfoDelegate(string messaggio);

}
