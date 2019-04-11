using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using w3tools.common;

namespace w3tools.App.Settings
{

	
    /*sealed class AppSettings : ApplicationSettingsBase
    {
        [UserScopedSettingAttribute()]
        public string GamePath
        {
            get { return (string)(this["GamePath"]); }
            set { this["GamePath"] = value; }
        }

        [UserScopedSettingAttribute()]
        public string ToolsPath
        {
            get { return (string)(this["ToolsPath"]); }
            set { this["ToolsPath"] = value; }
        }

        [UserScopedSettingAttribute()]
        public string ModKitPath
        {
            get { return (string)(this["ModKitPath"]); }
            set { this["ModKitPath"] = value; }
        }

       
    }*/


    sealed class AppSettings : ObservableObject
    {
        private string _ModKitPath;
        public string ModKitPath
        {
            get
            {
                return _ModKitPath;
            }
            set
            {
                if (_ModKitPath != value)
                {
                    _ModKitPath = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _GamePath;
        public string GamePath
        {
            get
            {
                return _GamePath;
            }
            set
            {
                if (_GamePath != value)
                {
                    _GamePath = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _ToolsPath;
        public string ToolsPath
        {
            get
            {
                return _ToolsPath;
            }
            set
            {
                if (_ToolsPath != value)
                {
                    _ToolsPath = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
