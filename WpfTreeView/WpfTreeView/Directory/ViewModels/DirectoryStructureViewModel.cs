

using System.Collections.ObjectModel;
using System.Linq;
using WpfTreeView.Directory;
using WpfTreeView.Directory.Data;

namespace WpfTreeView
{
    /// <summary>
    /// Te view model for the applications mains directory view
    /// </summary>
    public class DirectoryStructureViewModel : BaseViewModel
    {
        public ObservableCollection<DirectoryItemViewModel> Itens { get; set; }

        /// <summary>
        /// Default Contructor
        /// </summary>
        public DirectoryStructureViewModel()
        {
            var children = DirectoryStructure.GetLogicalDrives();
            this.Itens = new ObservableCollection<DirectoryItemViewModel>(children.Select(drive => new DirectoryItemViewModel( drive.FullPath, DirectoryItemType.Drive)));

        }

    }
}
