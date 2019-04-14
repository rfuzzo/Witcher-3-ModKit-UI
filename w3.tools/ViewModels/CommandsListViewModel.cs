using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using w3tools.common;
using wcc.core;
using wcc.core.Commands;
using w3tools.App.Commands;

namespace w3tools.App.ViewModels
{
    public class CommandsListViewModel : DockableViewModel
    {
        public CommandsListViewModel()
        {
            AddToWorkflowCommand = new DelegateCommand<IWorkflowItem>(AddToWorkflow);
            SearchCommand = new RelayCommand(Search);


            Toolbox = new List<CommandCategory>();
            Toolbox.Add(new CommandCategory { Name = "WCC", Commands = new WCCCommandsCollection().ToList() });
            Toolbox.Add(new CommandCategory { Name = "RAD", Commands = new RADCommandsCollection().ToList() });
            Toolbox.Add(new CommandCategory { Name = "WFWCC", Commands = new WF_WCC_CommandsCollection().ToList() });
            Toolbox.Add(new CommandCategory { Name = "WFRAD", Commands = new WF_RAD_CommandsCollection().ToList() });
            Toolbox.Add(new CommandCategory { Name = "WFWIN", Commands = new WF_WIN_CommandsCollection().ToList() });


        }

        #region Properties
        private IWorkflowItem _activeCommand;
        public IWorkflowItem ActiveCommand
        {
            get => _activeCommand;
            set
            {
                if (_activeCommand != value)
                {
                    _activeCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<CommandCategory> _toolbox;
        public List<CommandCategory> Toolbox
        {
            get => _toolbox;
            set
            {
                if (_toolbox != value)
                {
                    _toolbox = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    // change treeview view
                    FilterTreeView(value);
                }
            }
        }

        

        #endregion

        #region Commands
        public ICommand AddToWorkflowCommand { get; }
        public ICommand SearchCommand { get; }

        public bool CanAddToWorkflow()
        {
            return true;
        }
        public void AddToWorkflow(IWorkflowItem sender)
        {
            CommandDoubleClick(sender);
        }
        public void Search()
        {

        }


        public void CommandDoubleClick(IWorkflowItem sender)
        {
            //FIXME handle open docs
            DocumentViewModel currentDoc = ParentViewModel.DocumentsSource.FirstOrDefault(x => x.IsSelected);

            IWorkflowItem emptyCopy = (IWorkflowItem)Activator.CreateInstance(sender.GetType());
            currentDoc.Workflow.Add(emptyCopy);
        }
        #endregion

        #region Filtering
        private void FilterTreeView(string str)
        {
            foreach (CommandCategory cat in Toolbox)
            {
                foreach (IWorkflowItem item in cat.Commands)
                    MarkVisible(item, str);
            }
        }
        private void MarkVisible(IWorkflowItem item, string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                item.IsVisible = true;
            }
            else if (item.Name.ToLower().Contains(str.ToLower()))
            {
                item.IsVisible = true;
            }
            else
            {
                item.IsVisible = false;
            }
        }
        #endregion



    }

    public class CommandCategory
    {
        public CommandCategory() => Commands = new List<IWorkflowItem>();
        public string Name { get; set; }
        public string Image { get; set; }
        public List<IWorkflowItem> Commands { get; set; }

        public override string ToString() => Name;
    }
}
