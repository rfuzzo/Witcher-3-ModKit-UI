/// <summary>
/// Copyright
/// Limez3ro Viperkit
/// </summary>



using System;
using System.Windows;
using w3tools.App.ViewModels;

namespace w3tools.App.ViewModels.Dialogs
{
    /// <summary>
    /// Represents a viewmodel used for custom dialogs.
    /// </summary>
    public abstract class DialogViewModel : ViewModel, IDialogViewModel
    {
        public event EventHandler<DialogCloseRequestEventArgs> CloseRequest;

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                if(_message != value)
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }

        public IViewModel Owner { get; set; }

        public DialogViewModel()
        {
            // Default title
            Title = "Radish Tools";
        }

        protected virtual void InvokeDialogCloseRequest(bool? dialogResult)
        {
            var closerequest = CloseRequest;
            closerequest.Invoke(this, new DialogCloseRequestEventArgs(dialogResult));
        }

        protected virtual void InvokeDialogCloseRequest(DialogCloseRequestEventArgs args)
        {
            var closerequest = CloseRequest;
            closerequest.Invoke(this, args);
        }
    }
}