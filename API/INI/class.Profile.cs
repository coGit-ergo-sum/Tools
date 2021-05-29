
using Vi.Tools.Extensions.Long;
using Vi.Tools.Extensions.String;

namespace Vi.Tools
{
	/// <summary>
	/// Collects a set of methods to manage an 'INI' file. (choose this class if you want manage more tha one 'INI' file.) 
	/// The methods this class uses are implemented in 
	/// </summary>
	/// Provides methods to manipulate a 'INI' file. This class is only a wrapper around the static class: 'Profile' where methods are truly implemented.
	/// It provides an object oriented way to interact with the INI file. (for example: if your application needs more than one INI file, have many instances 
	/// of this class is a good way to cope with this requirement.)
	/// </summary>
	public class Profile
	{


		/// <summary>
		/// The full path of the INI file.
		/// </summary>
		/// <value>The full path of the INI file.</value>
		public readonly string FileName;

		/// <summary>
		/// Creates an object 'Profile' that doesn't send back any information on its flows.
		/// </summary>
		/// <param name="fileName">The name of the INI file currently used.</param>
		public Profile(string fileName)
		{
			Vi.Tools.statics.Profile.CheckFileName(fileName, this.OnWarning);
			this.FileName = fileName;
		}

		/// <summary>
		/// Creates an object 'Profile' that sends back information on its flows.
		/// </summary>
		/// <param name="fileName">The name of the INI file currently used.</param>
		/// <param name="warning">This function is called each time somenthing goes wrong. It ia a way to move outside of this class the management of the 'exceptions'.</param>
		public Profile(string fileName, Vi.Tools.Profile.WarningDelegate warning) : this(fileName)
		{
			this.Warning += warning;
		}

		#region event 'Warning'

		#region delegates
		/// <summary>
		/// The 'delegate' for the event 'Warning'.
		/// </summary>
		/// <param name="parameter">The name of the parameter with something wrong.</param>
		/// <param name="message">The info about what went wrong.</param>
		public delegate void WarningDelegate(string parameter, string message);

		#endregion

		#region events
		/// <summary>
		/// This event is fired, instead of a 'System.Exception' everytime something goes wrong.
		/// </summary>
		private event WarningDelegate Warning;

		#endregion

		#region OnWarning
		/// <summary>
		/// Internal method used to call properly the event 'Warning'
		/// </summary>
		/// <param name="parameter">The name of the parameter with something wrong.</param>
		/// <param name="message">The info about what went wrong.</param>
		/// <param name="fileName">The name of the INI file currently used.</param>
		private void OnWarning(string parameter, string message, string fileName)
		{
			if (this.Warning != null) { this.Warning(parameter, message); }
		}
		private void OnWarning(string parameter, string message)
		{
			this.OnWarning(parameter, message, this.FileName);
		}
		#endregion

		#endregion

		#region Sections
		/// <summary>
		/// Retrives all the 'Sections' in an INI file.
		/// </summary>
		/// <returns>The names of all the Sections.</returns>
		public string[] Sections()
		{
			return Vi.Tools.statics.Profile.Sections(this.FileName, this.OnWarning);
		}
		#endregion

		#region Keys
		/// <summary>
		/// Retrives all the 'Keys' belonging to a 'Sections' in an INI file.
		/// </summary>
		/// <param name="section">The name of the section to read.</param>
		/// <returns>The names of all the Keys in the provided section.</returns>
		public string[] Keys(string section)
		{
			return Vi.Tools.statics.Profile.Keys(section, this.FileName, this.OnWarning);
		}



		#endregion

		#region Read

		/// <summary>
		/// Reads a string from the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="default">The return value in case something  goes wrong.</param>
		/// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
		public string Read(string section, string key, string @default)
		{
			return Vi.Tools.statics.Profile.Read(section, key, @default, this.FileName, this.OnWarning);
		}

		/// <summary>
		/// Reads a boolean from the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="default">The return value in case something  goes wrong.</param>
		/// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
		public bool Read(string section, string key, bool @default)
		{
			return this.Read(section, key, System.String.Empty).ToBool(@default);
		}

		/// <summary>
		/// Reads an integer from the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="default">The return value in case something  goes wrong.</param>
		/// <param name="fileName">The full path of the INI file from which read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		/// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
		public int Read(string section, string key, int @default)
		{
			return this.Read(section, key, System.String.Empty).ToInt(@default);
		}

