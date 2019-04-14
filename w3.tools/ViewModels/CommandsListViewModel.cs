using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using w3tools.App.Commands;
using w3tools.common;

namespace w3tools.App.ViewModels
{
    public class CommandsListViewModel : DockableViewModel
    {
        private const string SearchIcon = "/w3tools.UI;component/Resources/Icons/Search_16x.png";
        private const string ClearIcon = "/w3tools.UI;component/Resources/Icons/Close_White_16x.png";
        public CommandsListViewModel()
        {
            AddToWorkflowCommand = new DelegateCommand<IWorkflowItem>(AddToWorkflow);
            ClearCommand = new RelayCommand(Clear);


            Toolbox = new ObservableCollection<CommandCategory>();
            Toolbox.Add(new CommandCategory { Name = "WCC", Commands = new WCCCommandsCollection() });
            Toolbox.Add(new CommandCategory { Name = "RAD", Commands = new RADCommandsCollection() });
            Toolbox.Add(new CommandCategory { Name = "WFWCC", Commands = new WF_WCC_CommandsCollection() });
            Toolbox.Add(new CommandCategory { Name = "WFRAD", Commands = new WF_RAD_CommandsCollection() });
            Toolbox.Add(new CommandCategory { Name = "WFWIN", Commands = new WF_WIN_CommandsCollection() });

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

        private ObservableCollection<CommandCategory> _toolbox;
        public ObservableCollection<CommandCategory> Toolbox
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

 
        #endregion

        #region Commands
        public ICommand AddToWorkflowCommand { get; }
        public ICommand ClearCommand { get; }

        public bool CanAddToWorkflow()
        {
            return true;
        }
        public void AddToWorkflow(IWorkflowItem sender)
        {
            CommandDoubleClick(sender);
        }
        public void Clear()
        {
            SearchText = "";
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
                    OnPropertyChanged("SearchBoxImage");
                    // change treeview view
                    FilterTreeView(value);
                }
            }
        }
        private void FilterTreeView(string str)
        {
            foreach (CommandCategory cat in Toolbox)
            {
                foreach (IWorkflowItem item in cat.Commands)
                    MarkVisible(item, cat, str);
            }
        }
        private void MarkVisible(IWorkflowItem item, CommandCategory cat, string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                item.IsVisible = true;
            }
            else if (item.Name.ToLower().Contains(str.ToLower()))
            {
                item.IsVisible = true;
                cat.IsExpanded = true;
            }
            else
            {
                item.IsVisible = false;
            }
        }
        public string SearchBoxImage
        {
            get
            {
                if (String.IsNullOrEmpty(SearchText))
                    return SearchIcon;
                else
                    return ClearIcon;
            }
        }

        #endregion



    }


    /// <summary>
    /// 
    /// </summary>
    public class CommandCategory : ObservableObject, ITreeViewItem
    {
        public CommandCategory()
        {
            Commands = new ObservableCollection<IWorkflowItem>();
            IsExpanded = false;
            IsVisible = true;
        }

        public string Name { get; set; }
        public string Image { get; set; }
        public ITreeViewItem ParentTreeViewItem { get; set; }
        public ObservableCollection<IWorkflowItem> Commands { get; set; }


        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged();
                }
            }
        }

       
        public override string ToString() => Name;
    }
}
