/// <summary>
/// Copyright
/// Limez3ro Viperkit
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using w3tools.App.Commands;

namespace w3tools.App.ViewModels.Dialogs
{
    public class ExceptionDialogViewModel : DialogViewModel
    {
        private Exception _exception;
        public Exception Exception
        {
            get
            {
                return _exception;
            }
            set
            {
                if(_exception != value)
                {
                    _exception = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ExceptionString => Exception.ToString();

        public ICommand ContinueCommand { get; }
        public ICommand QuitCommand { get; }
        public ICommand ReportCommand { get; }

        public ExceptionDialogViewModel()
        {
            ContinueCommand = new RelayCommand(() => InvokeDialogCloseRequest(true));
            QuitCommand = new RelayCommand(() => InvokeDialogCloseRequest(false));
            ReportCommand = new RelayCommand(() => { }, () => false);
        }
    }
}