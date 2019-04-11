/// <summary>
/// Copyright
/// Limez3ro Viperkit
/// </summary>


namespace w3tools.App.ViewModels.Dialogs
{
    public interface IDialogViewModel : IViewModel, IDialogCloseRequest
    {
        string Title { get; set; }
        string Message { get; set; }
        IViewModel Owner { get; set; }
    }
}
