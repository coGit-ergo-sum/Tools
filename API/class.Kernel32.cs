using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Vi.API
{
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
		public static extern long WritePrivateProfileString(string section, string key, string value, string fileName);


		/// <summary
		/// Retrieves a string from the specified section in an initialization file.
		/// </summary>
		/// <param name="section">The name of the section to which the string will be copied. If the section does not exist, it is created. The name of the section is case-independent; the string can be any combination of uppercase and lowercase letters.</param>
		/// <param name="key">The name of the key to be associated with a string. If the key does not exist in the specified section, it is created. If this parameter is NULL, the entire section, including all entries within the section, is deleted.</param>
		/// <param name="@default"></param>
		/// <param name="retVal"></param>
		/// <param name="size"></param>
		/// <param name="fileName"></param>
		/// <returns></returns>
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I8)]
		public static extern long GetPrivateProfileString(string section, string key, string @default, StringBuilder sb, int size, string fileName);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I8)]
		public static extern long GetPrivateProfileString(string section, string key, string @default, char[] characters, int size, string fileName);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I8)]
		public static extern long GetPrivateProfileString(string section, string key, string @default, byte[] bytes, int size, string fileName);

		//[DllImport("Vi.Setting.dll", EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true), SuppressUnmanagedCodeSecurity]
		//public static extern int GetPrivateProfileString(string section, string key, string lpDefault, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] char[] lpReturnedString, int size, string fileName);

		#region GetPrivateProfileSectionNames
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I8)]
		public static extern long GetPrivateProfileSectionNames(char[] characters, int size, string fileName);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I8)]
		public static extern long GetPrivateProfileSectionNames(byte[] bytes, int size, string fileName);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I8)]
		public static extern long GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, uint size, string fileName);
		#endregion

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I8)]
		public static extern long GetPrivateProfileSection(string section, byte[] bytes, int size, string fileName);


		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I8)]
		public static extern long GetLastError();
		#endregion


	}
}
