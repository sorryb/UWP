﻿    using System;
    using System.Windows.Input;
    using System.Diagnostics;


namespace MyCalculatorv1
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other
    /// objects by invoking delegates. The default return value for the
    /// CanExecute method is 'true'.
    /// See:
    /// http://www.nullskull.com/faq/905/pass-command-parameter-to-relaycommand.aspx
    /// </summary>
     public class RelayCommandGeneric<T> : ICommand
    {
        #region Fields

        readonly Action<T> _execute = null;
        readonly Predicate<T> _canExecute = null;

        #endregion // Fields

        #region Constructors


        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommandGeneric(Action<T> execute): this(execute, null)
        {

        }
 
	    /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommandGeneric(Action<T> execute, Predicate<T> canExecute)
        {

	         if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;

            _canExecute  = canExecute;

        }
   
	     #endregion // Constructors



        #region ICommand Members



        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {     
	       return _canExecute == null ? true : _canExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
         
        public void Execute(object parameter)
        {     
	       _execute((T)parameter);
        }

	    #endregion // ICommand Members

    }
}