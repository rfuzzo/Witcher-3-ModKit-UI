using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3tools.common;

namespace w3tools.App.Settings
{

	
    sealed class AppSettings : ApplicationSettingsBase
    {
        [UserScopedSettingAttribute()]
        public string Host
        {
            get { return (string)(this["Host"]); }
            set { this["Host"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValue("8080")]
        public int Port
        {
            get { return (int)(this["Port"]); }
            set { this["Port"] = value; }
        }
    }

    /*
    public class AppSettings : ObservableObject
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

        private ObservableCollection<WorkflowItem> _WCC_Commands;
        public ObservableCollection<WorkflowItem> WCC_Commands
        {
            get
            {
                return _WCC_Commands;
            }
            set
            {
                if (_WCC_Commands != value)
                {
                    _WCC_Commands = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<WccUIVariable> _EditorVariables;
        public ObservableCollection<WccUIVariable> EditorVariables
        {
            get
            {
                return _EditorVariables;
            }
            set
            {
                if (_EditorVariables != value)
                {
                    _EditorVariables = value;
                    OnPropertyChanged();
                }
            }
        }



    }

    */
}
