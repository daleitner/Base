using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;


namespace Base
{
	public class RelayCommand : ICommand
	{
		readonly Action<object> _execute;
		readonly Predicate<object> _canExecute;

		public RelayCommand(Action<object> execute)
			: this(execute, null) { }

		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");
			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			try
			{
				return _canExecute == null ? true : _canExecute(parameter);
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e.Message);
			}

			return false;
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object parameter)
		{
			try
			{
				_execute(parameter);
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e.Message);
			}
		}
	}
}
