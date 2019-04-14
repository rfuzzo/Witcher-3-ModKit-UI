using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using w3tools.common;

namespace w3tools.UI.Views
{
    /// <summary>
    /// Interaction logic for UCCommands.xaml
    /// </summary>
    public partial class CommandsView : UserControl
    {

        public CommandsView()
        {
            InitializeComponent();
        }

        //hack but fuck wpf seriously //FIXME
        // sends the parent TreeviewItem as well... ? 
        private void _commandsListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MenuItem_Click(sender, e);
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            dynamic viewModel = DataContext;
            var senderAsTreeViewItem = (TreeViewItem)sender;
            var senderDC = senderAsTreeViewItem.DataContext as WorkflowItem;
            if (senderDC != null)
                viewModel.CommandDoubleClick((WorkflowItem)senderDC);
        }

        //Selects the node when rightclicking
        void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem =
                      VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.IsSelected = true;
                e.Handled = true;
            }
        }
        static T VisualUpwardSearch<T>(DependencyObject source) where T : DependencyObject
        {
            DependencyObject returnVal = source;

            while (returnVal != null && !(returnVal is T))
            {
                DependencyObject tempReturnVal = null;
                if (returnVal is Visual || returnVal is Visual3D)
                {
                    tempReturnVal = VisualTreeHelper.GetParent(returnVal);
                }
                if (tempReturnVal == null)
                {
                    returnVal = LogicalTreeHelper.GetParent(returnVal);
                }
                else returnVal = tempReturnVal;
            }

            return returnVal as T;
        }

       
    }

}
