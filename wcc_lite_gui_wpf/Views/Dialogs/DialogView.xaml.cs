/// <summary>
/// Copyright
/// Limez3ro Viperkit
/// </summary>

using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using w3tools.App.ViewModels.Dialogs;

namespace w3tools.UI.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogView : Window, IDialogView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogView"/> class.
        /// </summary>
        /// <param name="viewModel">The dialog viewmodel the set as the data context of the dialog view.</param>
        public DialogView(IDialogViewModel viewModel)
        {
            InitializeComponent();
            viewModel.CloseRequest += OnDialogCloseRequest;
            DataContext = viewModel;
        }

        /// <summary>
        /// Opens a window and returns only when the newly opened window is closed.
        /// </summary>
        /// <param name="parent">A <see cref="Window"/> instance that represents the owner of this <see cref="Window"/></param>
        /// <returns>A <see cref="Nullable"/> value of type <see cref="Boolean"/> that specifies whether the activity
        /// was accepted (true) or canceled (false). The return value is the value of the
        /// <see cref="Window.DialogResult"/> property before a window closes.</returns>
        public bool? ShowDialog(Window parent)
        {
            this.Owner = parent;
            return this.ShowDialog();
        }

        /// <summary>
        /// Called when the dialog is requested to close.
        /// </summary>
        /// <param name="sender">The <see cref="IDialogCloseRequest"/> instance that issued the request.</param>
        /// <param name="e">The dialog close arguments.</param>
        private void OnDialogCloseRequest(object sender, DialogCloseRequestEventArgs e)
        {
            this.DialogResult = e.DialogResult;
            this.Close();
        }
    }
}