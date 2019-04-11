﻿/// <summary>
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
    public class OpenFileDialogViewModel : DialogViewModel
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
                if (_initialDirectory != value)
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
                if (_filter != value)
                {
                    _filter = value;
                    OnPropertyChanged();
                }
            }
        }

        private string[] _filePaths;
        public string[] FilePaths
        {
            get
            {
                return _filePaths;
            }
            set
            {
                if (_filePaths != value)
                {
                    _filePaths = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _multiSelect;
        public bool MultiSelect
        {
            get
            {
                return _multiSelect;
            }
            set
            {
                if(_multiSelect != value)
                {
                    _multiSelect = value;
                    OnPropertyChanged();
                }
            }
        }

        public OpenFileDialogViewModel()
        {

        }
    }
}
