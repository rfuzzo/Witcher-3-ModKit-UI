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

        private WorkflowItem _activeCommand;
        /// <summary>
        /// Holds variables usable in workflows
        /// </summary>
        public WorkflowItem ActiveCommand
        {
            get
            {
                return _activeCommand;
            }
            set
            {
                if (_activeCommand != value)
                {
                    _activeCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        #region Commands
        public ICommand AddToFavouritesCommand { get; }
        public ICommand RemoveFromfavouritesCommand { get; }

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


        public CommandsListViewModel()
        {
            AddToFavouritesCommand = new RelayCommand(AddToFavourites, CanAddToFavourites);
            RemoveFromfavouritesCommand = new RelayCommand(RemoveFromfavourites, CanRemoveFromfavourites);

        }


       
    }
}
