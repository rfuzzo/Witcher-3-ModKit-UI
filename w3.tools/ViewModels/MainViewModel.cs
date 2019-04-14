using System;
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
    using System.Xml.Linq;
    using ViewModels.Dialogs;
    using w3tools.Services;

    /// <summary>
    /// Represents the currently open workspace and project information.
    /// </summary>
    public class MainViewModel : ViewModel, IHaveKernel
    {
        public IKernel Kernel { get; }


        public MainViewModel(IKernel kernel, IDialogService dialogService, ILoggerService logger, IConfigService configService)
        {
            Kernel = kernel;
            DialogService = dialogService;
            ConfigService = configService;
            Logger = logger;

            Commands = new WCCCommandsCollection();
            //Hier = new WFHierarchy();
            Workflows = new RadishWorkflowCollection();


            #region ViewModels
            Utilities = kernel.Get<UtilitiesViewModel>();
            

            #endregion

            #region Relay Commands

            ExitCommand = new RelayCommand(Exit);
            RunCommand = new RelayCommand(Run, CanRun);
            StartGameCommand = new RelayCommand(StartGame, CanStartGame);

            NewCommand = new RelayCommand(New);
            OpenCommand = new RelayCommand(Open);
            SaveAllCommand = new RelayCommand(SaveAll);


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
                new ErrorListViewModel(Kernel.Get<ILoggerService>())
                {
                    Title = "Error List",
                    ContentId = "errorList",
                    ParentViewModel = this,
                },
                new LogViewModel(Kernel.Get<ILoggerService>())
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
            DocumentsSource = new ObservableCollection<DocumentViewModel>
            {
                new DocumentViewModel(DialogService, ConfigService)
                {
                    Title = "Workspace",
                    ContentId = "workspace",
                    ParentViewModel = this,
                    Settings = new WF_Settings(ConfigService, Logger) //FIXME
                },


            };
            #endregion

            InitSettings();
        }

       



        #region DEBUG
        //dbg
        public ICommand DEBUGCMD { get; }
        public ICommand DEBUGCMD2 { get; }
        //dbg
        private void DBG()
        {
            var s = ConfigService;
            //s.Load();
            var t = s.GetConfigSetting("RAD_Path");

            Logger.LogString($"{DateTime.Now}");
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
        public IConfigService ConfigService { get; }
        public ILoggerService Logger { get; }
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

        private ICollection<DocumentViewModel> _documentsSource;
        /// <summary>
        /// Holds all the currently open documents in the project.
        /// </summary>
        public ICollection<DocumentViewModel> DocumentsSource
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

        public List<List<string>> TestObject { get; set; } = new List<List<string>>()
        {
            new List<string>() { "a", "b", "c" },
            new List<string>() { "a", "b", "c" },
            new List<string>() { "a", "b", "c" }
        };


        private ObservableCollection<IWorkflowItem> _commands;
        /// <summary>
        /// Holds the Wcc CommandsCollection stored in the Settings.
        /// </summary>
        public ObservableCollection<IWorkflowItem> Commands
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

        #endregion

        #region Commands
        public ICommand ExitCommand { get; }
        public ICommand RunCommand { get; }
        public ICommand StartGameCommand { get; }

        public ICommand NewCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand SaveAllCommand { get; }


        #region Command Implementation



        private void New()
        {
            AddDocument("untitled");
        }
        //FIXME
        private void Open()
        {
            var dialog = new OpenFileDialogViewModel
            {
                Title = "Open Workflow",
                Filter = "W3Tools Workflow (*.rwx)|*.rwx",
                Owner = this,
            };
            var result = DialogService.ShowDialog(dialog);
            if (result == true )
            {
                foreach (var file in dialog.FilePaths)
                {
                    //check if file exists?
                    if (!File.Exists(file))
                        continue;
                    //check if it is workflow file
                    // FIXME

                    var data = RwxData.Deserialize(file);
                    if (data is null)
                        continue;

                    WF_Settings settings = new WF_Settings(ConfigService, Logger); //FIXME
                    settings.FromXElement(data.Xsettings); //FIXMEEEE

                    var Workflow = new ObservableCollection<IWorkflowItem>();
                    foreach (var item in data.WorkflowItems)
                    {
                        //we have all commands stored in Commands, no need for assembly qualified names // FIXME
                        var cmd = Commands.FirstOrDefault(x => x.GetType().Name == item);
                        if (cmd is null)
                            continue;

                        IWorkflowItem emptyCopy = (IWorkflowItem)Activator.CreateInstance(cmd.GetType());
                        emptyCopy.Parent = settings;
                        Workflow.Add(emptyCopy);
                    }

                    var vm =  AddDocument(data.Title);
                    vm.Settings = settings;
                    vm.Workflow = Workflow;
                    vm.DocumentPath = file;

                }
            }
        }
        private void SaveAll()
        {
            throw new NotImplementedException();
        }



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
            //check if any commands in active workflow
            DocumentViewModel wvm = DocumentsSource.FirstOrDefault(x => x.IsSelected);
            return wvm != null && wvm.Workflow.Any();
        }
        public void Run()
        {
            DocumentViewModel wvm = DocumentsSource.FirstOrDefault(x => x.IsSelected);
            var workflow = wvm.Workflow;
            RAD_Task RAD_Task = Kernel.Get<RAD_Task>();
            WCC_Task WCC_Task = Kernel.Get<WCC_Task>();

            Logger.Clear();
            Logger.LogString("Starting Workflow...");

            // FIXME structure?
            foreach (IWorkflowItem item in workflow)
            {
                WFR completed = WFR.WFR_Error;

                if (item.GetType().IsSubclassOf(typeof(RAD_Command)))
                {
                    completed = RAD_Task.RunCommandSync((RAD_Command)item);
                }
                else if (item.GetType().IsSubclassOf(typeof(WCC_Command)))
                {
                    completed = WCC_Task.RunCommandSync((WCC_Command)item);
                }
                else //workflowitems
                {
                    
                    completed = item.Run();
                }

                if (completed == WFR.WFR_Error)
                {
                    Logger.LogString("Aborting.");
                    break;
                }
                else
                {
                    continue;
                }
            }

            Logger.LogString("Done.");
        }


        #endregion
        #endregion

        #region Methods

        private void InitSettings()
        {
            // check if config is OK
            if (ConfigService != null)
            {
                // if either variable is empty or not initialized
                if (String.IsNullOrEmpty(ConfigService.GetConfigSetting("RAD_Path")) || String.IsNullOrEmpty(ConfigService.GetConfigSetting("WCC_Path")))
                {
                    // attempt to load settinsg from xml
                    if (!ConfigService.Load()) //either no config file exists or it's bugged or variables are bad
                    {
                        // open settings

                    }
                }
                // settings succesfully loaded

            }
        }

        /// <summary>
        /// Adds a new Document. Optionally adds a Command or Workflow
        /// </summary>
        /// <param name="title"></param>
        public DocumentViewModel AddDocument(string title)
        {
            //FIXME handle better?
            string ContentId = "workspace_" + (this.DocumentsSource.Count + 1).ToString();

            DocumentViewModel document = new DocumentViewModel(DialogService, ConfigService)
            {
                Title = title,
                ContentId = ContentId,
                ParentViewModel = this,
                Settings = new WF_Settings(ConfigService,Logger) //FIXME
            };
            DocumentsSource.Add(document);

            //FIXME bugged?
            DocumentViewModel currentDoc = this.DocumentsSource.FirstOrDefault(x => x.ContentId == ContentId);

            return document;
        }




        #endregion
    }
}