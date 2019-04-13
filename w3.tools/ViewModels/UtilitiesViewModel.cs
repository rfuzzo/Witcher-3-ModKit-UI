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
using w3tools.App.ViewModels.Dialogs;
using w3tools.App.Services;

namespace w3tools.App.ViewModels
{
    /// <summary>
    /// Common Viewmodel for Settings and About Views
    /// </summary>
    public class UtilitiesViewModel : ViewModel
    {
        public UtilitiesViewModel( IDialogService dialogService)
        {
            DialogService = dialogService;

            OpenAboutCommand = new RelayCommand(OpenAbout);
            OpenSettingsCommand = new RelayCommand(OpenSettings);
        }

        #region Services
        private IDialogService DialogService { get; }
        #endregion


        #region Commands

        public ICommand OpenAboutCommand { get; }
        public ICommand OpenSettingsCommand { get; }


        #region Command Implementation
        public void OpenAbout()
        {
            var dialog = new AboutDialogViewModel()
            {
                Owner = this,
                Title = "About",
            };
            var result = DialogService.ShowDialog(dialog);
            if (result == true)
            {

            }
        }


        //private readonly IDialogService dialogService;
        public void OpenSettings()
        {
            var dialog = new SettingsDialogViewModel( Config)
            {
                Title = "Settings",
                Message = "Please Specify Tools Locations.",
            };
            var result = DialogService.ShowDialog(dialog);
            if (result == true)
            {

            }
        }
        #endregion
        #endregion
    }
}
