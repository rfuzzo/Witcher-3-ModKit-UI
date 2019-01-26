using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc_lite_gui_wpf.ViewModels
{
    public class CommandsViewModel : ObservableObject, IViewModel
    {
        private string _selectedCommand;
        public string SelectedCommand
        {
            get { return _selectedCommand; }
            set { ChangeProperty(ref _selectedCommand, value); }
        }

        public ObservableCollection<string> Commands { get; private set; }

        public CommandsViewModel()
        {

        }

        public void LoadCommands(IEnumerable<string> commands)
        {
            Commands = new ObservableCollection<string>(commands);
            InvokePropertyChanged("Commands");
        }
    }
}
