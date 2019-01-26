using System.Windows;
using wcc_lite_gui_wpf.ViewModels;

namespace wcc_lite_gui_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            //FIXME ??


        }
    }
}
