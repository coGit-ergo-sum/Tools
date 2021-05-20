using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vi.Tools.Delegates
{
	/// <summary>
	/// Da utilizzare nei log destinati ai tecnici.
	/// </summary>
	/// <param name="messaggio">il messaggio da registrare.</param>
	/// <param name="line">La linea del codice dove il metodo Trace è stato chiamato.</param>
	/// <param name="member">Il ni dove il metodo Trace è stato chiamato.</param>
	/// <param name="file"></param>
	public delegate void TraceDelegate(string messaggio, int line = 0, string member = "?", string file = "?");

	// Da utilizzare nelle richieste di scelta da parte dell'utilizzatore dell'applicativo. (Le 'message box' Si-No)
	public delegate bool ConfirmDelegate(string messaggio);

	// Da utilizzare per le comunicazioni dello stato della applicazione all'utilizzatore.
	public delegate void InfoDelegate(string messaggio);
}
