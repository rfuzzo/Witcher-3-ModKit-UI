/// <summary>
/// Copyright
/// Limez3ro Viperkit
/// </summary>


using System;

namespace w3tools.App.ViewModels.Dialogs
{
    public class DialogCloseRequestEventArgs : EventArgs
    {
        public bool? DialogResult { get; }

        public DialogCloseRequestEventArgs(bool? result)
        {
            this.DialogResult = result;
        }
    }
}
