#region Namespaces Microsoft
using System;
using System.Windows.Input;
#endregion


namespace PasswordManager.VM.Infrastructure.Other
{
	internal sealed class RelayCommand : ICommand
	{
		#region Fields internal
		private readonly Action<object>
			_execute;

		private readonly Func<object, bool>
			_canExecute;
		#endregion


		#region Constructors interface
		public RelayCommand(Action<object> execute) =>
			_execute =
				execute
				?? throw GetMessageArgumentNullException(nameof(execute));

		public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
		: this(execute) =>
			_canExecute =
				canExecute
				?? throw GetMessageArgumentNullException(nameof(canExecute));
		#endregion


		#region Methods
		#region Methods interface
		internal void RaiseCanExecuteChanged() =>
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		#endregion


		#region Methods internal
		private ArgumentNullException GetMessageArgumentNullException(string parameterNameEqualNull) =>
			new ArgumentNullException($"The argument of the \"{parameterNameEqualNull}\" parameter can't be \"null\"!");
		#endregion
		#endregion


		#region Implementing ICommand members
		public event EventHandler
			CanExecuteChanged;

		void ICommand.Execute(object commandParameter) =>
			_execute?.Invoke(commandParameter);

		bool ICommand.CanExecute(object commandParameter) =>
			_canExecute == null
			|| _canExecute(commandParameter);
		#endregion
	}
}