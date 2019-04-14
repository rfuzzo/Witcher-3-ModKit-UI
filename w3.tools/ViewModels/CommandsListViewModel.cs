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
            AddToFavouritesCommand = new RelayCommand(AddToFavourites, CanAddToFavourites);
            AddToWorkflowCommand = new DelegateCommand<WorkflowItem>(AddToWorkflow);
            RemoveFromfavouritesCommand = new RelayCommand(RemoveFromfavourites, CanRemoveFromfavourites);

            Toolbox = new List<CommandCategory>();
            Toolbox.Add(new CommandCategory { Name = "WCC", Commands = new WCCCommandsCollection().ToList() });
            Toolbox.Add(new CommandCategory { Name = "RAD", Commands = new RADCommandsCollection().ToList() });
            Toolbox.Add(new CommandCategory { Name = "WF_WCC", Commands = new WF_WCC_CommandsCollection().ToList() });
            Toolbox.Add(new CommandCategory { Name = "WF_RAD", Commands = new WF_RAD_CommandsCollection().ToList() });
            Toolbox.Add(new CommandCategory { Name = "WF_WIN", Commands = new WF_WIN_CommandsCollection().ToList() });


        }

        #region Properties
        private WorkflowItem _activeCommand;
        /// <summary>
        /// Holds variables usable in workflows
        /// </summary>
        public WorkflowItem ActiveCommand
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
        #endregion

        #region Commands
        public ICommand AddToFavouritesCommand { get; }
        public ICommand AddToWorkflowCommand { get; }
        public ICommand RemoveFromfavouritesCommand { get; }

        public bool CanAddToWorkflow()
        {
            return true;
        }
        public void AddToWorkflow(WorkflowItem sender)
        {
            CommandDoubleClick(sender);
        }

        public bool CanAddToFavourites()
        {
            return ActiveCommand != null && ActiveCommand.Category != WccCommandCategory.Favourites;
        }
        public void AddToFavourites()
        {
            ActiveCommand.Category = WccCommandCategory.Favourites;
        }
        public bool CanRemoveFromfavourites()
        {
            return ActiveCommand != null && ActiveCommand.Category == WccCommandCategory.Favourites;
        }
        public void RemoveFromfavourites()
        {
            ActiveCommand.ResetCategory();
        }



        public void CommandDoubleClick(WorkflowItem sender)
        {
            //FIXME handle open docs
            DocumentViewModel currentDoc = ParentViewModel.DocumentsSource.FirstOrDefault(x => x.IsSelected);

            WorkflowItem emptyCopy = (WorkflowItem)Activator.CreateInstance(sender.GetType());
            currentDoc.Workflow.Add(emptyCopy);
        }
        #endregion

    }

    public class CommandCategory
    {
        public CommandCategory() => Commands = new List<WorkflowItem>();
        public string Name { get; set; }
        public string Image { get; set; }
        public List<WorkflowItem> Commands { get; set; }

        public override string ToString() => Name;
    }
}
