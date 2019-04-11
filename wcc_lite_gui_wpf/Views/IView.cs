/// <summary>
/// Copyright
/// Limez3ro Viperkit
/// </summary>

using System.Windows;

namespace w3tools.UI.Views
{
    /// <summary>
    /// Represents a view that can be shown to the user.
    /// </summary>
    public interface IView
    {
        string Title { get; set; }
        object DataContext { get; set; }
        bool IsActive { get; }
        void Show();
        void Close();
        Window Owner { get; set; }
    }
}