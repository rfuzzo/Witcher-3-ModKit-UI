using Microsoft.Win32;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using w3tools.common;
using w3tools.App.Commands;
using WPFFolderBrowser;

namespace w3tools.App.ViewModels
{
    /// <summary>
    /// Duplicate App Settings class updating Properties.Settings
    /// </summary>
    public class AppSettings : ObservableObject
    {

        private string _ModKit;
        public string ModKit
        {
            get
            {
                return _ModKit;
            }
            set
            {
                if (_ModKit != value)
                {
                    _ModKit = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _Encoder;
        public string Encoder
        {
            get
            {
                return _Encoder;
            }
            set
            {
                if (_Encoder != value)
                {
                    _Encoder = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _Game;
        public string Game
        {
            get
            {
                return _Game;
            }
            set
            {
                if (_Game != value)
                {
                    _Game = value;
                    OnPropertyChanged();
                }
            }
        }


    }


    /// <summary>
    /// Settings Category class holding a Usercontrol
    /// </summary>
    public class SettingsCategory : ObservableObject
    {
        public SettingsCategory(string _name, Uri _uri)
        {
            Name = _name;
            Page = _uri;
        }

        public Uri Page { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }


    /// <summary>
    /// Common Viewmodel for Settings and About Views
    /// </summary>
    public class UtilitiesViewModel : ViewModel
    {
        public UtilitiesViewModel(IKernel kernel)
        {
            this.kernel = kernel;

            OpenGithubCommand = new RelayCommand(() => Process.Start(URLGithub));
            //SaveSettingsCommand = new RelayCommand(SaveSettings);
            //SaveSettingsCommand = new DelegateCommand<Window>(CloseWindow);

            OpenAboutCommand = new RelayCommand(OpenAbout);
            OpenSettingsCommand = new RelayCommand(OpenSettings);

            LocateWccCommand = new RelayCommand(LocateWcc);
            LocateEncodersCommand = new RelayCommand(LocateEncoder);
            LocateGameCommand = new RelayCommand(LocateGame);


            SettingsCategories = new ObservableCollection<SettingsCategory>();
            SettingsCategories.Add(new SettingsCategory( "Editor" ,new Uri("pack://application:,,,/Views/Pages/Settings_Editor.xaml")));
            SettingsCategories.Add(new SettingsCategory( "Test" ,new Uri("pack://application:,,,/Views/Pages/Settings_Editor.xaml")));

            //this.dialogService = dialogService;

            Settings = new AppSettings {
                ModKit = Properties.Settings.Default.WccPath,
                Encoder = Properties.Settings.Default.ToolsPath,
                Game = Properties.Settings.Default.GamePath
            };

        }

        #region Properties
        


        private IKernel kernel;
        public string URLGithub { get; }  = "https://github.com/rfuzzo/Witcher-3-ModKit-UI";
        public Version Version { get; set; }  = Assembly.GetExecutingAssembly().GetName().Version;

        /// <summary>
        /// Holds the selected Settinsg Category 
        /// </summary>
        private SettingsCategory _ActiveSetting;
        public SettingsCategory ActiveSetting
        {
            get
            {
                return _ActiveSetting;
            }
            set
            {
                if (_ActiveSetting != value)
                {
                    _ActiveSetting = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Holds all Settings Options
        /// </summary>
        private ObservableCollection<SettingsCategory> _SettingsCategories;
        public ObservableCollection<SettingsCategory> SettingsCategories
        {
            get
            {
                return _SettingsCategories;
            }
            set
            {
                if (_SettingsCategories != value)
                {
                    _SettingsCategories = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Holds the Editor Duplicate Settings Class
        /// </summary>
        private AppSettings _Settings;
        public AppSettings Settings
        {
            get
            {
                return _Settings;
            }
            set
            {
                if (_Settings != value)
                {
                    _Settings = value;
                    OnPropertyChanged();
                }
            }
        }

       

        #endregion

        #region Commands
        public ICommand OpenGithubCommand { get; }

        public ICommand OpenAboutCommand { get; }
        public ICommand OpenSettingsCommand { get; }

        public ICommand LocateWccCommand { get; }
        public ICommand LocateEncodersCommand { get; }
        public ICommand LocateGameCommand { get; }

        public ICommand SaveSettingsCommand { get; }




        #region Command Implementation
        public void OpenAbout()
        {
            //var about = kernel.Get<AboutView>();
            //about.DataContext = this;
            //about.ShowDialog();
        }


        //private readonly IDialogService dialogService;
        public void OpenSettings()
        {
            //DialogResult = null;

            //var settings = kernel.Get<SettingsView>();
            //settings.DataContext = this;
            //settings.ShowDialog();

            //ShowDialog();

            //FIXME
            ResetSettings();
        }
        /*private void ShowDialog()
        {
            var dialogViewModel = new SettingsViewModel();

            bool? success = dialogService.ShowDialog<SettingsView>(this, dialogViewModel);
            if (success == true)
            {
                
            }
        }*/
        /*private void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }*/

        private void ResetSettings()
        {
            Settings.ModKit = Properties.Settings.Default.WccPath;
            Settings.Encoder = Properties.Settings.Default.ToolsPath;
            Settings.Game = Properties.Settings.Default.GamePath;
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.WccPath = Settings.ModKit;
            Properties.Settings.Default.ToolsPath = Settings.Encoder;
            Properties.Settings.Default.GamePath = Settings.Game;

            Properties.Settings.Default.Save();

            
        }

        public void LocateWcc()
        {
            /*var fd = new OpenFileDialog
            {
                Title = "Select wcc_lite.exe.",
                FileName = Properties.Settings.Default.WccPath,
                Filter = "wcc_lite.exe|wcc_lite.exe"
            };
            if (fd.ShowDialog() == true && fd.CheckFileExists)
            {
                Properties.Settings.Default.WccPath = fd.FileName;
            }*/
        }
        public void LocateEncoder()
        {
            var fd = new WPFFolderBrowserDialog
            {
                Title = "Select Encoder Directory.",
                FileName = Properties.Settings.Default.ToolsPath
            };
            if (fd.ShowDialog() == true && Directory.Exists(fd.FileName))
            {
                Properties.Settings.Default.ToolsPath = fd.FileName;
            }
        }
        public void LocateGame()
        {
            /*var fd = new OpenFileDialog
            {
                Title = "Select witcher3.exe.",
                FileName = Properties.Settings.Default.GamePath,
                Filter = "witcher3.exe|witcher3.exe"
            };
            if (fd.ShowDialog() == true && fd.CheckFileExists)
            {
                Properties.Settings.Default.GamePath = fd.FileName;
            }*/
        }
        #endregion

        #endregion

        

       

        
    }
}
