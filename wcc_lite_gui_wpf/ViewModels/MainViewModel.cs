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
        #endregion


        #region Properties
        private WccLite_Command _activeCommand;
        /// <summary>
        /// Holds the currently active Wcc Lite Command in the window.
        /// </summary>
        public WccLite_Command ActiveCommand
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

        private ObservableCollection<WccLite_Command> _commands;
        /// <summary>
        /// Holds the Wcc CommandsCollection stored in the Settings.
        /// </summary>
        public ObservableCollection<WccLite_Command> Commands
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
        //FIXME in child View Model 
        public ICommand AddToFavouritesCommand { get; }
        public ICommand RemoveFromfavouritesCommand { get; }


        #region Command Implementation
        private void Exit()
        {
            Kernel.Get<MainWindow>().Close();
        }

        public bool CanRun()
        {
            return ActiveCommand != null;
        }
        public void Run()
        {
            StartWccComand(ActiveCommand);
        }
        public async void StartWccComand(WccLite_Command cmd)
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

        //FIXME in child View Model 
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

            //FIXME in child View Model 
            AddToFavouritesCommand = new RelayCommand(AddToFavourites, CanAddToFavourites);
            RemoveFromfavouritesCommand = new RelayCommand(RemoveFromfavourites, CanRemoveFromfavourites);
            #endregion


            WccTaskHandler = new WccTaskHandler(wcc_lite_gui_wpf.Properties.Settings.Default.WccPath);
            Commands = wcc_lite_gui_wpf.Properties.Settings.Default.WccLite_Commands;
            Logger = WccTaskHandler.Logger;


        }

       
    }
}