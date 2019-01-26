﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace wcc_lite_gui_wpf.Commands
{
    /// <summary>
    /// Defines an ICommand that delegates implementation to an <see cref="Action{T}"/> and <see cref="Predicate{T}"/>.
    /// </summary>
    /// <typeparam name="T">The command parameter type.</typeparam>
    public class DelegateCommand<T> : CommandBase
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">The methood used to execute the command.</param>
        public DelegateCommand(Action<T> execute)
            : this(execute, null)
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">The methood used to execute the command.</param>
        /// <param name="canExecute">The methood used to check if the command can be executed.</param>
        public DelegateCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        #region ICommand Members
        /// <summary>
        /// Check if the command can be executed.
        /// </summary>
        /// <param name="parameter">The command parameter</param>
        /// <returns>True if the command can be executed, otherwise false.</returns>
        public override bool CanExecute(object parameter)
        {
            if (_canExecute is null)
            {
                return true;
            }
            return _canExecute((T)parameter);
        }
        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">The command parameter</param>
        public override void Execute(object parameter)
        {
            _execute((T)parameter);
        }
        #endregion
    }
}