using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

using Vi.Tools.Extensions.String;
using Vi.Tools.Extensions.Long;

namespace Vi.INI
{
	/// <summary>
	/// Collects a set of static methods to manage an 'INI' file. (Opt-in for static methods if, for your application only needs one 'INI' file. 
	/// </summary>
	public  static partial class profile
	{

		#region event 'Warning'

		#region delegates
		/// <summary>
		/// The 'delegate' for the event 'Warning'.
		/// </summary>
		/// <param name="parameter">The name of the parameter with something wrong.</param>
		/// <param name="message">The info about what went wrong.</param>
		/// <param name="fileName">The name of the INI file currently used.</param>
		public delegate void WarningDelegate(string parameter, string message, string fileName);

		#endregion

		#region events
		/// <summary>
		/// This event is fired instead of a 'System.Exception' anytime something goes wrong.
		/// </summary>
		private static event WarningDelegate Warning;

		#endregion

		#region OnWarning
		/// <summary>
		/// Private method used to call properly the event 'Warning'
		/// </summary>
		/// <param name="parameter">The name of the parameter with something wrong.</param>
		/// <param name="message">The info about what went wrong.</param>
		/// <param name="fileName">The name of the INI file currently used.</param>
		private static void OnWarning(string parameter, string message, string fileName)
		{
			if (Vi.INI.profile.Warning != null) { Vi.INI.profile.Warning(parameter, message, fileName); }
		}

		#endregion

		#endregion

		#region Sections
		/// <summary>
		/// Retrives all the 'Sections' in an INI file.
		/// </summary>
		/// <param name="fileName">The name of the initialization file. (If the file was created using Unicode characters, the function writes Unicode characters to the file. Otherwise, the function writes ANSI characters.)</param>
		/// <param name="bufferSize">The number of bytes fetched by the method 'GetPrivateProfileString'. If the buffer is not enough to contain all the data. 'GetPrivateProfileString' is acalled again.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		/// <returns>The names of all the Sections or an empty array (a zero length array.)</returns>
		//private static string[] Sections(string[] @default, int bufferSize, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		private static string[] Sections(int bufferSize, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			var SUCCESS = -2147024896;
			var lengthMax = bufferSize - 2;

			char[] characters = new char[bufferSize];
			var length = Vi.API.Kernel32.GetPrivateProfileSectionNames(characters, characters.Length, fileName);

			if (length == 0)
			{
				var lastError = Marshal.GetHRForLastWin32Error();
				if (lastError == SUCCESS)
				{
					return new string[0];
				}
				else
				{
					var message = System.String.Format("Error: {0} executing 'GetPrivateProfileSectionNames'.", lastError);
					onWarning("sections", message, fileName);
					return new string[0];
				}
			}
			else if (length > 0 && length < lengthMax)
			{
				int _length = (int)Math.Min(length, int.MaxValue);
				string value = new string(characters, 0, _length - 1);
				string[] sections = Vi.INI.profile.ToArray(value);

				return sections;
			}
			else
			{
				return Vi.INI.profile.Sections(bufferSize * 2, fileName, onWarning);
			}

		}

		/// <summary>
		/// Retrives all the 'Sections' in an INI file.
		/// </summary>
		/// <param name="fileName">The name of the initialization file. (If the file was created using Unicode characters, the function writes Unicode characters to the file. Otherwise, the function writes ANSI characters.)</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		/// <returns>The names of all the Sections.</returns>
		public static string[] Sections(string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			int bufferSize = 256;
			////////string[] @default = null;
			return Vi.INI.profile.Sections(bufferSize, fileName, onWarning);
		}
		#endregion

		#region Keys
		/// <summary>
		/// Retrives all the 'Keys' belonging to a 'Sections' in an INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
		/// <param name="fileName">The full path of the INI file from which read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="bufferSize">The number of bytes fetched by the method 'GetPrivateProfileString'. If the buffer is not enough to contain all the data. 'GetPrivateProfileString' is acalled again.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		/// <returns>The names of all the Keys under a section.</returns>
		internal static string[] Keys(string section, int bufferSize, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			var value = Vi.INI.profile.Read(section, null, null, 1024, fileName, onWarning);
			string[] keys = Vi.INI.profile.ToArray(value);
			return keys;
		}

