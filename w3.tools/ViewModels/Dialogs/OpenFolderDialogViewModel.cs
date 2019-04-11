/// <summary>
/// Copyright
/// Limez3ro Viperkit
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3tools.App.ViewModels.Dialogs
{
    public class OpenFolderDialogViewModel : DialogViewModel
    {
        private string _initialDirectory;
        public string InitialDirectory
        {
            get
            {
                return _initialDirectory;
            }
            set
            {
                if(_initialDirectory != value)
                {
                    _initialDirectory = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _folderPath;
        public string FolderPath
        {
            get
            {
                return _folderPath;
            }
            set
            {
                if(_folderPath != value)
                {
                    _folderPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public OpenFolderDialogViewModel()
        {






        }
    }
}
