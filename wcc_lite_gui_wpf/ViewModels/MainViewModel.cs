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

namespace wcc_lite_gui_wpf.ViewModels
{
    

    /// <summary>
    /// Represents the currently open workspace and project information.
    /// </summary>
    public class MainViewModel : ObservableObject, IViewModel
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

        private ICollection<DockabaleViewModel> _anchorablesSource;
        /// <summary>
        /// Holds the anchorable panes for controls.
        /// </summary>
        public ICollection<DockabaleViewModel> AnchorablesSource
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

        private WccLite_Command _activeCommand;
        /// <summary>
        /// Holds the currently active content in the window.
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


        

        private ObservableCollection<string> _rawLog;
        /// <summary>
        /// Holds the currently active content in the window.
        /// </summary>
        public ObservableCollection<string> RawLog
        {
            get
            {
                return _rawLog;
            }
            set
            {
                if (_rawLog != value)
                {
                    _rawLog = value;
                    InvokePropertyChanged();
                }
            }
        }

        #endregion

        #region Properties
        public WccTaskHandler WccTaskHandler { get; set; }



        #endregion

        #region Commands
        public ICommand RunWccCmdCommand { get; }
        public ICommand SaveFileCommand { get; }
        //FIXME in child View Model 
        public ICommand AddToFavouritesCommand { get; }
        public ICommand RemoveFromfavouritesCommand { get; }


        #region Command Implementation
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
            return ActiveCommand != null && ActiveCommand.Category != WccCommandCategory.Default;
        }
        public void RemoveFromfavourites()
        {
            ActiveCommand.Category = WccCommandCategory.Default;
        }




        #endregion
        #endregion

        /// <summary>
        /// lock for interthreadable bindings
        /// </summary>
        private static object _lock = new object();

        public MainViewModel()
        {

            #region Relay Commands
            RunWccCmdCommand = new RelayCommand(Run, CanRun);
            SaveFileCommand = new RelayCommand(Save, CanSave);
            //FIXME in child View Model 
            AddToFavouritesCommand = new RelayCommand(AddToFavourites, CanAddToFavourites);
            RemoveFromfavouritesCommand = new RelayCommand(RemoveFromfavourites, CanRemoveFromfavourites);


            #endregion


            WccTaskHandler = new WccTaskHandler(wcc_lite_gui_wpf.Properties.Settings.Default.WccPath);

        }

       







    }
}