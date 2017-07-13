using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WpfTreeView.Directory.Data;
using System.Windows.Controls;

namespace WpfTreeView.Directory
{
    /// <summary>
    /// A helper class to query information about directories
    /// </summary>
    public static class DirectoryStructure
    {
        /// <summary>
        /// Get all logical drives on the computer
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            //Get every logical drive on the machine
            return System.IO.Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
           
        }

        /// <summary>
        /// Gets the directories top-level content 
        /// </summary>
        /// <param name="fullPath">The full path to the directory</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            var itens = new List<DirectoryItem>();

            #region Get Folders


            try
            {
                var dirs = System.IO.Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    itens.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder}));
            }
            catch (Exception)
            {

                throw;
            }

           
            #endregion

            #region Get Files

           
            try
            {
                var fs = System.IO.Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    itens.AddRange(fs.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File}));
            }
            catch (Exception)
            {

                throw;
            }

            return itens;

            #endregion
        }


        #region Helpers
        /// <summary>
        /// Find the file or folder name from a full path
        /// </summary>
        /// <param name="path">The full path</param>
        /// <returns></returns>
        public static string GetFileOrFolderName(string path)
        {
            //if we have no path, return empty
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            //make all slashes back slaches
            var normalizedPath = path.Replace('/', '\\');

            //Find the last backslash in the path
            var lastIndex = normalizedPath.LastIndexOf('\\');

            // if we dont find a backslash, return the path itself
            if (lastIndex <= 0)

                return path;

            // return the name after the last backslash
            return path.Substring(lastIndex + 1);
        }
        #endregion
    }
}
