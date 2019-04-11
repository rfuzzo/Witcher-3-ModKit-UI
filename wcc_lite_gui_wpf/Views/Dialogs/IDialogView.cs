/// <summary>
/// Copyright
/// Limez3ro Viperkit
/// </summary>

using System.Windows;

namespace w3tools.UI.Views.Dialogs
{
    /// <summary>
    /// Represents a dialog view
    /// </summary>
    public interface IDialogView : IView
    {
        bool? DialogResult { get; set; }
        bool? ShowDialog();
        bool? ShowDialog(Window parent);
    }
}
