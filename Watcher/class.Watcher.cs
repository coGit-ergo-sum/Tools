using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vi.Tools
{
    public class Watcher
    {

        public delegate void CopyDelegate(System.IO.WatcherChangeTypes changeType, string subPath, string fileName);
        public CopyDelegate Copy;
        private void OnCopy(System.IO.WatcherChangeTypes changeType, string subPath, string fileName)
        {
            if (this.Copy != null) { this.Copy(changeType, subPath, fileName); }
        }

        public delegate void ExceptionDelegate(System.Exception se);
        public ExceptionDelegate Exception;
        private void OnException(System.Exception se)
        {
            if (this.Exception != null) { this.Exception(se); }
        }

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

            this.FSW.Changed += FileSystemWatcher_Changed;
            this.FSW.Created += FileSystemWatcher_Created;
            this.FSW.Deleted += FileSystemWatcher_Deleted;
            this.FSW.Renamed += FileSystemWatcher_Renamed;
            this.FSW.Error += Fsw_Error;

            var directories = path.Split(System.IO.Path.DirectorySeparatorChar);
            var root = directories[directories.Length - 1];

            //string name = DateTime.Now.ToString("yyyy-MM-dd");
            string fullPath = System.IO.Path.Combine(backup, root);
            System.IO.Directory.CreateDirectory(fullPath);

            this.Backup = fullPath;
        }

        private void Fsw_Error(object sender, ErrorEventArgs e)
        {
            //listBox1.Items.Add(string.Format("Error:"));
        }

        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            this.Store(e);
        }

        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            this.Store(e);
        }

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            this.Store(e);
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            this.Store(e);
        }

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

                    //////////var directories = this.Path.Split(System.IO.Path.DirectorySeparatorChar);
                    //////////var root = directories[directories.Length - 1];
                    string yyyyMMdd = DateTime.Now.ToString("yyyy-MM-dd");
                    ////////////string fullPath = System.IO.Path.Combine(this.Backup, name);
                    var destinationPath = System.IO.Path.Combine(this.Backup, yyyyMMdd, subPath);


                    var destFileName = "";
                    int counter = 0;
                    do
                    {
                        var HHmmSS = System.DateTime.Now.ToString("HH-mm-ss");
                        var newFileName = String.Format("{0}.{1}.{2}", HHmmSS, counter, fileName);
                        destFileName = System.IO.Path.Combine(destinationPath, newFileName);
                    }
                    while (System.IO.File.Exists(destFileName));

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
//////      /// <summary>
//////      /// The base directory that the assembly resolver uses to probe for assemblies.
//////      /// </summary>
//////public string BaseDirectory { get; private set; }