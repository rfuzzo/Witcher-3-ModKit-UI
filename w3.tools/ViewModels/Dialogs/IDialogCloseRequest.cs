/// <summary>
/// Copyright
/// Limez3ro Viperkit
/// </summary>


using System;

namespace w3tools.App.ViewModels.Dialogs
{
    public interface IDialogCloseRequest
    {
        event EventHandler<DialogCloseRequestEventArgs> CloseRequest;
    }
}
