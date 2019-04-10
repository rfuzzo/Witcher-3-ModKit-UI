using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using w3tools.App.Commands;
using WPFFolderBrowser;

namespace w3tools.App.ViewModels
{
    class SettingsViewModel : ViewModel
    {
        

        public SettingsViewModel()
        {
            OkCommand = new RelayCommand(Ok);
        }

        

        public ICommand OkCommand { get; }

        private bool? _dialogResult;
        public bool? DialogResult
        {
            get => _dialogResult;
            set
            {
                if (_dialogResult != value)
                {
                    _dialogResult = value;
                    OnPropertyChanged();
                }
            }
        }

        private void Ok()
        {
            DialogResult = true;
        }
    }
}
