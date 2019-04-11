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
    public class SaveFileDialogViewModel : DialogViewModel
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

        private string _filter;
        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                if(_filter != value)
                {
                    _filter = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _filePath;
        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                if(_filePath != value)
                {
                    _filePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public SaveFileDialogViewModel()
        {






        }
    }
}
