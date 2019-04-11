/// <summary>
/// Copyright
/// Limez3ro Viperkit
/// </summary>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace w3tools.App.ViewModels.Dialogs
{
    using App.Commands;

    /// <summary>
    /// Represents a dialog viewmodel that handles a dialog to alert the user to unsaved document changes.
    /// </summary>
    public class UnsavedChangesDialogViewModel : DialogViewModel
    {
        private ICollection<WorkspaceViewModel> _documents;
        public ICollection<WorkspaceViewModel> Documents
        {
            get
            {
                return _documents;
            }
            private set
            {
                if(_documents != value)
                {
                    _documents = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand YesCommand { get; }
        public ICommand NoCommand { get; }
        public ICommand CancelCommand { get; }

        public CustomDialogResult Result { get; private set; }

        public UnsavedChangesDialogViewModel()
        {
            Documents      = new ObservableCollection<WorkspaceViewModel>();
            YesCommand     = new RelayCommand(Yes);
            NoCommand      = new RelayCommand(No);
            CancelCommand  = new RelayCommand(Cancel);
        }

        private void Yes()
        {
            Result = CustomDialogResult.Yes;
            InvokeDialogCloseRequest(true);
        }

        private void No()
        {
            Result = CustomDialogResult.No;
            InvokeDialogCloseRequest(true);
        }

        private void Cancel()
        {
            Result = CustomDialogResult.Cancel;
            InvokeDialogCloseRequest(false);
        }
    }
}
