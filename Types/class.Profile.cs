
using System;
using Vi.Extensions.Long;
using Vi.Extensions.String;
using Vi.Extensions.Array;
using System.Linq;

namespace Vi.Types
{
	/// <summary>
	/// Provides methods to manipulate a 'INI' file. This class is a wrapper around the static class: 'Vi.Statics.Profile' where methods are truly implemented.
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
		/// Creates an object 'Profile'.
		/// </summary>
		/// <param name="fileName">The name of the INI file currently used.</param>
		public Profile(string fileName)
		{
			Vi.Statics.Profile.CheckFileName(fileName, this.OnWarning);
			this.FileName = fileName;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class, used to access an INI fie.
        /// </summary>
        public Profile() { }

        /// <summary>
        /// Creates an object 'Profile' that sends back information on its flows.
        /// </summary>
        /// <param name="fileName">The name of the INI file currently used.</param>
        /// <param name="onWarning">This function is called each time somenthing goes wrong. It ia a way to move outside of this class the management of the 'exceptions'.</param>
        public Profile(string fileName, Vi.Statics.Profile.OnWarningDelegate onWarning) : this(fileName)
		{
			this.OnWarning += onWarning;
		}


		#region event 'OnWarning'


		#region events
		/// <summary>
		/// This event is fired, instead of a 'System.Exception' everytime something goes wrong.
		/// </summary>
		private event Vi.Statics.Profile.OnWarningDelegate OnWarning;

		#endregion

		#endregion

		#region Sections
		/// <summary>
		/// Retrives all the 'Sections' in an INI file.
		/// </summary>
		/// <returns>The names of all the Sections.</returns>
		public string[] Sections()
		{
			return Vi.Statics.Profile.Sections(this.FileName);
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
			return Vi.Statics.Profile.Keys(section, this.FileName, this.OnWarning);
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
			return Vi.Statics.Profile.Read(section, key, @default, this.FileName, this.OnWarning);
		}

        /// <summary>
        /// Reads a string from the INI file.
        /// </summary>
        /// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
        /// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
        /// <param name="default">The return value in case something  goes wrong.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
        /// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
        public string Read(string section, string key, string @default, Vi.Statics.Profile.OnWarningDelegate onWarning)
        {
            return Vi.Statics.Profile.Read(section, key, @default, this.FileName, onWarning);
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
			return this.Read(section, key, System.String.Empty, this.OnWarning).ToBool(@default);
		}

        /// <summary>
        /// Reads an integer from the INI file.
        /// </summary>
        /// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
        /// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
        /// <param name="default">The return value in case something  goes wrong.</param>
		/// <param name="onWarning">The callback to call in case of warning.</param>
        /// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
        public int Read(string section, string key, int @default, Vi.Statics.Profile.OnWarningDelegate onWarning)
        {
            return this.Read(section, key, System.String.Empty, onWarning).ToInt(@default);
        }

        /// <summary>
        /// Reads an integer from the INI file.
        /// </summary>
        /// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
        /// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
        /// <param name="default">The return value in case something  goes wrong.</param>
        /// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
        public int Read(string section, string key, int @default)
		{
			return this.Read(section, key, System.String.Empty, this.OnWarning).ToInt(@default);
		}

		/// <summary>
		/// Reads a long from the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="default">The return value in case something  goes wrong.</param>
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
		/// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
		public System.DateTime Read(string section, string key, System.DateTime @default)
		{
			return this.Read(section, key, System.String.Empty, this.OnWarning).Split(';')[0].ToLong(@default.Ticks).ToDateTime(@default);
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
			return this.Read(section, key, System.String.Empty, this.OnWarning).ToInt(';', @default);
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
			return Vi.Statics.Profile.Read(section, key, @default, this.FileName);
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
			return Vi.Statics.Profile.Read(section, key, @default, this.FileName, null);
		}



		/// <summary>
		/// Reads an array of integer from the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="default">The return value in case something  goes wrong.</param>
		/// <param name="fileName">The full path of the INI file from which read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		/// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
		public static int[] Read(string section, string key, int[] @default, string fileName, Vi.Statics.Profile.OnWarningDelegate onWarning)
		{
			return Vi.Statics.Profile.Read(section, key, System.String.Empty, fileName, onWarning).ToInt(';', @default);
		}

        /// <summary>
        /// Reads a section from the INI file and returns its key-value pairs as a NameValueCollection.
        /// </summary>
        /// <param name="section">The name of the section to read. If null, the callback 'onWarning' will be called.</param>
        /// <returns>A NameValueCollection containing the key-value pairs in the specified section.</returns>
        public System.Collections.Specialized.NameValueCollection Read(string section)
		{
			return Vi.Statics.Profile.Read(section, this.FileName);
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
		/// Writes a 'decimal' in the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="value">The value to write in the INI file.</param>
		public void Write(string section, string key, decimal value)
		{
			Vi.Statics.Profile.Write(section, key, value, this.FileName, this.OnWarning);
		}

		/// <summary>
		/// Writes a 'string' in the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="value">The value to write in the INI file.</param>
		public void Write(string section, string key, string value)
		{
			Vi.Statics.Profile.Write(section, key, value, this.FileName, this.OnWarning);
		}

		/// <summary>
		/// Writes an 'integer' in the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="value">The value to write in the INI file.</param>
		public void Write(string section, string key, int value)
		{
			Vi.Statics.Profile.Write(section, key, value, this.FileName, this.OnWarning);
		}


		/// <summary>
		/// Writes a 'long' in the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="value">The value to write in the INI file.</param>
		public void Write(string section, string key, long value)
		{
			Vi.Statics.Profile.Write(section, key, value, this.FileName, this.OnWarning);
		}

		/// <summary>
		/// Writes 'DateTime.Ticks' in the INI file, (Wites also the date  in the format "yyyy-MM-dd HH:mm:ss.fff" only to make it human readable. This is ignored.)
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="value">The value to write in the INI file.</param>
		public void Write(string section, string key, System.DateTime value)
		{
			Vi.Statics.Profile.Write(section, key, value, this.FileName, this.OnWarning);
		}

		/// <summary>
		/// Writes a 'boolean' in the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="value">The value to write in the INI file.</param>
		public void Write(string section, string key, bool value)
		{
			Vi.Statics.Profile.Write(section, key, value, this.FileName, this.OnWarning);
		}

		/// <summary>
		/// Writes an array of integer separated by ';' in the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="values">The array of integer to write in the INI file.</param>
		public void Write(string section, string key, int[] values)
		{
			Vi.Statics.Profile.Write(section, key, values, this.FileName, this.OnWarning);
		}

		/// <summary>
		/// Writes an array of boolean separated by ';' in the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="values">The array of booleans to write in the INI file.</param>
		public void Write(string section, string key, bool[] values)
        {
            Vi.Statics.Profile.Write(section, key, values, this.FileName, this.OnWarning);
        }

        /// <summary>
        /// Writes an array of strings separated by ';' in the INI file.
        /// </summary>
        /// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
        /// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
        /// <param name="values">The array of booleans to write in the INI file.</param>
        public void Write(string section, string key, string[] values)
        {
            Vi.Statics.Profile.Write(section, key, 	string.Join(";", values), this.FileName, this.OnWarning);
        }


        /// <summary>
        /// Writes a 'NameValueCollection' in the INI file. (It is a collection of key/value pairs.) Adds a key for each item in the collection.
        /// </summary>
        /// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
        /// <param name="nvc">the collection to store.</param>
        public void Write(string section, System.Collections.Specialized.NameValueCollection nvc)
        {
            Vi.Statics.Profile.Write(section, nvc, this.FileName);
        }

        /// <summary>
        /// Writes a 'System.Drawing.Point' value (can store the form position when the form is closing.)
        /// </summary>
        /// <param name="section">The name of t/he section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
        /// <param name="key">The name of the 'key' (in a 'section') where write data. If null the callback 'onWarning' will be called.</param>
        /// <param name="point">A location object.</param>
        public void Write(string section, string key, System.Drawing.Point point)
		{
			Vi.Statics.Profile.Write(section, key, point, this.FileName);
		}

		/// <summary>
		/// Writes a 'System.Drawing.Size' value (Can store the form size when the form is closing.)
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data. If null the callback 'onWarning' will be called.</param>
		/// <param name="size">A size object.</param>
		public void Write(string section, string key, System.Drawing.Size size)
		{
			Vi.Statics.Profile.Write(section, key, size, this.FileName);
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
			Vi.Types.Profile.Write(section, key, value, this.FileName, this.OnWarning);
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
			Vi.Types.Profile.Write(section, key, value, this.FileName, this.OnWarning);
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
			Vi.Statics.Profile.DeleteKey(section, key, this.FileName, this.OnWarning);
		}

		/// <summary>
		/// Deletes all the keys under the provided section.
		/// </summary>
		/// <param name="section">The section to clear from all its keys.</param>
		public void DeleteKeys(string section)
		{
			Vi.Statics.Profile.DeleteKeys(section, this.FileName, this.OnWarning);
		}

		/// <summary>
		/// Deletes an entire section (and all its keys) from the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		public void DeleteSection(string section)
		{
			Vi.Statics.Profile.DeleteSection(section, this.FileName, this.OnWarning);
		}

		/// <summary>
		/// Deletes everything. (Clears the INI file)
		/// </summary>
		public void DeleteAll()
		{
			Vi.Statics.Profile.DeleteAll(this.FileName, this.OnWarning);
		}

		#endregion

		#region Show
		/// <summary>
		/// Opens the 'INI' file with the default application.
		/// </summary>
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
