//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading.Tasks;

//namespace Vi.API.CS
//{
//    public class Shell32
//    {
//        /// <summary>
//        /// Retrieves the target path of a Windows shortcut (.lnk file).
//        /// </summary>
//        /// <param name="lpszShortcutFile">The path to the shortcut file (.lnk).</param>
//        /// <param name="lpszTarget">A StringBuilder that receives the target path of the shortcut.</param>
//        /// <param name="cchTarget">A reference to an integer that specifies the size of the target path buffer.</param>
//        /// <returns>
//        /// Returns an integer indicating the success or failure of the operation. 
//        /// A return value of 0 indicates success (S_OK).
//        /// </returns>
//        [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
//        public static extern int SHGetShortcutTarget(string lpszShortcutFile, StringBuilder lpszTarget, ref int cchTarget);

//        /// <summary>
//        /// Analyzes the 'shortcut' file to find the source.
//        /// </summary>
//        /// <param name="file">The file to analyze</param>
//        /// <returns>If the file is a shortcut returns the target path, otherwise return the parameter file as it is.</returns>
//        public static string getTargetPath(Vi.Types.File file)
//        {
//            if (Path.GetExtension(file).Equals(".lnk", StringComparison.OrdinalIgnoreCase))
//            {
//                StringBuilder targetPath = new StringBuilder(260);
//                int targetPathLength = targetPath.Capacity;

//                int result = SHGetShortcutTarget(file.FullFileName, targetPath, ref targetPathLength);
//                return (result == 0) ? targetPath.ToString() : string.Empty;
//            }
//            else
//            {
//                return file;
//            }
//            //if (Path.GetExtension(filePath).Equals(".lnk", StringComparison.OrdinalIgnoreCase))
//            //{
//            //    WshShell shell = new WshShell();
//            //    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(filePath);
//            //    return shortcut.TargetPath; // Restituisce il percorso di destinazione
//            //}
//            //else
//            //{
//            //    return filePath; // Restituisce il file originale se non è un collegamento
//            //}
//        }
//    }
//}
