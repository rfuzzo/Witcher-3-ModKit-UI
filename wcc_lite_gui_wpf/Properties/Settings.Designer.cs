﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace wcc_lite_gui_wpf.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string WccPath {
            get {
                return ((string)(this["WccPath"]));
            }
            set {
                this["WccPath"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string GamePath
        {
            get
            {
                return ((string)(this["GamePath"]));
            }
            set
            {
                this["GamePath"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ToolsPath
        {
            get
            {
                return ((string)(this["ToolsPath"]));
            }
            set
            {
                this["ToolsPath"] = value;
            }
        }

        [global::System.Configuration.SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Binary)]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Collections.ObjectModel.ObservableCollection<Wcc_lite_core.WorkflowItem> WccLite_Commands
        {
            get
            {
                return ((System.Collections.ObjectModel.ObservableCollection<Wcc_lite_core.WorkflowItem>)(this["WccLite_Commands"]));
            }
            set
            {
                this["WccLite_Commands"] = value;
            }
        }

        [global::System.Configuration.SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Binary)]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Collections.ObjectModel.ObservableCollection<WccUIVariable> EditorVariables
        {
            get
            {
                return ((System.Collections.ObjectModel.ObservableCollection<WccUIVariable>)(this["EditorVariables"]));
            }
            set
            {
                this["EditorVariables"] = value;
            }
        }
    }
}
