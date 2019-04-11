using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using w3tools.App.Commands;
using w3tools.App.Services;

namespace w3tools.App.ViewModels.Dialogs
{
    public class SettingsDialogViewModel : DialogViewModel
    {
        private ViewModel vm; //FIXME

        public SettingsDialogViewModel(IViewModel owner)
        {
            Owner = owner;
            vm = owner as ViewModel;

            OKCommand = new RelayCommand(Save, CanSave);
            CancelCommand = new RelayCommand(Cancel);

            Register();
        }


        public void Register()
        {
            if (!(vm.Config is null))
            {
                _WCC_Path = vm.Config.GetConfigSetting("WCC_Path");
                _RAD_Path = vm.Config.GetConfigSetting("RAD_Path");
                _TW3_Path = vm.Config.GetConfigSetting("TW3_Path");
            }
        }

        #region Services
        private IConfigProvider ConfigProvider { get; }



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

                    vm.Config.SetConfigSetting("WCC_Path", value);
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

                    vm.Config.SetConfigSetting("RAD_Path", value);
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

                    vm.Config.SetConfigSetting("TW3_Path", value);
                }
            }
        }

       
        #endregion

        public ICommand OKCommand { get; }
        public ICommand CancelCommand { get; }

       

        private void Save()
        {
            vm.Config.Save();
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
    }
}
