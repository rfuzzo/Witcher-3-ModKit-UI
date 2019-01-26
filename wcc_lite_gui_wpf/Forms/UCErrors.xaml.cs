using System.Windows.Controls;
using System.Windows.Data;
using static Wcc_lite_core.WccExtendedLogger;

namespace wcc_lite_gui_wpf.Forms
{
    /// <summary>
    /// Interaction logic for UCErrors.xaml
    /// </summary>
    public partial class UCErrors : UserControl
    {
        public UCErrors()
        {
            InitializeComponent();

            Xceed.Wpf.DataGrid.ColumnWidth columnWidth = new Xceed.Wpf.DataGrid.ColumnWidth(200);
            //_extendedLogGrid.Columns[_extendedLogGrid.Columns.Count-1].Width = columnWidth;
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
    }
}