		/// <summary>
		/// Reads a long from the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="default">The return value in case something  goes wrong.</param>
		/// <param name="fileName">The full path of the INI file from which read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		/// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
		public long Read(string section, string key, long @default)
		{
			return this.Read(section, key, System.String.Empty).ToLong(@default);
		}

		/// <summary>
		/// Reads a DateTime from the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="default">The return value in case something  goes wrong.</param>
		/// <param name="fileName">The full path of the INI file from which read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		/// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
		public System.DateTime Read(string section, string key, System.DateTime @default)
		{
			return this.Read(section, key, System.String.Empty).Split(';')[0].ToLong(@default.Ticks).ToDateTime(@default);
		}

		/// <summary>
		/// Reads an array of integer from the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="default">The return value in case something  goes wrong.</param>
		/// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
		public int[] Read(string section, string key, int[] @default)
		{
			return this.Read(section, key, System.String.Empty).ToInt(';', @default);
		}

		/// <summary>
		/// Reads a points.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="default">The return value in case something  goes wrong.</param>
		/// <returns>A System.Drawing.Point from the INI file, if possible. 'default' otherwise.</returns>
		public System.Drawing.Point Read(string section, string key, System.Drawing.Point @default)
		{
			return Vi.Tools.statics.Profile.Read(section, key, @default, this.FileName);
		}

		/// <summary>
		/// Reads s 'Size'
		/// </summary>
		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="default"></param>
		/// <returns>A System.Drawing.Size from the INI file, if possible. 'default' otherwise.</returns>
		public System.Drawing.Size Read(string section, string key, System.Drawing.Size @default)
		{
			return Vi.Tools.statics.Profile.Read(section, key, @default, this.FileName);
		}

		/*
		/// <summary>
		/// Reads a 'System.Drawing.Point' from the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="default">The return value in case something  goes wrong.</param>
		/// <param name="fileName">The full path of the INI file from which read data. If null the callback 'onWarning' will be called.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		/// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
		public System.Drawing.Point Read(string section, string key, System.Drawing.Point @default)
		{
			var values = this.Read(section, key, new int[2] { @default.X, @default.Y });
			return new System.Drawing.Point(values[0], values[1]);
		}
		*/
		
		/*
		/// <summary>
		/// Reads a 'System.Drawing.Point' from the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="default">The return value in case something  goes wrong.</param>
		/// <param name="fileName">The full path of the INI file from which read data. If null the callback 'onWarning' will be called.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		/// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
		public System.Drawing.Size Read(string section, string key, System.Drawing.Size @default)
		{
			var values = this.Read(section, key, new int[2] { @default.Width, @default.Height });
			return new System.Drawing.Size(values[0], values[1]);
		}
		*/

		#endregion

		#region Write


		/// <summary>
		/// Writes a 'string' in the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="value">The value to write in the INI file.</param>
		/// <param name="fileName">The full path of the INI file where write data. If null the callback 'onWarning' will be called.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		public void Write(string section, string key, string value)
		{
			Vi.Tools.statics.Profile.Write(section, key, value, this.FileName, this.OnWarning);
		}

		/// <summary>
		/// Writes an 'integer' in the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="value">The value to write in the INI file.</param>
		/// <param name="fileName">The full path of the INI file where write data. If null the callback 'onWarning' will be called.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		public void Write(string section, string key, int value)
		{
			Vi.Tools.statics.Profile.Write(section, key, value, this.FileName, this.OnWarning);
		}


		/// <summary>
		/// Writes a 'long' in the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="value">The value to write in the INI file.</param>
		/// <param name="fileName">The full path of the INI file where write data. If null the callback 'onWarning' will be called.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		public void Write(string section, string key, long value)
		{
			Vi.Tools.statics.Profile.Write(section, key, value, this.FileName, this.OnWarning);
		}

		/// <summary>
		/// Writes 'DateTime.Ticks' in the INI file, (Wites also the date  in the format "yyyy-MM-dd HH:mm:ss.fff" only to make it human readable. This is ignored.)
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="value">The value to write in the INI file.</param>
		/// <param name="fileName">The full path of the INI file where write data. If null the callback 'onWarning' will be called.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		public void Write(string section, string key, System.DateTime value)
		{
			Vi.Tools.statics.Profile.Write(section, key, value, this.FileName, this.OnWarning);
		}

