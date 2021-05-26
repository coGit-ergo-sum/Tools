


//////////////using Vi.Tools.Extensions.String;

//////////////namespace Vi.Tools.statics
//////////////{
//////////////	// Collezione di metodi (di infrastruttura) in 'overload' con quelli fondamentali definiti nell'altro file di questa 'partial class'

//////////////	/// <summary>
//////////////	/// Collection of overloads for the methods 'Read'& 'Write'
//////////////	/// </summary>
//////////////	public static partial class Profile
//////////////	{
			   
//////////////		#region Read

//////////////		/// <summary>
//////////////		/// Reads a boolean from the INI file.
//////////////		/// </summary>
//////////////		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="default">The return value in case something  goes wrong.</param>
//////////////		/// <param name="fileName">The full path of the INI file from which read data.  If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="onWarning">The callback used to manage exceptions.</param>
//////////////		/// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
//////////////		public static bool Read(string section, string key, bool @default, string fileName, Vi.Tools.statics.Profile.WarningDelegate onWarning)
//////////////		{
//////////////			return Vi.Tools.statics.Profile.Read(section, key, System.String.Empty, fileName, onWarning).ToBool(@default);
//////////////		}

//////////////		/// <summary>
//////////////		/// Reads an integer from the INI file.
//////////////		/// </summary>
//////////////		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="default">The return value in case something  goes wrong.</param>
//////////////		/// <param name="fileName">The full path of the INI file from which read data.  If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="onWarning">The callback used to manage exceptions.</param>
//////////////		/// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
//////////////		public static int Read(string section, string key, int @default, string fileName, Vi.Tools.statics.Profile.WarningDelegate onWarning)
//////////////		{
//////////////			return Vi.Tools.statics.Profile.Read(section, key, System.String.Empty, fileName, onWarning).ToInt(@default);
//////////////		}

//////////////		/// <summary>
//////////////		/// Reads a long from the INI file.
//////////////		/// </summary>
//////////////		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="default">The return value in case something  goes wrong.</param>
//////////////		/// <param name="fileName">The full path of the INI file from which read data.  If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="onWarning">The callback used to manage exceptions.</param>
//////////////		/// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
//////////////		public static long Read(string section, string key, long @default, string fileName, Vi.Tools.statics.Profile.WarningDelegate onWarning)
//////////////		{
//////////////			return Vi.Tools.statics.Profile.Read(section, key, System.String.Empty, fileName, onWarning).ToLong(@default);
//////////////		}


//////////////		/// <summary>
//////////////		/// Reads an array of integer from the INI file.
//////////////		/// </summary>
//////////////		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="default">The return value in case something  goes wrong.</param>
//////////////		/// <param name="fileName">The full path of the INI file from which read data.  If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="onWarning">The callback used to manage exceptions.</param>
//////////////		/// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
//////////////		public static int[] Read(string section, string key, int[] @default, string fileName, Vi.Tools.statics.Profile.WarningDelegate onWarning)
//////////////		{
//////////////			return Vi.Tools.statics.Profile.Read(section, key, System.String.Empty, fileName, onWarning).ToInt(':', @default);
//////////////		}

//////////////		#endregion


//////////////		#region Write

//////////////		/// <summary>
//////////////		/// Writes an 'integer' in the INI file.
//////////////		/// </summary>
//////////////		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="value">The value to write in the INI file.</param>
//////////////		/// <param name="fileName">The full path of the INI file where write data. If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="onWarning">The callback used to manage exceptions.</param>
//////////////		public static void Write(string section, string key, int value, string fileName, Vi.Tools.statics.Profile.WarningDelegate onWarning)
//////////////		{
//////////////			Vi.Tools.statics.Profile.Write<int>(section, key, value, fileName, onWarning);
//////////////		}


//////////////		/// <summary>
//////////////		/// Writes a 'long' in the INI file.
//////////////		/// </summary>
//////////////		/// <param name="section">The name of the section where the string will be writed. If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="value">The value to write in the INI file.</param>
//////////////		/// <param name="fileName">The full path of the INI file where write data. If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="onWarning">The callback used to manage exceptions.</param>
//////////////		public static void Write(string section, string key, long value, string fileName, Vi.Tools.statics.Profile.WarningDelegate onWarning)
//////////////		{
//////////////			Vi.Tools.statics.Profile.Write<long>(section, key, value, fileName, onWarning);
//////////////		}


//////////////		/// <summary>
//////////////		/// Writes a 'boolean' in the INI file.
//////////////		/// </summary>
//////////////		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="value">The value to write in the INI file.</param>
//////////////		/// <param name="fileName">The full path of the INI file where write data. If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="onWarning">The callback used to manage exceptions.</param>
//////////////		public static void Write(string section, string key, bool value, string fileName, Vi.Tools.statics.Profile.WarningDelegate onWarning)
//////////////		{
//////////////			Vi.Tools.statics.Profile.Write<bool>(section, key, value, fileName, onWarning);
//////////////		}

//////////////		/// <summary>
//////////////		/// Writes a 'string' in the INI file.
//////////////		/// </summary>
//////////////		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="value">The value to write in the INI file.</param>
//////////////		/// <param name="fileName">The full path of the INI file where write data. If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="onWarning">The callback used to manage exceptions.</param>
//////////////		public static void Write(string section, string key, string value, string fileName, Vi.Tools.statics.Profile.WarningDelegate onWarning)
//////////////		{
//////////////			// 'Comments' are not allowed when the value is a string.
//////////////			Vi.Tools.statics.Profile.Write<string>(section, key, value, fileName, onWarning);
//////////////		}


//////////////		/// <summary>
//////////////		/// Writes an array of integer separated by ';' in the INI file.
//////////////		/// </summary>
//////////////		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="values">The value to write in the INI file.</param>
//////////////		/// <param name="fileName">The full path of the INI file where write data. If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="onWarning">The callback used to manage exceptions.</param>
//////////////		public static void Write(string section, string key, int[] values, string fileName, Vi.Tools.statics.Profile.WarningDelegate onWarning)
//////////////		{
//////////////			Vi.Tools.statics.Profile.Write<string>(section, key, System.String.Join(";", values), fileName, onWarning);
//////////////		}

//////////////		/// <summary>
//////////////		/// Writes an array of booleans separated by ';' in the INI file.
//////////////		/// </summary>
//////////////		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="values">The value to write in the INI file.</param>
//////////////		/// <param name="fileName">The full path of the INI file where write data. If null the callback 'onWarning' will be called.</param>
//////////////		/// <param name="onWarning">The callback used to manage exceptions.</param>
//////////////		public static void Write(string section, string key, bool[] values, string fileName, Vi.Tools.statics.Profile.WarningDelegate onWarning)
//////////////        {
//////////////            Vi.Tools.statics.Profile.Write<string>(section, key, System.String.Join(";", values), fileName, onWarning);
//////////////        }



//////////////        #endregion
//////////////    }


//////////////}
