using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using wcc.core;
using w3tools.App.Commands;
using radish.core;
using w3tools;
using w3tools.common;
using wcc.core.Commands;
using w3tools.Workflows;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using w3tools.App.ViewModels.Dialogs;
using w3tools.App.Services;
using w3tools.Services;

namespace w3tools.App.ViewModels
{
    /// <summary>
    /// SUMMARY: viewmodel for wcc lite workflows
    /// TODO serialize as xmls
    /// 
    /// </summary>
    public class DocumentViewModel : DockableViewModel/*, IDropTarget*/
    {

        public DocumentViewModel(IDialogService dialogService, IConfigService configService)
        {
            DialogService = dialogService;
            ConfigService = configService as ConfigService;

            CloseCommand = new RelayCommand(Close);
            SaveCommand = new RelayCommand(Save);
            SaveAsCommand = new RelayCommand(SaveAs);

            Workflow = new ObservableCollection<IWorkflowItem>();
        }

        #region Services
        public IConfigService ConfigService { get; }
        public IDialogService DialogService { get; }
        #endregion

        #region Properties
        private WF_Settings _settings;
        public WF_Settings Settings
        {
            get => _settings;
            set
            {
                if (_settings != value)
                {
                    _settings = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _hasUnsavedChanges;
        public bool HasUnsavedChanges
        {
            get => _hasUnsavedChanges;
            set
            {
                if (_hasUnsavedChanges != value)
                {
                    _hasUnsavedChanges = value;
                    OnPropertyChanged();
                }
            }
        }


        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();

                    ParentViewModel.ActiveDocument = ParentViewModel.DocumentsSource.FirstOrDefault(x => x.IsSelected);

                }
            }
        }

        private string _documentpath;
        public string DocumentPath
        {
            get => _documentpath;
            set
            {
                if (_documentpath != value)
                {
                    _documentpath = value;
                    OnPropertyChanged();
                }
            }
        }


        private ObservableCollection<IWorkflowItem> _workflow;
        /// <summary>
        /// Holds variables usable in workflows
        /// </summary>
        public ObservableCollection<IWorkflowItem> Workflow
        {
            get => _workflow;
            set
            {
                if (_workflow != value)
                {
                    _workflow = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Commands

        public ICommand CloseCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SaveAsCommand { get; }

        #region Command Implementation
       

        private void Close()
        {

        }
        private void Save()
        {
            if (DocumentPath != null && File.Exists(DocumentPath))
            {
                SaveToXml(DocumentPath);
            }
            else
            {
                SaveAs();
            }
        }
        private void SaveAs()
        {
            var dialog = new SaveFileDialogViewModel()
            {
                Owner = this,
                Title = "Save File As",
                Filter = "W3Tools Workflow (*.rwx)|*.rwx",
                FilePath = DocumentPath
            };
            var result = DialogService.ShowDialog(dialog);
            if (result == true)
            {
                DocumentPath = dialog.FilePath;
                SaveToXml(DocumentPath);
            }
        }


        #endregion
        #endregion

        #region Methods
        public void DeleteWorkflowItem(IWorkflowItem item)
        {
            _workflow.Remove(item);
        }

        public void AddWorkflowItems(List<IWorkflowItem> items)
        {
            // Optionally add Commands
            if (!(items is null))
            {
                foreach (IWorkflowItem item in items)
                {
                    IWorkflowItem emptyCopy = (IWorkflowItem)Activator.CreateInstance(item.GetType());
                    emptyCopy.CustomTag = Settings;
                    Workflow.Add(emptyCopy);
                }
            }
        }

        #endregion



        #region Drag and Drop
        //FIXME add more checks when I add more droppable lists.
        public void DragOver(IDropInfo dropInfo)
        {
            object sourceItem = dropInfo.Data;
            var targetItem = dropInfo.TargetItem;
            IDragInfo dragInfo = dropInfo.DragInfo;
            var sourceCollection = dragInfo.SourceCollection;
            var targetCollection = dropInfo.TargetCollection;

            if (sourceItem != null)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Copy;
                if (targetCollection == sourceCollection) // drag and drop within one list
                    dropInfo.Effects = DragDropEffects.Move;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            IDragInfo dragInfo = dropInfo.DragInfo;
            object sourceItem = dropInfo.Data;
            object targetItem = dropInfo.TargetItem;
            

            if (dropInfo.Effects == DragDropEffects.Move) //if drag and drop *within* the list
            {
                var sourceCollection = dropInfo.DragInfo.SourceCollection.Cast<IWorkflowItem>().ToArray(); // UNSAFE if not castable to list
                var insertIndex = dropInfo.InsertIndex;
                var sourceIndex = dropInfo.DragInfo.SourceIndex;
                insertIndex = Math.Min(insertIndex, sourceCollection.Length - 1);

                Workflow.Move(sourceIndex, insertIndex);
            }
            else //handle all dropping *into* the list
            {
                // handle dropping of commands
                if (sourceItem.GetType().BaseType == typeof(IWorkflowItem))
                {
                    IWorkflowItem emptyCopy = (IWorkflowItem)Activator.CreateInstance(sourceItem.GetType());
                    Workflow.Add(emptyCopy);
                }
                // handle dropping of variables 
                else if (sourceItem.GetType() == typeof(WccUIVariable) && targetItem.GetType() == typeof(string)) 
                {
                    
                }    
            }
        }

        /// <summary>
        /// FIXME
        /// </summary>
        /// <param name="file"></param>
        private void SaveToXml(string file)
        {
            List<string> workflownames = new List<string>();
            foreach (var item in Workflow)
            {
                workflownames.Add(item.GetType().Name);
            }

            var data = new RwxData(Title, Settings.ToXElement(), workflownames);

            data.Serialize(file);
        }

       

        #endregion
    }
}
