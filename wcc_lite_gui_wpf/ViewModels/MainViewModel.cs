using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using wcc_lite_gui_wpf.Commands;
using Wcc_lite_core;
using System.ComponentModel;
using System.Threading;
using Microsoft.Win32;
using Ninject;
using Ninject.Infrastructure;
using Radish_core;

namespace wcc_lite_gui_wpf.ViewModels
{
    

    /// <summary>
    /// Represents the currently open workspace and project information.
    /// </summary>
    public class MainViewModel : ViewModel, IHaveKernel
    {
        #region Documents / Content / Anchorables
        private object _activeContent;
        /// <summary>
        /// Holds the currently active content in the window.
        /// </summary>
        public object ActiveContent
        {
            get
            {
                return _activeContent;
            }
            set
            {
                if (_activeContent != value)
                {
                    _activeContent = value;
                    InvokePropertyChanged();
                }
            }
        }

        private ICollection<WorkspaceViewModel> _documentsSource;
        /// <summary>
        /// Holds all the currently open documents in the project.
        /// </summary>
        public ICollection<WorkspaceViewModel> DocumentsSource
        {
            get
            {
                return _documentsSource;
            }
            set
            {
                if (_documentsSource != value)
                {
                    _documentsSource = value;
                    InvokePropertyChanged();
                }
            }
        }

        private object _activeDocument;
        /// <summary>
        /// Holds the currently active content in the window.
        /// </summary>
        public object ActiveDocument
        {
            get
            {
                return _activeDocument;
            }
            set
            {
                if (_activeDocument != value)
                {
                    _activeDocument = value;
                    InvokePropertyChanged();
                }
            }
        }

        private ICollection<DockableViewModel> _anchorablesSource;
        /// <summary>
        /// Holds the anchorable panes for controls.
        /// </summary>
        public ICollection<DockableViewModel> AnchorablesSource
        {
            get
            {
                return _anchorablesSource;
            }
            set
            {
                if (_anchorablesSource != value)
                {
                    _anchorablesSource = value;
                    InvokePropertyChanged();
                }
            }
        }
        #endregion

        #region Properties
        private object _activeProperty;
        /// <summary>
        /// Holds the currently active Wcc Lite Command in the window.
        /// </summary>
        public object ActiveProperty
        {
            get
            {
                return _activeProperty;
            }
            set
            {
                if (_activeProperty != value)
                {
                    _activeProperty = value;
                    InvokePropertyChanged();
                }
            }
        }

        private WccExtendedLogger _logger;
        /// <summary>
        /// Holds the Logger Class from the Wcc Task Handler.
        /// </summary>
        public WccExtendedLogger Logger
        {
            get
            {
                return _logger;
            }
            set
            {
                if (_logger != value)
                {
                    _logger = value;
                    InvokePropertyChanged();
                }
            }
        }

        private ObservableCollection<WorkflowItem> _commands;
        /// <summary>
        /// Holds the Wcc CommandsCollection stored in the Settings.
        /// </summary>
        public ObservableCollection<WorkflowItem> Commands
        {
            get
            {
                return _commands;
            }
            set
            {
                if (_commands != value)
                {
                    _commands = value;
                    InvokePropertyChanged();
                }
            }
        }

        private ObservableCollection<Radish_Workflow> _workflows;
        /// <summary>
        /// Holds the Wcc Workflow collection
        /// </summary>
        public ObservableCollection<Radish_Workflow> Workflows
        {
            get
            {
                return _workflows;
            }
            set
            {
                if (_workflows != value)
                {
                    _workflows = value;
                    InvokePropertyChanged();
                }
            }
        }

        



        private IViewModel _utilities;
        public IViewModel Utilities
        {
            get
            {
                return _utilities;
            }
            set
            {
                if (_utilities != value)
                {
                    _utilities = value;
                    InvokePropertyChanged();
                }
            }
        }

        public WccTaskHandler WccTaskHandler { get; set; }
        #endregion

        #region Commands
        public ICommand ExitCommand { get; }
        public ICommand RunWccCmdCommand { get; }
        public ICommand SaveFileCommand { get; }
        public ICommand LocateWccCommand { get; }
       


        #region Command Implementation
        private void Exit()
        {
            Kernel.Get<MainWindow>().Close();
        }

        public bool CanRun()
        {
            WorkspaceViewModel wvm = DocumentsSource.FirstOrDefault(x => x.ContentId == "workspace");
            return wvm.Workflow.Any();
        }
        public void Run()
        {
            WorkspaceViewModel wvm = DocumentsSource.FirstOrDefault(x => x.ContentId == "workspace");
            var workflow = wvm.Workflow;
            foreach (WccCommand cmd in workflow)
            {
                StartWccComand(cmd);
            }

        }
        public async void StartWccComand(WccCommand cmd)
        {
            await WccTaskHandler.RunCommand(cmd);
        }

        public bool CanSave()
        {
            return true;
        }
        public void Save()
        {
            wcc_lite_gui_wpf.Properties.Settings.Default.Save();
        }
        public bool CanLocateWcc()
        {
            return true;
        }
        public void LocateWcc()
        {
            var fd = new OpenFileDialog
            {
                Title = "Select wcc_lite.exe.",
                FileName = wcc_lite_gui_wpf.Properties.Settings.Default.WccPath,
                Filter = "wcc_lite.exe|wcc_lite.exe"
            };
            if (fd.ShowDialog() == true && fd.CheckFileExists)
            {
                Properties.Settings.Default.WccPath = fd.FileName;
            }
        }

        




        #endregion
        #endregion

        public IKernel Kernel { get; }

        public MainViewModel(IKernel kernel)
        {
            Kernel = kernel;

            #region ViewModels
            Utilities = kernel.Get<UtilitiesViewModel>();

            #endregion

            #region Relay Commands
            ExitCommand = new RelayCommand(Exit);
            RunWccCmdCommand = new RelayCommand(Run, CanRun);
            SaveFileCommand = new RelayCommand(Save, CanSave);
            LocateWccCommand = new RelayCommand(LocateWcc, CanLocateWcc);

            #endregion

            // Layout
            AnchorablesSource = new ObservableCollection<DockableViewModel>()
            {
                new CommandsListViewModel()
                {
                    Title = "Commands List",
                    ContentId = "commandsList",
                    ParentViewModel = this,
                },
                new WorkflowListViewModel()
                {
                    Title = "Workflow List",
                    ContentId = "workflowList",
                    ParentViewModel = this,
                },
                new ErrorListViewModel()
                {
                    Title = "Error List",
                    ContentId = "errorList",
                    ParentViewModel = this,
                },
                new LogViewModel()
                {
                    Title = "Log",
                    ContentId = "log",
                    ParentViewModel = this,
                },
                new PropertiesViewModel()
                {
                    Title = "Properties",
                    ContentId = "properties",
                    ParentViewModel = this,
                },
                new VariablesViewModel()
                {
                    Title = "Variables",
                    ContentId = "variables",
                    ParentViewModel = this,
                },
            };
            DocumentsSource = new ObservableCollection<WorkspaceViewModel>
            {
                new WorkspaceViewModel()
                {
                    Title = "Workspace",
                    ContentId = "workspace",
                    ParentViewModel = this,
                },
                

            };
            

            // core logic
            WccTaskHandler = new WccTaskHandler(Properties.Settings.Default.WccPath);
            Commands = Properties.Settings.Default.WccLite_Commands;
            Workflows = new RadishWorkflowCollection();

            Logger = WccTaskHandler.Logger;

            
        }

       
    }
}