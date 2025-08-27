using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Vi.Extensions.String;

namespace Vi.Types
{
    /// <summary>
    /// Collect some of the most common function on a string representing a Directory
    /// 
    /// </summary>
    public class Directory
    {
        /// <summary>
        /// The path in the string format.
        /// </summary>
        public readonly string Path;

        /// <summary>
        /// Determines whether the given path refers to an existing directory on disk.
        /// Uses 'System.IO.Directory.Exists'
        /// </summary>
        public bool Exists { get { return System.IO.Directory.Exists(this.Path); } }


        /// <summary>
        /// Main CTor (assigns the path)
        /// </summary>
        /// <param name="path">The path in the string format.</param>
        public Directory(string path)
        {
            this.Path = path.Trim('\\')  + "\\";
        }

        /// <summary>
        /// The name of the directory (without the path)
        /// </summary>
        public string Name {
            get {
                string folderName = System.IO.Path.GetFileName(this.Path.TrimEnd(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar));
                return folderName; 
            } 
        }

        /// <summary>
        /// checks if the access to the directory is denied!
        /// </summary>
        public bool IsAccessDenied
        {
            get
            {
                try
                {
                    var _ = System.IO.Directory.GetFiles(this.Path, "*.*");
                    return false;
                }
                catch (System.Exception)
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Determines whether the specified path is valid.
        /// A valid path is defined as one that:
        /// - Is not null or whitespace,
        /// - Does not contain invalid path characters,
        /// - Is rooted (i.e., has a valid drive or network location),
        /// - Does not exceed the maximum length of 260 characters.
        /// </summary>
        /// <returns>
        /// True if the path is valid; otherwise, false.
        /// </returns>
        public bool IsValid
        {
            get
            {
                return !(
                    (string.IsNullOrWhiteSpace(this.Path)) ||
                    (this.Path.IndexOfAny(System.IO.Path.GetInvalidPathChars()) >= 0) ||
                    !System.IO.Path.IsPathRooted(this.Path) ||
                    ((this.Path?.Length ?? 261) > 260)
                );
            }
        }

        /// <summary>
        /// Implements a kind of 'implicit' cast to make this type interchangeable between 'string' and 'Vi.Types.Directory'
        /// </summary>
        /// <param name="directory">The instance of a 'Directory' to cast as string.</param>
        public static implicit operator string(Vi.Types.Directory directory)
        {
            return directory?.Path;
        }

        /// <summary>
        /// Implements a kind of 'implicit' cast to make this type interchangeable between 'string' and 'Vi.Types.Directory'
        /// </summary>
        /// <param name="path">The string to 'cast'  into 'Vi.Types.Directory'.</param>
        public static implicit operator Vi.Types.Directory(string path)
        {
            return new Vi.Types.Directory(path);
        }

        /// <summary>
        /// Do Nothing. Returns the parameter.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vi.Types.Directory ToDirectory(Vi.Types.Directory value)
        {
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vi.Types.Directory ToDirectory(string value)
        {
            return value.ToDirectory();
        }

        /// <summary>
        /// Creates a new directory if not already existing
        /// </summary>
        public void Create()
        {
            if (!this.Exists)
            {
                System.IO.Directory.CreateDirectory(this.Path);
            }
        }


        /// <summary>
        /// Returns the Path (defined just for DEBUGGIN pourposes).
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Path;
        }

        /// <summary>
        /// Returns the subdirectories of the specified path.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns>The  array of subDirectories, Empty array otherwise</returns>
        private string[] SubDirectories()
        {
            try
            {
                return System.IO.Directory.GetDirectories(this.Path);
            }
            catch { return new string[0];}
        }

        /// <summary>
        /// Checks if the directory has subdirectories.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns>the number of sub directories, 0 otherwise.</returns>
        public bool HasSubDirectories()
        {
            return this.SubDirectories().Length > 0;
        }
    }
}
