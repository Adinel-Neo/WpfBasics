using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;



namespace WpfTreeView
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region On loaded
        /// <summary>
        /// When the application first opens 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Get every logical drive on the machine
            foreach (var drive in Directory.GetLogicalDrives())
            {
                //Create a new item for it
                var item = new TreeViewItem()
                {
                    //Set the header 
                    Header = drive,
                    //Set the full path
                    Tag = drive,
                };

                //Add dummy item
                item.Items.Add(null);

                //Listen out for item being explanded
                item.Expanded += Folder_Expanded;

                //Add it to the main tree-view
                FolderView.Items.Add(item);
            }
        }
        #endregion

        #region Folder Expanded
        /// <summary>
        /// When folder as expanded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region initial checks
            var item = (TreeViewItem)sender;

            //If the item only contains the dummy data
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            //Clear dummy data
            item.Items.Clear();

            //Get full path
            var fullpath = (string)item.Tag;

            #endregion

            #region Get Directories

            //Create a blank List for directories
            var directories = new List<string>();

            try
            {
                var dirs = Directory.GetDirectories(fullpath);

                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch (Exception)
            {

                throw;
            }

            //For each directory...
            directories.ForEach(directoryPath =>
            {
                //Create directory item
                var subItem = new TreeViewItem()
                {
                    //Set header as folder name
                    Header = GetFileOrFolderName(directoryPath),
                    //And tag as full path
                    Tag = directoryPath
                };

                //add dummy item so we can expand folder
                subItem.Items.Add(null);

                //Handle expanded
                subItem.Expanded += Folder_Expanded;

                //add this folder to the parent
                item.Items.Add(subItem);
            });

            #endregion

            #region Get Files

            //Create a blank List for Files
            var files = new List<string>();

            try
            {
                var fs = Directory.GetFiles(fullpath);

                if (fs.Length > 0)
                    files.AddRange(fs);
            }
            catch (Exception)
            {

                throw;
            }

            //For each File...
            files.ForEach(filePath =>
            {
                //Create file item
                var subItem = new TreeViewItem()
                {
                    //Set header as file name
                    Header = GetFileOrFolderName(filePath),
                    //And tag as full path
                    Tag = filePath
                };

                //add this folder to the parent
                item.Items.Add(subItem);
            });

            #endregion
        }
        #endregion

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
