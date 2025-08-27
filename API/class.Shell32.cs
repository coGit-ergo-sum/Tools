using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Vi.API.CS
{
    /// <summary>
    /// La versione 'C#' delle rispettive funzione API.
    /// </summary>
    public static partial class Shell32
    {


        /// <summary>
        /// Retrieves the target path of a Windows shortcut (.lnk file).
        /// </summary>
        /// <param name="lpszShortcutFile">The path to the shortcut file (.lnk).</param>
        /// <param name="lpszTarget">A StringBuilder that receives the target path of the shortcut.</param>
        /// <param name="cchTarget">A reference to an integer that specifies the size of the target path buffer.</param>
        /// <returns>
        /// Returns an integer indicating the success or failure of the operation. 
        /// A return value of 0 indicates success (S_OK).
        /// </returns>
        [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
        public static extern int SHGetShortcutTarget(string lpszShortcutFile, StringBuilder lpszTarget, ref int cchTarget);

        /// <summary>
        /// Analyzes the 'shortcut' file to find the source.
        /// </summary>
        /// <param name="file">The file to analyze</param>
        /// <returns>If the file is a shortcut returns the target path, otherwise return the parameter file as it is.</returns>
        public static string getTargetPath(Vi.Types.File file)
        {
            if (Path.GetExtension(file).Equals(".lnk", StringComparison.OrdinalIgnoreCase))
            {
                StringBuilder targetPath = new StringBuilder(260);
                int targetPathLength = targetPath.Capacity;

                int result = SHGetShortcutTarget(file, targetPath, ref targetPathLength);
                return (result == 0) ? targetPath.ToString() : string.Empty;
            }
            else
            {
                return file;
            }
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        public const uint SHGFI_ICON = 0x000000100;
        public const uint SHGFI_LARGEICON = 0x000000000;
        public const uint SHGFI_SMALLICON = 0x000000001;
        public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
        public const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        public static Icon GetFolderIcon(string folderPath, bool largeIcon)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            uint flags = SHGFI_ICON | SHGFI_USEFILEATTRIBUTES;
            if (largeIcon)
                flags |= SHGFI_LARGEICON;
            else
                flags |= SHGFI_SMALLICON;

            IntPtr hImgList = SHGetFileInfo(folderPath, FILE_ATTRIBUTE_DIRECTORY, ref shinfo, (uint)Marshal.SizeOf(shinfo), flags);

            if (shinfo.hIcon != IntPtr.Zero)
            {
                Icon icon = Icon.FromHandle(shinfo.hIcon);
                DestroyIcon(shinfo.hIcon); // Remember to release the handle
                return icon;
            }

            return null;
        }

    }
}
