#region Namespaces
#region Namespaces Microsoft
using System.Windows.Input;
#endregion


#region Namespaces others
using PasswordManager.VM.Infrastructure.AVMs;
using PasswordManager.VM.Infrastructure.Other;
#endregion
#endregion


namespace PasswordManager.VM.VMs.Contents.Level2.Standards
{
	public sealed class VMLoginPassword : AVMInfrastructure
	{
		#region Fields internal
		private string
			_login,
			_password;

		private bool
			_loginIsSetFocus = true;

		private RelayCommand
			_continue;
		#endregion


		#region Constructors interface
		public VMLoginPassword(InfrastructureArgs infrastructureArgs)
		: base(infrastructureArgs)
		{ }
		#endregion


		#region Properties
		#region Properties data infrastructure for the View interface
		public string Login
		{
			get =>
				_login;

			set
			{
				_login = value;
				OnPropertyChanged();

				_continue.RaiseCanExecuteChanged();
			}
		}

		public string Password
		{
			get =>
				_password;

			set
			{
				_password = value;
				OnPropertyChanged();

				_continue.RaiseCanExecuteChanged();
			}
		}

		public bool LoginIsSetFocus
		{
			get =>
				_loginIsSetFocus;

			set
			{
				_loginIsSetFocus = value;
				OnPropertyChanged();
			}
		}
		#endregion


		#region Properties commands infrastructure for the View interface
		public ICommand Continue =>
			_continue
			?? (_continue = new RelayCommand(Execute_Continue, CanExecute_Continue));
		#endregion
		#endregion


		#region Methods commands for the View internal
		//'Continue'.
		private void Execute_Continue(object commandParameter)
		{
			if
			(
				_login == "q"
				&& _password == "q"
			)
			{
				InfrastructureArgs
					infrastructureArgs = new InfrastructureArgs(MDatabaseRepositoriesFacade, ChangingContentService);

				VMChangeLoginPassword
					vmChangeLoginPassword = new VMChangeLoginPassword(infrastructureArgs);

				ChangingContentService.SetCurrentContent(vmChangeLoginPassword);
			}
		}

		private bool CanExecute_Continue(object commandParameter) =>
			!(
				string.IsNullOrEmpty(_login)
				|| string.IsNullOrEmpty(_password)
			);
		#endregion
	}
}