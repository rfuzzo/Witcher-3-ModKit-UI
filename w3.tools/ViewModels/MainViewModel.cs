﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Microsoft.Win32;
using System.Diagnostics;
using Ninject;
using Ninject.Infrastructure;
using w3tools.common;
using w3tools.Workflows;
using radish.core;
using radish.core.Commands;
using wcc.core;
using wcc.core.Commands;

namespace w3tools.App.ViewModels
{
    using Commands;
    using Services;
    using ViewModels.Dialogs;

    /// <summary>
    /// Represents the currently open workspace and project information.
    /// </summary>
    public class MainViewModel : ViewModel, IHaveKernel
    {
        public IKernel Kernel { get; }


        public MainViewModel(
            IKernel kernel, 
            IDialogService dialogService)
        {
            Kernel = kernel;
            DialogService = dialogService;

            Logger = new WccExtendedLogger();
            Commands = new WccCommandsCollection();
            Workflows = new RadishWorkflowCollection();


            #region ViewModels
            Utilities = kernel.Get<UtilitiesViewModel>();
            

            #endregion

            #region Relay Commands
            ExitCommand = new RelayCommand(Exit);

            RunCommand = new RelayCommand(Run, CanRun);
            SaveFileCommand = new RelayCommand(SaveFile, CanSaveFile);

            StartGameCommand = new RelayCommand(StartGame, CanStartGame);

            //DBG
            DEBUGCMD = new RelayCommand(DBG);
            DEBUGCMD2 = new RelayCommand(DBG2);
            //DBG

            #endregion

            #region Layout
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
                // FIXME on hold until I figure out how to drag and drop variables into a propertygrid
                /*new VariablesViewModel()
                {
                    Title = "Variables",
                    ContentId = "variables",
                    ParentViewModel = this,
                },*/
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
            #endregion

           
        }

       

        #region DEBUG
        //dbg
        public ICommand DEBUGCMD { get; }
        public ICommand DEBUGCMD2 { get; }
        //dbg
        private void DBG()
        {
            var s = Config;
            //s.Load();

            Logger.LogString($"--- IConfigProvider ---");
            Logger.LogString($"GamePath: {s.GetConfigSetting("TW3_Path")};");
            Logger.LogString($"ToolsPath: {s.GetConfigSetting("RAD_Path")};");
            Logger.LogString($"ModKitPath: {s.GetConfigSetting("WCC_Path")};");
        }
        private void DBG2()
        {

           
        }

        public event EventHandler ClosingRequest;
        protected void OnClosingRequest()
        {
            if (this.ClosingRequest != null)
            {
                this.ClosingRequest(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Services
        public IDialogService DialogService { get; }
        #endregion

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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }

        private object _activeDocument;
        /// <summary>
        /// Holds the currently active content in the window.
        /// </summary>
        /// //FIXME
        public object ActiveDocument
        {
            get
            {
                return _activeDocument;
                //var selectedDoc = DocumentsSource.FirstOrDefault(x => x.IsSelected);
                //return selectedDoc;
            }
            set
            {
                if (_activeDocument != value)
                {
                    _activeDocument = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Properties


        private object _activeProperty;
        /// <summary>
        /// Holds the currently active object to display in the properties window.
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<RAD_Workflow> _workflows;
        /// <summary>
        /// Holds the Wcc Workflow collection
        /// </summary>
        public ObservableCollection<RAD_Workflow> Workflows
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }

        public WccTaskHandler WccTaskHandler { get; set; }
        #endregion

        #region Commands
        public ICommand ExitCommand { get; }

        public ICommand RunCommand { get; }

        public ICommand SaveFileCommand { get; }

        public ICommand StartGameCommand { get; }





        #region Command Implementation



        private void Exit()
        {
            this.OnClosingRequest();
        }


        private bool CanStartGame()
        {
            return !String.IsNullOrEmpty(Config.GetConfigSetting("TW3_Path"));

        }
        private void StartGame()
        {
            Process.Start(Config.GetConfigSetting("TW3_Path"));
        }


        public bool CanRun()
        {
            // check if config is OK
            if (Config != null)
            {
                // if either variable is empty or not initialized
                if (String.IsNullOrEmpty(Config.GetConfigSetting("RAD_Path")) || String.IsNullOrEmpty(Config.GetConfigSetting("WCC_Path")))
                {
                    // attempt to load settinsg from xml
                    if (!Config.Load()) //either no config file exists or it's bugged or variables are bad
                    {
                        // open settings


                        return false;
                    }
                }
                // settings succesfully loaded
                //check if any commands in active workflow
                WorkspaceViewModel wvm = DocumentsSource.FirstOrDefault(x => x.IsSelected);
                return wvm != null && wvm.Workflow.Any();
            }
            else
            {
                return false;
            }
        }
        public void Run()
        {
            WorkspaceViewModel wvm = DocumentsSource.FirstOrDefault(x => x.IsSelected);
            var workflow = wvm.Workflow;


            Logger.ClearLog();
            Logger.LogString("Starting Workflow...");

            // FIXME structure?
            foreach (WorkflowItem item in workflow)
            {
                WFR completed = WFR.WFR_Error;

                if (item.GetType().IsSubclassOf(typeof(RAD_Command)))
                {
                    RAD_Task rth = new RAD_Task(Config.GetConfigSetting("RAD_Path"), Logger);
                    completed = rth.RunCommandSync((RAD_Command)item);
                }
                else if (item.GetType().IsSubclassOf(typeof(WCC_Command)))
                {
                    WCC_Task wth = new WCC_Task(Config.GetConfigSetting("WCC_Path"), Logger);
                    completed = wth.RunCommandSync((WCC_Command)item);
                }
                else //workflowitems
                {
                    completed = item.Run();
                }
                
                if (completed == WFR.WFR_Error)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }

            Logger.LogString("Done.");
        }


        public bool CanSaveFile()
        {
            return true;
        }
        public void SaveFile()
        {
            //w3tools.App.Properties.Settings.Default.Save();
        }

       



        #endregion
        #endregion


    }
}