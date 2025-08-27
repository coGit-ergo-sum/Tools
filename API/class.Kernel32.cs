using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Vi.API
{
	/// <summary>
	/// Translates the mothods of the API 'Kernel32' to make them usable like mormal c# methods. 
	/// </summary>
    public static class Kernel32
    {


		// ======================================================================================================================================================= //
		// ATTENTION  Using the native method 'WritePrivateProfileString' be aware of:
		//            - If 'value'   is NULL, the line indicated by SectionName and KeyName will be deleted.
		//            - If 'key'     is NULL, the entire section indicated by SectionName and all keys and values therein will be deleted.
		//            - If 'section' is NULL, the entire file will be deleted.
		// ======================================================================================================================================================= //


		#region Kernel32. Native function
		/// <summary>
		/// Copies a string into the specified section of an initialization file. Using the native unmanaged kernel's function.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be copied. If the section does not exist, it is created. The name of the section is case-independent; the string can be any combination of uppercase and lowercase letters.</param>
		/// <param name="key">The name of the key to be associated with a string. If the key does not exist in the specified section, it is created. If this parameter is NULL, the entire section, including all entries within the section, is deleted.</param>
		/// <param name="lpString">A string to be written to the file. If this parameter is NULL, the key pointed to by the 'key' parameter is deleted.</param>
		/// <param name="fileName">The name of the initialization file. (If the file was created using Unicode characters, the function writes Unicode characters to the file. Otherwise, the function writes ANSI characters.)</param>
		/// <returns>If the function successfully copies the string to the initialization file, the return value is nonzero. If the function fails, or if it flushes the cached version of the most recently accessed initialization file, the return value is zero.To get extended error information, call GetLastError.</returns>
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I8)]
		public static extern long WritePrivateProfileString(string section, string key, string lpString, string fileName);


		/// <summary>
		/// Retrieves a string from the specified section in an initialization file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be copied. If the section does not exist, it is created. The name of the section is case-independent; the string can be any combination of uppercase and lowercase letters.</param>
		/// <param name="key">The name of the key to be associated with a string. If the key does not exist in the specified section, it is created. If this parameter is NULL, the entire section, including all entries within the section, is deleted.</param>
		/// <param name="default">The returnig value if enything goes wrong.</param>
		/// <param name="sb">The string.Builder used to compose the resulting string.</param>
		/// <param name="size">The dimension of the buffer.</param>
		/// <param name="fileName">The name of the INI file.</param>
		/// <returns>A number segnalating if the function succeded.</returns>
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I8)]
		public static extern long GetPrivateProfileString(string section, string key, string @default, StringBuilder sb, int size, string fileName);

		/// <summary>
		/// Retrieves an array of characters from the specified section in an initialization file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be copied. If the section does not exist, it is created. The name of the section is case-independent; the string can be any combination of uppercase and lowercase letters.</param>
		/// <param name="key">The name of the key to be associated with a string. If the key does not exist in the specified section, it is created. If this parameter is NULL, the entire section, including all entries within the section, is deleted.</param>
		/// <param name="default">The returnig value if enything goes wrong.</param>
		/// <param name="characters">The container used to keep the result.</param>
		/// <param name="size">The dimension of the buffer.</param>
		/// <param name="fileName">The name of the INI file.</param>
		/// <returns>A number segnalating if the function succeded.</returns>
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I8)]
		public static extern long GetPrivateProfileString(string section, string key, string @default, char[] characters, int size, string fileName);

		/// <summary>
		/// Retrieves an array of bytes from the specified section in an initialization file
		/// </summary>
		/// <param name="section">The name of the section to which the string will be copied. If the section does not exist, it is created. The name of the section is case-independent; the string can be any combination of uppercase and lowercase letters.</param>
		/// <param name="key">The name of the key to be associated with a string. If the key does not exist in the specified section, it is created. If this parameter is NULL, the entire section, including all entries within the section, is deleted.</param>
		/// <param name="default">The returnig value if enything goes wrong.</param>
		/// <param name="bytes">The container used to keep the result.</param>
		/// <param name="size">The dimension of the buffer.</param>
		/// <param name="fileName">The name of the INI file.</param>
		/// <returns></returns>
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I8)]
		public static extern long GetPrivateProfileString(string section, string key, string @default, byte[] bytes, int size, string fileName);

		//[DllImport("Vi.Setting.dll", EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true), SuppressUnmanagedCodeSecurity]
		//public static extern int GetPrivateProfileString(string section, string key, string lpDefault, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] char[] lpReturnedString, int size, string fileName);

		#region GetPrivateProfileSectionNames
		/// <summary>
		/// 
		/// </summary>
		/// <param name="characters"></param>
		/// <param name="size"></param>
		/// <param name="fileName"></param>
		/// <returns></returns>
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I8)]
		public static extern long GetPrivateProfileSectionNames(char[] characters, int size, string fileName);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="bytes"></param>
		/// <param name="size"></param>
		/// <param name="fileName"></param>
		/// <returns></returns>
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I8)]
		public static extern long GetPrivateProfileSectionNames(byte[] bytes, int size, string fileName);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="lpszReturnBuffer"></param>
		/// <param name="size"></param>
		/// <param name="fileName"></param>
		/// <returns></returns>
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I8)]
		public static extern long GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, uint size, string fileName);
		#endregion

		/// <summary>
		/// Retrieves all the keys and values for the specified section of an initialization file.
		/// </summary>
		/// <param name="section">The name of the section in the initialization file.</param>
		/// <param name="bytes">A buffer that receives the key name and value pairs associated with the named section. The buffer is filled with one or more null-terminated strings; the last string is followed by a second null character.</param>
		/// <param name="size">The size of the buffer pointed to by the lpReturnedString parameter, in characters. The maximum profile section size is 32,767 characters.</param>
		/// <param name="fileName">The name of the initialization file. If this parameter does not contain a full path to the file, the system searches for the file in the Windows directory.</param>
		/// <returns>The return value specifies the number of characters copied to the buffer, not including the terminating null character. If the buffer is not large enough to contain all the key name and value pairs associated with the named section, the return value is equal to nSize minus two.</returns>
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I8)]
		public static extern long GetPrivateProfileSection(string section, byte[] bytes, int size, string fileName);

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I8)]
		public static extern long GetLastError();
		#endregion


	}
}
