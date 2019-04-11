/// <summary>
/// Copyright
/// Limez3ro Viperkit
/// </summary>


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace w3tools.App.ViewModels.Dialogs
{
    using Commands;

    /// <summary>
    /// A generic message box dialog, with custom buttons and icon support.
    /// </summary>
    public class MessageDialogViewModel : DialogViewModel
    {
        private Uri _messageIcon;
        public Uri MessageIcon
        {
            get
            {
                return _messageIcon;
            }
            set
            {
                if(_messageIcon != value)
                {
                    _messageIcon = value;
                    OnPropertyChanged();
                }
            }
        }

        private ICollection<MessageDialogOptionViewModel> _options;
        public ICollection<MessageDialogOptionViewModel> Options
        {
            get
            {
                return _options;
            }
            private set
            {
                if(_options != value)
                {
                    _options = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SelectCommand { get; }

        public CustomDialogResult Result { get; private set; }

        public MessageDialogViewModel()
        {
            SelectCommand = new DelegateCommand<MessageDialogOptionViewModel>(Select);
            Options       = new ObservableCollection<MessageDialogOptionViewModel>();
        }

        /// <summary>
        /// Add a custom option to this dialog.
        /// </summary>
        /// <param name="title">The display string shown to the user.</param>
        /// <param name="result">The result of this option when selected.</param>
        public void AddOption(string title, CustomDialogResult result)
        {
            var option = new MessageDialogOptionViewModel(title, result);
            Options.Add(option);
        }

        /// <summary>
        /// Called by <see cref="SelectCommand"/> when an option is selected.
        /// </summary>
        /// <param name="obj">The dialog option viewmodel selected.</param>
        private void Select(MessageDialogOptionViewModel obj)
        {
            Result = obj.Result;
            InvokeDialogCloseRequest(true);
        }
    }

    public class MessageDialogOptionViewModel : ViewModel
    {
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if(_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        private CustomDialogResult _result;
        public CustomDialogResult Result
        {
            get
            {
                return _result;
            }
            set
            {
                if(_result != value)
                {
                    _result = value;
                    OnPropertyChanged();
                }
            }
        }

        public MessageDialogOptionViewModel(string title, CustomDialogResult result)
        {
            Title = title;
            Result = result;
        }
    }
}
