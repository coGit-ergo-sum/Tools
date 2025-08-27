using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Vi.Types
{
    /// <summary>
    /// Represents a file and provides utility methods for file operations.
    /// It also create a Type (for a string representing a file) used to 
    /// enforce method overload 
    /// </summary>
    public class File
    {


        /// <summary>
        /// Collector of the most common function around a 'File'
        /// </summary>
        public readonly string FullFileName;

        //public bool IsDirectory { get { return System.IO.Directory.Exists(this.FullFileName); } }
        //public bool IsFile { get { return System.IO.File.Exists(this.FullFileName) || } };
        /// <summary>
        /// Determines if the file exists (applies 'System.IO.File.Exists')
        /// </summary>
        public bool Exists {get { return System.IO.File.Exists(this.FullFileName); } }

        /// <summary>
        /// The LONG version of the extension WITH the dot.
        /// </summary>
        public string Extention { get { return  ("" + System.IO.Path.GetExtension(this.FullFileName)).ToLower(); } }

        /// <summary>
        /// The SHORT version of the extension WITHOUT the dot.
        /// </summary>
        public string Ext { get { return this.Ext.Trim('.'); } }

        /// <summary>
        /// Determines whether the specified file path is valid.
        /// A valid path is defined as one that:
        /// - Is not null or whitespace,
        /// - Does not contain invalid path characters,
        /// - Is rooted (i.e., has a valid drive or network location),
        /// - Does not exceed the maximum length of 260 characters.
        /// </summary>
        /// <returns>
        /// True if the file path is valid; otherwise, false.
        /// </returns>
        public bool IsValidPath
        {
            get
            {
                return 
                    (this?.Path.IsValid ?? false) &&
                    ((this?.FullName.Length ?? 0) > 0) &&
                    (this.FullName.IndexOfAny(System.IO.Path.GetInvalidPathChars()) < 0) &&
                    (this?.FullFileName.Length ?? 261) < 261;
            }
        }

        /// <summary>
        /// Checks if the file is 'protected' against any kind of reading
        /// </summary>
        public bool IsAccessDenied
        {
            get
            {
                try
                {
                    var di = new System.IO.FileInfo(this.FullFileName);
                    return false;
                }
                catch (System.Exception)
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Determines if 'FullFileName' is empty (returns: ("" + this.FullFileName).Trim().Length == 0;)
        /// </summary>
        public bool IsEmpty { get { return ("" + this.FullFileName).Trim().Length == 0; } }

        /// <summary>
        /// Returns the File Name WITH the extention. Applies 'System.IO.Path.GetFileName'
        /// </summary>
        public string FullName
        {
            get
            {
                string fileNameExt = System.IO.Path.GetFileName(this.FullFileName.TrimEnd(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar));
                return fileNameExt; 
            }
        }

        /// <summary>
        /// Returns the file name without the extension. 
        /// Applies 'System.IO.Path.GetFileNameWithoutExtension'.
        /// </summary>
        public string FileName
        {
            get
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(this.FullFileName.TrimEnd(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar));
                return fileName;
            }
        }

        /// <summary>
        /// The File's 'full path'. (Applies 'System.IO.Path.GetDirectoryName')
        /// </summary>
        public Vi.Types.Directory Path
        {
            get
            {
                return System.IO.Path.GetDirectoryName(this.FullFileName);
            }
        }

        /// <summary>
        /// Main CTor. Trims the parameter and Assigns it the value 'fullFileName' to 'this.FullFIleName'. (this.FullFileName = fullFileName?.Trim() ?? "";)
        /// </summary>
        /// <param name="fullFileName"></param>
        public File(string fullFileName)
        {
            this.FullFileName = fullFileName?.Trim() ?? "";
        }

        /// <summary>
        /// Casting 'on the fly' to 'string' of the parameter
        /// </summary>
        /// <param name="file">The instance to be converted in 'string'.</param>
        public static implicit operator string (File file)
        {
            return file?.FullFileName;
        }

        /// <summary>
        /// Casting 'on the fly' to 'File' of the parameter
        /// </summary>
        /// <param name="fullFileName">the fully qualified file nane to be converted in 'Vi.Types.File'</param>
        public static implicit operator Vi.Types.File(string fullFileName)
        {
            return new Vi.Types.File(fullFileName);
        }

        /// <summary>
        /// Performs an explicit casting from string to 'Vi.Types.File'.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vi.Types.File ToFile(string value)
        {
            return new Vi.Types.File(value);
        }

        /// <summary>
        /// Returns the FullFileName (defined just for DEBUGGING pourposes.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.FullFileName;
        }
    }
}