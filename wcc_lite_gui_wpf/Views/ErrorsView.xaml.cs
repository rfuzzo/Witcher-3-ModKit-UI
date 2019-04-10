using System.Windows.Controls;
using System.Windows.Data;
using w3tools.common;

namespace w3tools.UI.Views
{
    /// <summary>
    /// Interaction logic for UCErrors.xaml
    /// </summary>
    public partial class ErrorsView : UserControl
    {
        public ErrorsView()
        {
            InitializeComponent();

        }




        private void FilterForErrors(object sender, FilterEventArgs e)
        {
            WccLogFlag value = ((ExtendedWCCLogMessage)e.Item).WccFlag;
            if (value == WccLogFlag.WLF_Error || value == WccLogFlag.WLF_Warning)
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
