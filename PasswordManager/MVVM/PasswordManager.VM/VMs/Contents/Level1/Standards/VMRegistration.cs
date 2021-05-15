#region Namespaces
#region Namespaces Microsoft
using System.Media;
using System.Windows.Input;
#endregion


#region Namespaces others
using PasswordManager.DTO.Auxiliary;
using PasswordManager.DTO.DTOs;
using PasswordManager.VM.Infrastructure.AVMs;
using PasswordManager.VM.Infrastructure.Other;
#endregion
#endregion

using System.Windows;
namespace PasswordManager.VM.VMs.Contents.Level1.Standards
{
	public sealed class VMRegistration : AVMInfrastructure, IContentNavigation
	{
		#region Fields internal
		private string
			_login,
			_password,
			_confirmPassword;

		private bool
			_confirmPasswordIsEnabled;

		private bool
			_loginIsSetFocus = true;

		private RelayCommand
			_register;
		#endregion


		#region Constructors interface
		public VMRegistration(InfrastructureArgs infrastructureArgs)
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

				_register.RaiseCanExecuteChanged();
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

				if (string.IsNullOrEmpty(value))
				{
					ConfirmPassword = string.Empty;
					ConfirmPasswordIsEnabled = false;
				}
				else
					if (!_confirmPasswordIsEnabled)
						ConfirmPasswordIsEnabled = true;

				_register.RaiseCanExecuteChanged();
			}
		}

		public string ConfirmPassword
		{
			get =>
				_confirmPassword;

			set
			{
				_confirmPassword = value;
				OnPropertyChanged();

				_register.RaiseCanExecuteChanged();
			}
		}

		public bool ConfirmPasswordIsEnabled
		{
			get =>
				_confirmPasswordIsEnabled;

			set
			{
				_confirmPasswordIsEnabled = value;
				OnPropertyChanged();
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
		public ICommand Register =>
			_register
			?? (_register = new RelayCommand(Execute_Register, CanExecute_Register));
		#endregion
		#endregion


		#region Methods commands for the View internal
		//'Register'.
		private void Execute_Register(object commandParameter)
		{
			DTOError
				errorRegister = MDatabaseRepositoriesFacade.RegisterNewAccount(_login, _password);

			if (errorRegister == null)
			{
				VMAuthorization
					vmAuthorization;

				var
					(errorAuthorizationStateIsRememberMe, authorizationStateIsRememberMe) =
						MDatabaseRepositoriesFacade.GetAuthorizationStateIsRememberMe();

				InfrastructureArgs
					infrastructureArgs = new InfrastructureArgs(MDatabaseRepositoriesFacade, ChangingContentService);

				if
				(
					authorizationStateIsRememberMe
					&& errorAuthorizationStateIsRememberMe == null
				)
				{
					DTOAuthorizationStateQuery
						authorizationStateQuery = MDatabaseRepositoriesFacade.GetAuthorizationState();

                    vmAuthorization = new VMAuthorization(authorizationStateQuery, infrastructureArgs);

					ChangingContentService.SetCurrentContent(vmAuthorization);
				}
				else
				{
					vmAuthorization = new VMAuthorization(_login, _password, infrastructureArgs);

					ChangingContentService.SetCurrentContent(vmAuthorization);
				}
			}
			else
			{
				SystemSounds.Hand.Play();

				switch (errorRegister.Type)
				{
					case ErrorTypes.Attention:
						MessageBox.Show(errorRegister.Message);
						//Пользователь с таким Логином уже зарегистрирован!.
						LoginIsSetFocus = true;
						break;

					case ErrorTypes.Fatal:
						MessageBox.Show(errorRegister.Message);
						//Не создана директория для размещения базы данных/Не создана сама база данных/Не создана
						// таблица для размещения данных зарегистрированных аккаунтов/Не создана таблица с записями
						// самого аккаунта/Не сохранены данные регистрируемого аккаунта.
						break;
				}
			}
		}

		private bool CanExecute_Register(object commandParameter) =>
			!(
				string.IsNullOrEmpty(_login)
				|| string.IsNullOrEmpty(_password)
			)
			&& _password == _confirmPassword;
		#endregion


		#region Implementing IContentNavigation members
		public void Back()
		{
			VMAuthorization
				vmAuthorization;

			var
				(errorAuthorizationStateIsRememberMe, authorizationStateIsRememberMe) =
					MDatabaseRepositoriesFacade.GetAuthorizationStateIsRememberMe();

			InfrastructureArgs
				infrastructureArgs = new InfrastructureArgs(MDatabaseRepositoriesFacade, ChangingContentService);

			if
			(
				authorizationStateIsRememberMe
				&& errorAuthorizationStateIsRememberMe == null
			)
			{
				DTOAuthorizationStateQuery
					authorizationStateQuery = MDatabaseRepositoriesFacade.GetAuthorizationState();

				vmAuthorization = new VMAuthorization(authorizationStateQuery, infrastructureArgs);

				ChangingContentService.SetCurrentContent(vmAuthorization);
			}
			else
			{
				vmAuthorization = new VMAuthorization(infrastructureArgs);

				ChangingContentService.SetCurrentContent(vmAuthorization);
			}
		}
		#endregion
	}
}