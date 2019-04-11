/// <summary>
/// Copyright
/// Limez3ro Viperkit
/// </summary>

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFFolderBrowser;

namespace w3tools.UI.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for NewProjectDialogView.xaml
    /// </summary>
    public partial class SettingsDialogView : UserControl
    {
        public SettingsDialogView()
        {
            InitializeComponent();
        }



        // In this situation it's just far easier to call the folder dialog from here.
        // The folder browser dialog does not use a viewmodel, and the parent can be set here as well.
        private void BrowseWCC_Click(object sender, RoutedEventArgs e)
        {
            var fd = new OpenFileDialog
            {
                Title = "Select wcc_lite.exe.",
                FileName = _WCC_TextBox.Text,
                Filter = "wcc_lite.exe|wcc_lite.exe"
            };
            if (fd.ShowDialog() == true && fd.CheckFileExists)
            {
                _WCC_TextBox.Text = fd.FileName;
            }
        }

        private void BrowseRAD_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new WPFFolderBrowserDialog("Select Radish Tools Folder Location");

            var dir = _RAD_TextBox.Text;
            if (Directory.Exists(dir))
            {
                dialog.InitialDirectory = dir;
            }

            var window = Window.GetWindow(this);
            var result = dialog.ShowDialog(window);
            if (result == true)
            {
                _RAD_TextBox.Text = dialog.FileName;
            }
        }

        private void BrowseTW3_Click(object sender, RoutedEventArgs e)
        {
            var fd = new OpenFileDialog
            {
                Title = "Select witcher3.exe.",
                FileName = _TW3_TextBox.Text,
                Filter = "witcher3.exe|witcher3.exe"
            };
            if (fd.ShowDialog() == true && fd.CheckFileExists)
            {
                _TW3_TextBox.Text = fd.FileName;
            }
        }
    }
}