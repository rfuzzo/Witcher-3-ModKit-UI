using Radish_core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wcc_lite_core;
using wcc_lite_gui_wpf.Commands;

namespace wcc_lite_gui_wpf.ViewModels
{
    class WorkflowListViewModel : DockableViewModel
    {
        private Radish_Workflow _activeWorkflow;
        /// <summary>
        /// Holds variables usable in workflows
        /// </summary>
        public Radish_Workflow ActiveWorkflow
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
                    InvokePropertyChanged();
                }
            }
        }

        #region Commands
        public ICommand AddToFavouritesCommand { get; }
        public ICommand RemoveFromfavouritesCommand { get; }


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

        public void CommandDoubleClick(Radish_Workflow sender)
        {
            var docs = ParentViewModel.DocumentsSource;
            
            docs.Add(new WorkspaceViewModel()
            {
                Title = sender.Name,
                ContentId = "file1", 
                ParentViewModel = ParentViewModel,
            });

            //FIXME handle open docs
            //WorkspaceViewModel currentDoc = ParentViewModel.DocumentsSource.FirstOrDefault(x => x.ContentId == "file1");
            WorkspaceViewModel currentDoc = ParentViewModel.DocumentsSource.FirstOrDefault(x => x.IsSelected);

            //add copies of all workflowitems from the radishWorkflow class to the workflow
            List<WorkflowItem> steps = sender.Steps;
            foreach (WorkflowItem item in steps)
            {
                WorkflowItem emptyCopy = (WorkflowItem)Activator.CreateInstance(item.GetType());
                currentDoc.Workflow.Add(emptyCopy);
            }
        }
        #endregion


        public WorkflowListViewModel()
        {
            AddToFavouritesCommand = new RelayCommand(AddToFavourites, CanAddToFavourites);
            RemoveFromfavouritesCommand = new RelayCommand(RemoveFromfavourites, CanRemoveFromfavourites);
        }
    }
}