		/// <summary>
		/// Retrives all the 'Keys' belonging to a 'Sections' in an INI file.
		/// </summary>
		/// <param name="section"></param>
		/// <param name="fileName"></param>
		/// <param name="onWarning"></param>
		/// <returns>The names of all the Keys.</returns>
		public static string[] Keys(string section, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			int bufferSize = 256;
			return Vi.INI.profile.Keys(section, bufferSize, fileName, onWarning);
		}



		#endregion

		
		/// <summary>
		/// Reads data from the 'INI' using the method 'GetPrivateProfileString'
		/// </summary>
		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="default">The return value in case something  goes wrong.</param>
		/// <param name="fileName">The full path of the INI file from which read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="bufferSize">The number of bytes fetched by the method 'GetPrivateProfileString'. If the buffer is not enough to contain all the data. 'GetPrivateProfileString' is acalled again.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		/// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
		private static string Read(string section, string key, string @default, int bufferSize, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			try
			{
				var SUCCESS = -2147024896;
				var lengthMax = bufferSize - 2;

				char[] characters = new char[bufferSize];
				var length = Vi.API.Kernel32.GetPrivateProfileString(section, key, System.String.Empty, characters, characters.Length, fileName);

				if (length == 0)
				{
					var lastError = Marshal.GetHRForLastWin32Error();
					if (lastError == SUCCESS) { return System.String.Empty; }
					else
					{
						var message = System.String.Format("Error: {0} executing 'GetPrivateProfileString' for section = {1}, key {2}.", lastError, section, key);
						onWarning("key", message, fileName);
						return @default;
					}
					//else { Marshal.ThrowExceptionForHR(lastError); }
				}
				else if (length > 0 && length < lengthMax)
				{
					int _length = (int)Math.Min(length, int.MaxValue);
					return new string(characters, 0, _length);
				}
				else //if (length >= lengthMax && length < bufferSize)
				{
					return Vi.INI.profile.Read(section, key, @default, bufferSize * 2, fileName, onWarning);
				}
			}
			catch (System.Exception se)
			{
				onWarning("method", se.Message, fileName);
				return @default;
			}

		}


		/// <summary>
		/// Reads a string from the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="default">The return value in case something  goes wrong.</param>
		/// <param name="fileName">The full path of the INI file from which read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		/// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
		public static string Read(string section, string key, string @default, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			var parametersOk =
				Vi.INI.profile.CheckSection(section, fileName, onWarning) &&
				Vi.INI.profile.CheckKey(key, fileName, onWarning);
			int bufferSize = 64;
			return (parametersOk) ? Vi.INI.profile.Read(section, key, @default, bufferSize, fileName, onWarning) : @default;
		}


		/// <summary>
		/// Reads a DateTime from the INI file. The dateTime is stored as a 'long': (the value of the property 'ticks')
		/// </summary>
		/// <param name="section">The name of the section to which the string will be read. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') from where read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="default">The return value in case something  goes wrong.</param>
		/// <param name="fileName">The full path of the INI file from which read data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		/// <returns>The data read from the INI file. '@default' if something  whent wrong. (This method should't raise any exception).</returns>
		public static System.DateTime Read(string section, string key, System.DateTime @default, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			return Vi.INI.profile.Read(section, key, System.String.Empty, fileName, onWarning)
				.Split(';')[0]
				.ToLong(@default.Ticks)
				.ToDateTime(@default);
		}


