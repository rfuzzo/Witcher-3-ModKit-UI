using System;
using System.Windows;
using Microsoft.Win32;
using Ninject;
using Ninject.Parameters;
using w3tools.App.Services;
using w3tools.App.ViewModels;
using w3tools.App.ViewModels.Dialogs;
using WPFFolderBrowser;

namespace w3tools.UI.Services
{
    using Ninject.Syntax;
    using Views.Dialogs;

    // TODO: The dialog service will need to have it's parent window instance in order for dialogs to have a parent window.
    // the owner window will need to be whatever view is associated with the view model the dialog service is injected into.
    // e.g: The MainViewModel has a dialog service dependency, which will mean that the window that needs to set in the 
    // ViewDialogService injected into MianViewModel will need to be MainView.
    // That way calling dialogs from MainViewModel will ensure that mainview is their parent.
    // without mainviewmodel directly knowing about a window.

    /// <summary>
    /// The UI implementation of the dialog service.
    /// This will display dialog windows to the user.
    /// </summary>
    public class ViewDialogService : IDialogService
    {
        /// <summary>
        /// The window owner of the dialog service.
        /// </summary>
        public Window Owner { get; }

        /// <summary>
        /// Initialise a new instance of the ViewDialogService.
        /// </summary>
        /// <param name="owner">The parent window that all dialog views will be the child of.</param>
        public ViewDialogService(Window owner)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner), "The dialog service owner window cannot be null.");
        }

        [Inject]
        public ViewDialogService()
        {
            Owner = Application.Current.MainWindow;
        }

        /// <summary>
        /// Display a custom modal dialog box to the user.
        /// </summary>
        /// <param name="viewModel">The viewmodel for the dialog.</param>
        /// <returns></returns>
        public bool? ShowDialog(IDialogViewModel viewModel)
        {
            switch(viewModel)
            {
                case OpenFileDialogViewModel openVm:
                    var openFileDialog = new OpenFileDialog()
                    {
                        Title = openVm.Title,
                        Filter = openVm.Filter,
                        Multiselect = openVm.MultiSelect,
                        InitialDirectory = openVm.InitialDirectory,
                    };
                    var openFileResult = openFileDialog.ShowDialog(Owner);
                    if(openFileResult == true)
                    {
                        openVm.FilePaths = openFileDialog.FileNames;
                    }
                    return openFileResult;

                case SaveFileDialogViewModel saveVm:
                    var saveFileDialog = new SaveFileDialog()
                    {
                        Title = saveVm.Title,
                        Filter = saveVm.Filter,
                        InitialDirectory = saveVm.InitialDirectory,
                    };
                    var saveFileResult = saveFileDialog.ShowDialog(Owner);
                    if(saveFileResult == true)
                    {
                        saveVm.FilePath = saveFileDialog.FileName;
                    }
                    return saveFileResult;

                case OpenFolderDialogViewModel folderVm:
                    var openFolderDialog = new WPFFolderBrowserDialog()
                    {
                        Title = folderVm.Title,
                        InitialDirectory = folderVm.InitialDirectory,
                    };
                    var opneFolderResult = openFolderDialog.ShowDialog(Owner);
                    if(opneFolderResult == true)
                    {
                        folderVm.FolderPath = openFolderDialog.FileName;
                    }
                    return opneFolderResult;

                default:
                    var view = new DialogView(viewModel);
                    var dResult = view.ShowDialog(Owner);
                    return dResult;
            }
        }

        /// <summary>
        /// Resolve and instance of a dialog view model.
        /// </summary>
        /// <typeparam name="T">The type of IDialogViewModel to resolve.</typeparam>
        /// <returns>A dialog viewmodel instance.</returns>
        public T GetDialog<T>() where T : IDialogViewModel
        {
            throw new NotImplementedException();
        }

        #region Win32
        private T CreateWin32Dialog<T>(IDialogViewModel viewModel) where T : CommonDialog, new()
        {
            return new T();
        }
        #endregion
    }
}