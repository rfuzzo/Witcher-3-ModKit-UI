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

namespace w3tools.UI.Views.UserControls
{
    /// <summary>
    /// Interaction logic for SearchBox.xaml
    /// </summary>
    public partial class SearchBox : UserControl
    {
        #region SearchButton
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(SearchBox),
                new UIPropertyMetadata(null));
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        #endregion
        #region SearchText
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(IEnumerable<string>),
                typeof(SearchBox),
                new UIPropertyMetadata(null));
        public IEnumerable<string> Text
        {
            get { return (IEnumerable<string>)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion
        public SearchBox()
        {
            InitializeComponent();
        }
    }
}
