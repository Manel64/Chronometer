using System;
using System.Windows.Input;

namespace Chronometer.Core.ICommands
{
    public class RelayCommand : ICommand
    {
        /// <summary>
        ///  Event ICommand
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Action to execute in ctor
        /// </summary>
        private readonly Action _methodToExecute;

        /// <summary>
        /// Function bool if can execute ICommand
        /// </summary>
        private readonly Func<bool> _canExecuteFunction;

        /// <summary>
        /// Ctor with method and if can execute ICommand
        /// </summary>
        /// <param name="methodToExecute"></param>
        /// <param name="canExecuteFunction"></param>
        public RelayCommand(Action methodToExecute, Func<bool> canExecuteFunction)
        {
            this._methodToExecute = methodToExecute;
            this._canExecuteFunction = canExecuteFunction;
        }

        /// <summary>
        /// Ctor only with method to execute 
        /// </summary>
        /// <param name="methodToExecute"></param>
        public RelayCommand(Action methodToExecute)
            : this(methodToExecute, null)
        {
        }

        /// <summary>
        /// Bool Execute function 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>bool</returns>
        public bool CanExecute(object parameter)
        {
            if (this._canExecuteFunction == null)
            {
                return true;
            }
            else
            {
                bool result = this._canExecuteFunction.Invoke();
                return result;
            }
        }

        /// <summary>
        /// Execute with params
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            this._methodToExecute.Invoke();
        }
    }
}
