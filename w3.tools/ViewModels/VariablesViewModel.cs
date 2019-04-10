using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using w3tools.App.Commands;

namespace w3tools.App.ViewModels
{

    /// <summary>
    /// SUMMARY: viewmodel for editor variables
    /// FEATURES
    /// 1. Display static Editor variables(mod directory, game directory)
    /// 2. add new variables(save them?)
    /// 3. Display dynamic Workspace variables(output stage 1, stage 2 etc)
    /// TODO
    /// 1. list binding
    /// 2. drag and drop -->
    /// </summary>
    
    public class VariablesViewModel : DockableViewModel
    {
        

        private ObservableCollection<WccUIVariable> _variables;
        /// <summary>
        /// Holds variables usable in workflows
        /// </summary>
        public ObservableCollection<WccUIVariable> Variables
        {
            get
            {
                return _variables;
            }
            set
            {
                if (_variables != value)
                {
                    _variables = value;
                    OnPropertyChanged();
                }
            }
        }

        #region Commands
        public ICommand AddVariableCommand { get; }
        public ICommand RemoveVariableCommand { get; }

        public bool CanAddVariable()
        {
            return true;
        }
        public void AddVariable()
        {
            var temp = new WccUIVariable()
            {
                Name = "New Variable",
                Parent = "Custom",
                Value = ""
            };
            _variables.Add(temp);
        }
        public bool CanRemoveVariable(WccUIVariable parameter)
        {
            return (parameter != null && parameter.Parent == "Custom");
        }
        public void RemoveVariable(WccUIVariable parameter)
        {
            _variables.Remove(parameter);
        }

        #endregion

        public VariablesViewModel()
        {
            AddVariableCommand = new RelayCommand(AddVariable, CanAddVariable);
            RemoveVariableCommand = new DelegateCommand<WccUIVariable>(RemoveVariable, CanRemoveVariable);

            
            // init saved variables
            if (Properties.Settings.Default.EditorVariables == null)
            {
                Variables = new ObservableCollection<WccUIVariable>
                {
                    new WccUIVariable()
                    {
                        Name = "Uncooked Directory",
                        Parent = "Custom",
                        Value = @"E:\moddingdir_tw3\DATA\Uncooked"
                    },
                    new WccUIVariable()
                    {
                        Name = "Mod Directory",
                        Parent = "Custom",
                        Value = @"E:\moddingdir_tw3\DATA\Uncooked"
                    },
                };
                Properties.Settings.Default.EditorVariables = Variables;
            }
            Variables = Properties.Settings.Default.EditorVariables;

        }
    }
}
