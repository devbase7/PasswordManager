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
	public sealed class VMChangeLoginPassword : AVMInfrastructure
	{
		#region Fields internal
		private string
			_newLogin,
			_confirmNewLogin,
			_newPassword,
			_confirmNewPassword;

		private bool
			_confirmNewLoginIsEnabled,
			_confirmNewPasswordIsEnabled;

		private bool
			_newLoginIsSetFocus,
			_newPasswordIsSetFocus;

		private RelayCommand
			_change;
		#endregion


		#region Constructors interface
		public VMChangeLoginPassword(InfrastructureArgs infrastructureArgs)
		: base(infrastructureArgs)
		{ }
		#endregion


		#region Properties
		#region Properties data infrastructure for the View interface
		public string NewLogin
		{
			get =>
				_newLogin;

			set
			{
				_newLogin = value;
				OnPropertyChanged();

				if (string.IsNullOrEmpty(value))
				{
					ConfirmNewLogin = string.Empty;
					ConfirmNewLoginIsEnabled = false;
				}
				else
					if (!_confirmNewLoginIsEnabled)
						ConfirmNewLoginIsEnabled = true;

				_change.RaiseCanExecuteChanged();
			}
		}

		public string ConfirmNewLogin
		{
			get =>
				_confirmNewLogin;

			set
			{
				_confirmNewLogin = value;
				OnPropertyChanged();

				_change.RaiseCanExecuteChanged();
			}
		}

		public string NewPassword
		{
			get =>
				_newPassword;

			set
			{
				_newPassword = value;
				OnPropertyChanged();

				if (string.IsNullOrEmpty(value))
				{
					ConfirmNewPassword = string.Empty;
					ConfirmNewPasswordIsEnabled = false;
				}
				else
					if (!_confirmNewPasswordIsEnabled)
						ConfirmNewPasswordIsEnabled = true;

				_change.RaiseCanExecuteChanged();
			}
		}

		public string ConfirmNewPassword
		{
			get =>
				_confirmNewPassword;

			set
			{
				_confirmNewPassword = value;
				OnPropertyChanged();

				_change.RaiseCanExecuteChanged();
			}
		}

		public bool ConfirmNewLoginIsEnabled
		{
			get =>
				_confirmNewLoginIsEnabled;

			set
			{
				_confirmNewLoginIsEnabled = value;
				OnPropertyChanged();
			}
		}

		public bool ConfirmNewPasswordIsEnabled
		{
			get =>
				_confirmNewPasswordIsEnabled;

			set
			{
				_confirmNewPasswordIsEnabled = value;
				OnPropertyChanged();
			}
		}

		public bool NewLoginIsSetFocus
		{
			get =>
				_newLoginIsSetFocus;

			set
			{
				_newLoginIsSetFocus = value;
				OnPropertyChanged();
			}
		}

		public bool NewPasswordIsSetFocus
		{
			get =>
				_newPasswordIsSetFocus;

			set
			{
				_newPasswordIsSetFocus = value;
				OnPropertyChanged();
			}
		}
		#endregion


		#region Properties commands infrastructure for the View interface
		public ICommand Change =>
			_change
			?? (_change = new RelayCommand(Execute_Change, CanExecute_Change));
		#endregion
		#endregion


		#region Methods
		#region Methods internal
		private bool LoginBlockIsValid()
		{
			bool
				newLoginIsEmpty = string.IsNullOrEmpty(_newLogin),
				newPasswordIsEmpty = string.IsNullOrEmpty(_newPassword),
				confirmNewPasswordIsEmpty = string.IsNullOrEmpty(_confirmNewPassword);

			return
				!newLoginIsEmpty
				&& _newLogin == _confirmNewLogin
				&& newPasswordIsEmpty
				&& confirmNewPasswordIsEmpty;
		}

		private bool PasswordBlockIsValid()
		{
			bool
				newLoginIsEmpty = string.IsNullOrEmpty(_newLogin),
				confirmNewLoginIsEmpty = string.IsNullOrEmpty(_confirmNewLogin),
				newPasswordIsEmpty = string.IsNullOrEmpty(_newPassword);

			return
				!newPasswordIsEmpty
				&& _newPassword == _confirmNewPassword
				&& newLoginIsEmpty
				&& confirmNewLoginIsEmpty;
		}

		private bool LoginAndPasswordBlocksIsValid()
		{
			bool
				newLoginIsEmpty = string.IsNullOrEmpty(_newLogin),
				newPasswordIsEmpty = string.IsNullOrEmpty(_newPassword);

			return
				!newLoginIsEmpty
				&& !newPasswordIsEmpty
				&& _newLogin == _confirmNewLogin
				&& _newPassword == _confirmNewPassword;
		}
		#endregion


		#region Methods commands for the View internal
		//'Change'.
		private void Execute_Change(object commandParameter)
		{ }

		private bool CanExecute_Change(object commandParameter) =>
			LoginBlockIsValid()
			|| PasswordBlockIsValid()
			|| LoginAndPasswordBlocksIsValid();
		#endregion
		#endregion
	}
}