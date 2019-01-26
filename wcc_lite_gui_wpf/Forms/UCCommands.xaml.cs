using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using wcc_lite_gui_wpf.ViewModels;
using Wcc_lite_core;
using Ninject;

namespace wcc_lite_gui_wpf.Forms
{
    /// <summary>
    /// Interaction logic for UCCommands.xaml
    /// </summary>
    public partial class UCCommands : UserControl
    {
       
        public UCCommands()
        {

            InitializeComponent();

           
        }

        private void _commandsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }

}
