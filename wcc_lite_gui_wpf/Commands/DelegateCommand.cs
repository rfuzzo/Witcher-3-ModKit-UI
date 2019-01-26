﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace wcc_lite_gui_wpf.Commands
{
    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {

        }
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
            : base(execute, canExecute)
        {
            
        }
    }
}