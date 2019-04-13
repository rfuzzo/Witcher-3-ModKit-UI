using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using w3tools.App.Commands;
using w3tools.Services;

namespace w3tools.App.ViewModels.Dialogs
{
    public class SettingsDialogViewModel : DialogViewModel
    {

        public SettingsDialogViewModel( IConfigService configService)
        {
            ConfigService = configService;

            OKCommand = new RelayCommand(Save, CanSave);
            CancelCommand = new RelayCommand(Cancel);

            Register();
        }


        public void Register()
        {
            if (!(ConfigService is null))
            {
                _WCC_Path = ConfigService.GetConfigSetting("WCC_Path");
                _RAD_Path = ConfigService.GetConfigSetting("RAD_Path");
                _TW3_Path = ConfigService.GetConfigSetting("TW3_Path");
            }
        }

        #region Services
        private IConfigService ConfigService { get; }
        #endregion

        #region Properties
        private string _WCC_Path;
        public string WCC_Path
        {
            get
            {
                return _WCC_Path;
            }
            set
            {
                if (_WCC_Path != value)
                {
                    _WCC_Path = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _RAD_Path;
        public string RAD_Path
        {
            get
            {
                return _RAD_Path;
            }
            set
            {
                if (_RAD_Path != value)
                {
                    _RAD_Path = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _TW3_Path;
        public string TW3_Path
        {
            get
            {
                return _TW3_Path;
            }
            set
            {
                if (_TW3_Path != value)
                {
                    _TW3_Path = value;
                    OnPropertyChanged();
                }
            }
        }


        #endregion

        #region Commands
        public ICommand OKCommand { get; }
        public ICommand CancelCommand { get; }

       

        private void Save()
        {
            ConfigService.SetConfigSetting("WCC_Path", WCC_Path);
            ConfigService.SetConfigSetting("TW3_Path", TW3_Path);
            ConfigService.SetConfigSetting("RAD_Path", RAD_Path);

            ConfigService.Save();

            InvokeDialogCloseRequest(true);
        }

        private bool CanSave()
        {
            //check for empty strings?
            return true;
        }

        private void Cancel()
        {
            InvokeDialogCloseRequest(false);
        }
        #endregion
    }
}