		/// <summary>
		/// Writes a 'boolean' in the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="value">The value to write in the INI file.</param>
		/// <param name="fileName">The full path of the INI file where write data. If null the callback 'onWarning' will be called.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		public void Write(string section, string key, bool value)
		{
			Vi.Tools.statics.Profile.Write(section, key, value, this.FileName, this.OnWarning);
		}

		/// <summary>
		/// Writes an array of integer separated by ';' in the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="values">The array of integer to write in the INI file.</param>
		public void Write(string section, string key, int[] values)
		{
			Vi.Tools.statics.Profile.Write(section, key, values, this.FileName, this.OnWarning);
		}

		/// <summary>
		/// Writes an array of boolean separated by ';' in the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="values">The array of booleans to write in the INI file.</param>
		public void Write(string section, string key, bool[] values)
        {
            Vi.Tools.statics.Profile.Write(section, key, values, this.FileName, this.OnWarning);
        }



		/////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Writes a 'System.Drawing.Point' value (can store the form position when the form is closing.)
		/// </summary>
		/// <param name="section">The name of t/he section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data. If null the callback 'onWarning' will be called.</param>
		/// <param name="point">A location object.</param>
		public void Write(string section, string key, System.Drawing.Point point)
		{
			Vi.Tools.statics.Profile.Write(section, key, point, this.FileName);
		}

		/// <summary>
		/// Writes a 'System.Drawing.Size' value (Can store the form size when the form is closing.)
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data. If null the callback 'onWarning' will be called.</param>
		/// <param name="size">A size object.</param>
		public void Write(string section, string key, System.Drawing.Size size)
		{
			Vi.Tools.statics.Profile.Write(section, key, size, this.FileName);
		}


		/*
		/// <summary>
		/// Writes a 'System.Drawing.Point' (as an array of integer) in the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="value">The value to write in the INI file.</param>
		/// <param name="fileName">The full path of the INI file where write data. If null the callback 'onWarning' will be called.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		public void Write(string section, string key, System.Drawing.Point value)
		{
			Vi.Tools.Profile.Write(section, key, value, this.FileName, this.OnWarning);
		}
		*/

		/*
		/// <summary>
		/// Writes a 'System.Drawing.Size' (as an array of integer) in the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="value">The value to write in the INI file.</param>
		/// <param name="fileName">The full path of the INI file where write data. If null the callback 'onWarning' will be called.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		public void Write(string section, string key, System.Drawing.Size value)
		{
			Vi.Tools.Profile.Write(section, key, value, this.FileName, this.OnWarning);
		}
		*/
		#endregion

		#region Delete
		/// <summary>
		/// Deletes a 'Key' (and its value) from the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		public void DeleteKey(string section, string key)
		{
			Vi.Tools.statics.Profile.DeleteKey(section, key, this.FileName, this.OnWarning);
		}

		/// <summary>
		/// Deletes all the keys under the provided section.
		/// </summary>
		/// <param name="section">The section to clear from all its keys.</param>
		/// <param name="key"></param>
		public void DeleteKeys(string section)
		{
			Vi.Tools.statics.Profile.DeleteKeys(section, this.FileName, this.OnWarning);
		}

		/// <summary>
		/// Deletes an entire section (and all its keys) from the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="fileName">The full path of the INI file where write data.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		public void DeleteSection(string section)
		{
			Vi.Tools.statics.Profile.DeleteSection(section, this.FileName, this.OnWarning);
		}

		/// <summary>
		/// Deletes everything. (Clears the INI file)
		/// </summary>
		public void DeleteAll()
		{
			Vi.Tools.statics.Profile.DeleteAll(this.FileName, this.OnWarning);
		}

		#endregion

		#region Show
		/// <summary>
		/// Opens the 'INI' file with the default application.
		/// </summary>
		/// <param name="fileName">The full path of the INI file where write data.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		public void Show()
		{
			try
			{
				System.Diagnostics.Process.Start(this.FileName);
			}
			catch (System.Exception se)
			{
				this.OnWarning("try/catch", se.Message);
			}

		}



		#endregion

	}
}
