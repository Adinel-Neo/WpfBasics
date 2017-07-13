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

            this.DataContext = new DirectoryStructureViewModel();
          
        }
        #endregion

       

    
    }
}
