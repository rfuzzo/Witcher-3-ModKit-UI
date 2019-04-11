namespace w3tools.App.Services
{
    using ViewModels;
    using ViewModels.Dialogs;

    /// <summary>
    /// Defines a service to open dialogs.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Show a custom dialog.
        /// </summary>
        /// <param name="viewModel">The dialog viewmodel representing the dialog.</param>
        /// <returns>the result of the dialog when closed.</returns>
        bool? ShowDialog(IDialogViewModel viewModel);

        /// <summary>
        /// Resolve an instance of an IDIalogViewModel
        /// </summary>
        /// <typeparam name="T">The type of dialog view model to resolve.</typeparam>
        /// <returns>The dialog viewmodel instance.</returns>
        T GetDialog<T>() where T : IDialogViewModel;
    }
}