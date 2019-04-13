using radish.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using w3tools;
using w3tools.common;
using wcc.core;
using w3tools.App.Commands;
using w3tools.Workflows;

namespace w3tools.App.ViewModels
{
    public class WorkflowListViewModel : DockableViewModel
    {
        public WorkflowListViewModel()
        {
            AddToFavouritesCommand = new RelayCommand(AddToFavourites, CanAddToFavourites);
            RemoveFromfavouritesCommand = new RelayCommand(RemoveFromfavourites, CanRemoveFromfavourites);
            AddWorkflowCommand = new DelegateCommand<RAD_Workflow>(AddWorkflow);
        }

        #region Properties
        private RAD_Workflow _activeWorkflow;
        /// <summary>
        /// 
        /// </summary>
        public RAD_Workflow ActiveWorkflow
        {
            get
            {
                return _activeWorkflow;
            }
            set
            {
                if (_activeWorkflow != value)
                {
                    _activeWorkflow = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion


        #region Commands
        public ICommand AddToFavouritesCommand { get; }
        public ICommand RemoveFromfavouritesCommand { get; }
        public ICommand AddWorkflowCommand { get; }





        #region Command Implementation
        public void AddWorkflow(RAD_Workflow sender)
        {
            DocumentViewModel wf = ParentViewModel.AddDocument(sender.Name);
            wf.AddWorkflowItems(sender.Steps);

        }

        public bool CanAddToFavourites()
        {
            return ActiveWorkflow != null && ActiveWorkflow.Category != WccCommandCategory.Favourites;
        }
        public void AddToFavourites()
        {
            ActiveWorkflow.Category = WccCommandCategory.Favourites;
        }
        public bool CanRemoveFromfavourites()
        {
            return ActiveWorkflow != null && ActiveWorkflow.Category == WccCommandCategory.Favourites;
        }
        public void RemoveFromfavourites()
        {
            ActiveWorkflow.ResetCategory();
        }
        #endregion
        #endregion

        /// <summary>
        /// initializes a new viewmodel and document with a list of workflowitems
        /// </summary>
        public void CommandDoubleClick(RAD_Workflow sender)
        {
           
        }


        

    }
}
