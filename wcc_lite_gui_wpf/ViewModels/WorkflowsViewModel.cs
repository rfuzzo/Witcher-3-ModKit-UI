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
using w3tools.UI.Commands;

namespace w3tools.UI.ViewModels
{
    class WorkflowListViewModel : DockableViewModel
    {
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

        /// <summary>
        /// initializes a new viewmodel and document with a list of workflowitems
        /// </summary>
        public void CommandDoubleClick(RAD_Workflow sender)
        {
            string ContentId = "workspace_" + (ParentViewModel.DocumentsSource.Count + 1).ToString();

            //FIXME handle open new doc
            ParentViewModel.DocumentsSource.Add(new WorkspaceViewModel()
            {
                Title = sender.Name,
                ContentId = ContentId, 
                ParentViewModel = ParentViewModel,
                Settings = new RAD_Settings(Properties.Settings.Default.GamePath, Properties.Settings.Default.WccPath, Properties.Settings.Default.ToolsPath, ParentViewModel.Logger)
            });            
            WorkspaceViewModel currentDoc = ParentViewModel.DocumentsSource.FirstOrDefault(x => x.ContentId == ContentId);

            //add copies of all workflowitems from the radishWorkflow class to the workflow
            List<WorkflowItem> steps = sender.Steps;
            foreach (WorkflowItem item in steps)
            {
                WorkflowItem emptyCopy = (WorkflowItem)Activator.CreateInstance(item.GetType());
                emptyCopy.Parent = currentDoc.Settings;
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