		#region Write
		/// <summary>
		/// This is the 'method' used by all the other version of the method 'Write'
		/// </summary>
		/// <typeparam name="T">Can by any type with the method 'ToString'.</typeparam>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="value">The value to write in the INI file.</param>
		/// <param name="fileName">The full path of the INI file where write data. If null the callback 'onWarning' will be called.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		private static void Write<T>(string section, string key, T value, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			var parametersOk =
				Vi.INI.profile.CheckSection(section, fileName, onWarning) &&
				Vi.INI.profile.CheckKey(key, fileName, onWarning) &&
				Vi.INI.profile.CheckFileName(fileName, onWarning);

			if (parametersOk)
			{
				// ==================================================================================== //
				// The function 'Vi.API.Kernel32.WritePrivateProfileString' has a strange 
				// behaviour: spaces at the end of the of the 'key' are not removed.
				// E.g. writing 3 times the string "abc  " ('abc' plus 2 spaces) you 
				// got in the 'INI' file the string "abc      " ('abc' plus 6 spaces).
				//
				// To correct this behaviour the key is removed every time an assignment is made.
				Vi.INI.profile.DeleteKey(section, key, fileName, onWarning);
				// ==================================================================================== //

				var text = value.ToString();

				// Checks if value is null and removes chr(0) from the string.
				Vi.INI.profile.CheckValue(ref text, fileName, onWarning);

				Vi.API.Kernel32.WritePrivateProfileString(section, key, value.ToString(), fileName);
			}
		}





		/// <summary>
		/// Writes 'DateTime.Ticks' in the INI file, (Writes also the date  in the format "yyyy-MM-dd HH:mm:ss.fff" only to make it human readable. This is ignored.)
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="value">The value to write in the INI file.</param>
		/// <param name="fileName">The full path of the INI file where write data. If null the callback 'onWarning' will be called.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		public static void Write(string section, string key, System.DateTime value, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			// {0} contains the value we want to store
			// {1} contains the human readable format (will be ignored!)
			var text = System.String.Format(@"{0}; {1}", value.Ticks, value.ToString("yyyy-MM-dd HH:mm:ss.fff"));
			Vi.INI.profile.Write<string>(section, key, text, fileName, onWarning);
		}



		#endregion

		#region Delete
		/// <summary>
		/// Deletes a 'Key' (and its value) from the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="fileName">The full path of the INI file where write data.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		public static void DeleteKey(string section, string key, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			try
			{
				var parametersOk =
					Vi.INI.profile.CheckSection(section, fileName, onWarning) &&
					Vi.INI.profile.CheckKey(key, fileName, onWarning);

				if (parametersOk)
				{ Vi.API.Kernel32.WritePrivateProfileString(section, key, null, fileName); }
			}
			catch (System.Exception se)
			{
				onWarning("DeleteKey", se.Message, fileName);
			}
		}

		/// <summary>
		/// Deletes all the keys in a session, without deleting the session.
		/// </summary>
		/// <param name="section">The name of the section to clear from its keys. (The section will not be removed.)</param>
		/// <param name="fileName">The full path of the INI file.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		public static void DeleteKeys(string section, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			try
			{
				Vi.INI.profile.DeleteSection(section, fileName, onWarning);
				Vi.INI.profile.Write(section, "Fake", "To bin", fileName, onWarning);
				Vi.INI.profile.DeleteKey(section, "Fake", fileName, onWarning);
			}
			catch (System.Exception se)
			{
				onWarning("DeleteKey", se.Message, fileName);
			}
		}

		/// <summary>
		/// Deletes an entire section (all its keys and the section itself) from the INI file.
		/// </summary>
		/// <param name="section">The name of the section to totally remove from the 'INI file..</param>
		/// <param name="fileName">The full path of the INI file.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		public static void DeleteSection(string section, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			try
			{
				var parametersOk =
					Vi.INI.profile.CheckSection(section, fileName, onWarning) &&
					Vi.INI.profile.CheckFileName(fileName, onWarning);

				if (parametersOk)
				{ Vi.API.Kernel32.WritePrivateProfileString(section, null, null, fileName); }
			}
			catch (System.Exception se)
			{
				onWarning("DeleteKey", se.Message, fileName);
			}
		}

		/// <summary>
		/// Deletes everything. (Clears the INI file)
		/// </summary>
		/// <param name="fileName">The full path of the INI file to clear.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		public static void DeleteAll(string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			try
			{
				var filenameOK = Vi.INI.profile.CheckFileName(fileName, onWarning);
				if (filenameOK)
				{
					//Vi.API.Kernel32.WritePrivateProfileString(null, null, null, fileName);
					System.IO.File.WriteAllText(fileName, System.String.Empty, Encoding.Unicode);
				}
			}
			catch (System.Exception se)
			{
				onWarning("FileName", se.Message, fileName);
			}
		}

		#endregion

		#region Internal Checks
		/// <summary>
		/// Check the correctness of the parameter 'section'
		/// </summary>
		/// <param name="section">The name of a section in the INI file. (Can't be: null; empty; spaces.)</param>
		/// <param name="fileName">The full path of the INI file where write data.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		/// <returns>True if 'section' passes the checks. False otherwise and the callback 'onWarning' is called also.</returns>
		internal static bool CheckSection(string section, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			if (section == null)
			{
				onWarning("section", "Parameter 'section' can't be null. (Use DeleteSection instead.)", fileName);
				return false;
			}

			if (section == "")
			{
				onWarning("section", "Parameter 'section' can't be the empty string. (Use DeleteSection instead.)", fileName);
				return false;
			}

			if (section.Trim() == "")
			{
				onWarning("section", "Parameter 'section' can't be a strink of spaces.", fileName);
				return false;
			}

			return true;
		}

		/// <summary>
		/// Check the correctness of the parameter 'key'
		/// </summary>
		/// <param name="key">The name of a key in the INI file. (Can't be: null; empty; spaces.)</param>
		/// <param name="fileName">The full path of the INI file where write data.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		/// <returns>True if 'section' passes the checks. False otherwise and the callback 'onWarning' is called also.</returns>
		internal static bool CheckKey(string key, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			if (key == null)
			{
				onWarning("key", "Parameter 'key' can't be null. (Use DeleteKey instead.)", fileName);
				return false;
			}

			else if (key == "")
			{
				onWarning("key", "Parameter 'key' can't be the empty string. (Use DeleteKey instead.)", fileName);
				return false;
			}

			else if (key.Trim() == "")
			{
				onWarning("key", "Parameter 'key' can't be a string of spaces.", fileName);
				return false;
			}

			else
			{
				return true;
			}
		}

		/// <summary>
		/// Check the correctness of the parameter 'value' (When it is a string)
		/// </summary>
		/// <param name="value">The name of a key in the INI file. (Can't be: null; empty; spaces. Any Chr(0) will be removed)</param>
		/// <param name="fileName">The full path of the INI file where write data.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		/// <returns>True if 'section' passes the checks. False otherwise and the callback 'onWarning' is called also.</returns>
		internal static bool CheckValue(ref string value, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{

			if (value == null)
			{
				onWarning("value", "Parameter 'value' can't be null. (Use 'DeleteKey' instead.)", fileName);
			}

			// ===================================================================================== //
			// 'WritePrivateProfileString' accepts to write a string of spaces, but 
			// 'GetPrivateProfileString' does not read key values with spaces. For 
			// this consistency reason a string of spaces is not accepted (and trimmed).
			value = ("" + value).Trim();
			// ===================================================================================== //


			if (value.IndexOf('\0') > 0)
			{
				value = value.Replace("\0", " ");
				onWarning("value", @"Parameter 'value' contains the 'Chr(0)'. Each 'Chr(0)' has been replaced with a 'space'.", fileName);
			};

			return true;
		}

		/// <summary>
		/// Check the correctness of the parameter 'value' (when it is an array of integer.)
		/// </summary>
		/// <param name="value">The name of a key in the INI file. (Can't be: null.)</param>
		/// <param name="fileName">The full path of the INI file where write data.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		/// <returns>True if 'section' passes the checks. False otherwise and the callback 'onWarning' is called also.</returns>
		internal static bool CheckValues(int[] value, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			if (value == null)
			{
				onWarning("value", "Parameter 'value' can't be null. (Use DeleteKey instead).", fileName);
				return false;
			}

			return true;
		}
		/// <summary>
		/// Check the correctness of the parameter 'filename' (when it is an array of integer.)
		/// </summary>
		/// <param name="fileName">The full path of the INI file where write data. Must be a valid full file path and the file must exist.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		/// <returns>True if 'section' passes the checks. False otherwise and the callback 'onWarning' is called also.</returns>
		internal static bool CheckFileName(string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			if (fileName == null)
			{
				onWarning("fileName", "The 'fileName' can't be null.", fileName);
				return false;
			}

			else if (fileName.IsEmpty())
			{
				onWarning("fileName", "The 'fileName' can't be the empty string.", fileName);
				return false;
			}

			else if (fileName.IsSpaces())
			{
				onWarning("fileName", "The 'fileName' can't be a string of spaces.", fileName);
				return false;
			}

			if (System.IO.File.Exists(fileName))
			{
				var isValidExtention =
					fileName.EndsWith(".ini", StringComparison.OrdinalIgnoreCase) ||
					fileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase);

				if (isValidExtention)
				{
					return true;
				}
				else
				{
					onWarning("fileName", "The extention of the file: " + fileName + " must be a '.ini / *.txt'.", fileName);
					return false;
				}

			}
			else
			{
				//System.IO.File.WriteAllText(fileName, String.Empty, System.Text.Encoding.Unicode);
				onWarning("fileName", "The File: " + fileName + " has been created.", fileName);
				return true;
			}



		}
		#endregion

		/// <summary>
		/// Converts a 'null character' ('\0') separated string into an array of strings
		/// </summary>
		/// <param name="value">A 'null character' ('\0') separated string. (e.g. "Red\0Green\0Blue\0"). All the 'null character' at the end of the string will be truncated.
		/// <returns>The string 'value' splitted in an array of string. If 'value is 'null', 'empty' or 'spaces' returns an empty array of strings. ('new string[0]')</returns>
		public static string[] ToArray(string value)
		{
			bool isNull = (value == null) || (value.IsEmpty()) || (value.IsSpaces());

			// If value != null, all the trailing '\0' are removed (one shot). Then the string is splitted. 
			return isNull ? new string[0] : Regex.Replace(value, "[\0]*[\0]$", "").Split('\0');
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
		public static System.Drawing.Point Read(string section, string key, System.Drawing.Point @default, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			var values = Vi.INI.profile.Read(section, key, new int[2] { @default.X, @default.Y }, fileName, onWarning);
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
		public static System.Drawing.Size Read(string section, string key, System.Drawing.Size @default, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			var values = Vi.INI.profile.Read(section, key, new int[2] { @default.Width, @default.Height }, fileName, onWarning);
			return new System.Drawing.Size(values[0], values[1]);
		}
		*/


		/*
		/// <summary>
		/// Writes a 'System.Drawing.Point' (as an array of integer) in the INI file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be writed. If null the callback 'onWarning' will be called.</param>
		/// <param name="key">The name of the 'key' (in a 'section') where write data.  If null the callback 'onWarning' will be called.</param>
		/// <param name="value">The value to write in the INI file.</param>
		/// <param name="fileName">The full path of the INI file where write data. If null the callback 'onWarning' will be called.</param>
		/// <param name="onWarning">The callback used to manage exceptions.</param>
		public static void Write(string section, string key, System.Drawing.Point value, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			Vi.INI.profile.Write(section, key, new int[] { value.X, value.Y }, fileName, onWarning);
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
		public static void Write(string section, string key, System.Drawing.Size value, string fileName, Vi.INI.profile.WarningDelegate onWarning)
		{
			Vi.INI.profile.Write(section, key, new int[] { value.Width, value.Height }, fileName, onWarning);
		}
		*/
	}

}
