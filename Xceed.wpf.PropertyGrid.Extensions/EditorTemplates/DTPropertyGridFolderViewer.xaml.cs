using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using WPFFolderBrowser;
using System.IO;

namespace Xceed.wpf.PropertyGrid.Extensions.EditorTemplates
{
    /// <summary>
    /// Interaction logic for PropertyGridFolderPicker.xaml
    /// </summary>
    public partial class PropertyGridFolderPicker : UserControl, ITypeEditor
    {
        public PropertyGridFolderPicker()
        {
            InitializeComponent();
        }

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        
        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(PropertyGridFolderPicker), new PropertyMetadata(null));


            
        public FrameworkElement ResolveEditor(PropertyItem propertyItem)
        {
            Binding binding = new Binding("Value");
            binding.Source = propertyItem;
            binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            BindingOperations.SetBinding(this, ValueProperty, binding);
            return this;
        }

        private void PickFolderButton_Click(object sender, RoutedEventArgs e)
        {
            WPFFolderBrowserDialog fd = new WPFFolderBrowserDialog();
            if (fd.ShowDialog() == true && Directory.Exists(fd.FileName))
            {
                Value = fd.FileName;
            }
        }
    }
}
