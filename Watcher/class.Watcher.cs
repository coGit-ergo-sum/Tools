using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vi.Extensions.String;

namespace Vi.Tools
{
    /// <summary>
    /// Observes a Directory and it's subDirectories, waiting for changes.
    /// If a change occours writes a copy of the changed file (before the change) in a backup diretory.
    /// </summary>
    public class Watcher
    {
        /// <summary>
        /// It's 'boilerplate code': the delegate for the event 'Copy'
        /// </summary>
        /// <param name="changeType">Changes that might occur to a file or directory.</param>
        /// <param name="subPath">The sub path where the file is stored (relative to the root path from where this watching is 'observing')</param>
        /// <param name="fileName">The name of the file copied.</param>
        public delegate void CopyDelegate(System.IO.WatcherChangeTypes changeType, string subPath, string fileName);

        /// <summary>
        /// Notifies the ouside world that a backup copy was created.
        /// </summary>
        public CopyDelegate Copy;
        private void OnCopy(System.IO.WatcherChangeTypes changeType, string subPath, string fileName)
        {
            if (this.Copy != null) { this.Copy(changeType, subPath, fileName); }
        }

        /// <summary>
        /// It's 'boilerplate code': the delegate for the event 'Exception'
        /// </summary>
        /// <param name="se"></param>
        public delegate void ExceptionDelegate(System.Exception se);

        /// <summary>
        ///  Notifies the ouside world that an exception occurren in this class.
        /// </summary>
        public ExceptionDelegate Exception;

        /// <summary>
        /// It's 'boilerplate code', but this method should be called instead of 'this.Exception'.
        /// </summary>
        private void OnException(System.Exception se)
        {
            if (this.Exception != null) { this.Exception(se); }
        }

        /// <summary>
        /// The 'pointer' to the watcher!
        /// </summary>
        private System.IO.FileSystemWatcher FSW;



        /// <summary>
        /// The directory to monitor, in standard or Universal Naming Convention (UNC) notation.
        /// </summary>
		public string Path { get { return this.FSW.Path; } }

        /// <summary>
        /// The type of files to watch. For example, "*.txt" watches for changes to all 'txt' files.
        /// </summary>
		public string Filter { get { return this.FSW.Filter; } }

        /// <summary>
        /// The full path of the backup directory, in standard or Universal Naming Convention (UNC) notation.
        /// </summary>
		public string Backup { get; private set; }


        /// <summary>
        /// Initialize a FileSystemWatcher.
        /// </summary>
        /// <param name="backup">The full path of the backup directory, in standard or Universal Naming Convention (UNC) notation.</param>
        /// <param name="path">The directory to monitor, in standard or Universal Naming Convention (UNC) notation.</param>
        /// <param name="notifyFilters">Specifies changes to watch for in a file or folder.</param>
        /// <param name="filter">The type of files to watch. For example, "*.txt" watches for changes to all 'txt' files.</param>
        public Watcher(string backup, string path, NotifyFilters notifyFilters, string filter)
        {

            this.FSW = new FileSystemWatcher(path, filter); // @"C:\Vi\Code\CSharp\CoViD", @"*.cs");

            //this.FSW.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.CreationTime;
            //this.FSW.NotifyFilter = NotifyFilters.FileName | NotifyFilters.FileName;
            //this.FSW.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Attributes | NotifyFilters.Size | NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.CreationTime | NotifyFilters.Security;
            this.FSW.NotifyFilter = notifyFilters;

            //this.FSW.Filter = "*.*";
            this.FSW.IncludeSubdirectories = true;
            this.FSW.EnableRaisingEvents = true;

            var eventHandler = new FileSystemEventHandler((sender, e) => { this.Store(e); });
            this.FSW.Changed += eventHandler;
            this.FSW.Created += eventHandler;
            this.FSW.Deleted += eventHandler;

            this.FSW.Renamed += new RenamedEventHandler((sender, e) => { this.Store(e); }); 

            this.FSW.Error += new ErrorEventHandler((sender, e) => { System.Console.WriteLine("Error"); }); ;

            var directories = path.Split(System.IO.Path.DirectorySeparatorChar);
            var root = directories[directories.Length - 1];

            //string name = DateTime.Now.ToString("yyyy-MM-dd");
            string fullPath = System.IO.Path.Combine(backup, root);
            System.IO.Directory.CreateDirectory(fullPath);

            this.Backup = fullPath;
        }

        /// <summary>
        /// Creates a copy of the modified file in the backup directory. 
        /// The backup directory is a 'mirror' of the original directory. 
        /// Each day there is a new backup diretory under the root folder.
        /// </summary>
        /// <param name="e">the parameter provided by all the managed events:{Changed, Created, Deleted, Renamed}</param>
        private void Store(FileSystemEventArgs e)
        {
            try
            {
                var fi = new System.IO.FileInfo(e.FullPath);

                // That prevents 'looping' if the backup directory is a subfolder of the watched directory.
                //if (fi.Attributes == FileAttributes.Archive && !e.FullPath.StartsWith(this.Backup))

                var isTMP = System.IO.Path.GetExtension(e.FullPath).ToUpper().EndsWith("TMP");
                var isArchive = (fi.Attributes == FileAttributes.Archive);
                var isBackup = e.FullPath.StartsWith(this.Backup);

                if (!isTMP && isArchive && !isBackup)
                {
                    var subPath = System.IO.Path.GetDirectoryName(e.Name);
                    var fileName = System.IO.Path.GetFileName(e.Name);

                    string yyyyMMdd = DateTime.Now.ToString("yyyy-MM-dd");
                    var destinationPath = System.IO.Path.Combine(this.Backup, yyyyMMdd, subPath);


                    var destFileName = "";
                    int counter = 0;
                    do
                    {
                        var HHmmSS = System.DateTime.Now.ToString("HH-mm-ss");
                        var newFileName = String.Format("{0}.{1}.{2}", HHmmSS, counter, fileName);
                        destFileName = System.IO.Path.Combine(destinationPath, newFileName);
                    }
                    // Tries to avoid name collision: 
                    // if the file already exists tries again with a new HHmmss
                    while (destFileName.ToFile().Exists);

                    System.IO.Directory.CreateDirectory(destinationPath);

                    System.IO.File.Copy(e.FullPath, destFileName, overwrite: false);

                    this.OnCopy(e.ChangeType, subPath, fileName);

                }
            }
            catch (System.Exception se)
            {
                this.OnException(se);
            }
        }

    }
}