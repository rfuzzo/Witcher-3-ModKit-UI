using System;
using System.Collections.Generic;
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
using w3tools.common;
using wcc.core;

namespace w3tools.UI.Templates
{
    /// <summary>
    /// Interaction logic for DTWorkspaceCommand.xaml
    /// </summary>
    public partial class DTWorkspaceCommand : UserControl
    {
        const string ExpandedPackUri = "pack://application:,,,/w3tools.UI;component/Resources/Icons/CollapseChevronDown_bold_blueNoHalo_16x.png";
        const string CollapsedPackUri = "pack://application:,,,/w3tools.UI;component/Resources/Icons/CollapseChevronRight_bold_blueNoHalo_16x.png";

        public DTWorkspaceCommand()
        {
            InitializeComponent();


            
        }

        private void Toggle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //dbg




            //if (_propertyGridMain.Height == _propertyGridMain.MinHeight) // expand
            if (_propertyGridMain.Visibility == Visibility.Collapsed) // expand
            {
                //_propertyGridMain.Height = Double.NaN;
                _propertyGridMain.Visibility = Visibility.Visible;
                _expandIcon.Source = new ImageSourceConverter().ConvertFromString(ExpandedPackUri) as ImageSource;
            }
            else // collapse
            {
                //_propertyGridMain.Height = _propertyGridMain.MinHeight;
                _propertyGridMain.Visibility = Visibility.Collapsed;
                _expandIcon.Source = new ImageSourceConverter().ConvertFromString(CollapsedPackUri) as ImageSource;
            }
        }
        private void Toggle_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Image)sender).Opacity = 0.7;
        }
        private void Toggle_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Image)sender).Opacity = 1;
        }



        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dynamic view = ((Image)sender).DataContext;
            dynamic viewModel = view.DataContext;
            WorkflowItem item = (WorkflowItem)DataContext;
            if (item != null && viewModel != null && view != null)
            {
                viewModel.DeleteWorkflowItem(item);
            }
        }
        private void Close_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Image)sender).Opacity = 0.7;
        }
        private void Close_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Image)sender).Opacity = 1;
        }
    }
}
