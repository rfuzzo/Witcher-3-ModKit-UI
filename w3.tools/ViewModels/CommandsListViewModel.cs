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
            AddCommand = new DelegateCommand<IWorkflowItem>(Add, CanAdd);
            ClearCommand = new RelayCommand(Clear);


            Toolbox = new ObservableCollection<IWorkflowItem>();
            
            foreach (var item in new WCCCommandsCollection())
                Toolbox.Add(item);
            foreach (var item in new RADCommandsCollection())
                Toolbox.Add(item);
            foreach (var item in new WF_WCC_CommandsCollection())
                Toolbox.Add(item);
            foreach (var item in new WF_RAD_CommandsCollection())
                Toolbox.Add(item);
            foreach (var item in new WF_WIN_CommandsCollection())
                Toolbox.Add(item);
                

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

        private ObservableCollection<IWorkflowItem> _toolbox;
        public ObservableCollection<IWorkflowItem> Toolbox
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
        public ICommand AddCommand { get; }
        public ICommand ClearCommand { get; }

        public bool CanAdd(IWorkflowItem sender)
        {
            return ParentViewModel.DocumentsSource.Any();
        }
        public void Add(IWorkflowItem sender)
        {
            CommandDoubleClick(sender);
        }
        public void Clear()
        {
            SearchText = "";
        }


        public void CommandDoubleClick(IWorkflowItem sender)
        {
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

                foreach (IWorkflowItem item in Toolbox)
                    MarkVisible(item, str);
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




}
