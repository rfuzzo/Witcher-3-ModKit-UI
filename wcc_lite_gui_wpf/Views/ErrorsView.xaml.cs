using System.Windows.Controls;
using System.Windows.Data;
using w3tools.Services;

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
            LogFlag value = ((WCCLogMessage)e.Item).Flag;
            if (value == LogFlag.WLF_Error || value == LogFlag.WLF_Warning)
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
