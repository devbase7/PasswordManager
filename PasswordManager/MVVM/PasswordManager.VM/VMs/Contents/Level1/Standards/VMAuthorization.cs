#region Namespaces
#region Namespaces Microsoft
using System.Threading.Tasks;
using System.Windows.Input;
#endregion


#region Namespaces others
using PasswordManager.DTO.Auxiliary;
using PasswordManager.DTO.DTOs;
using PasswordManager.VM.Auxiliary;
using PasswordManager.VM.Infrastructure.AVMs;
using PasswordManager.VM.Infrastructure.Other;
#endregion
#endregion


namespace PasswordManager.VM.VMs.Contents.Level1.Standards
{
	public sealed class VMAuthorization : AVMInfrastructure
	{
		#region Fields internal
		private string
			_login,
			_password;

		private bool
			_isRememberMe;

		private bool
			_loginIsSetFocus;

		private RelayCommand
			_logIn,
			_registeringNewAccount;
		#endregion


		#region Constructors interface
		public VMAuthorization(InfrastructureArgs infrastructureArgs)
		: base(infrastructureArgs) =>
			_loginIsSetFocus = true;

		public VMAuthorization
			(DTOAuthorizationStateQuery authorizationStateQuery, InfrastructureArgs infrastructureArgs)
		: base(infrastructureArgs)
		{
			if
			(
				authorizationStateQuery.Error == null
				&& authorizationStateQuery.AuthorizationState != null
			)
			{
				DTOAuthorizationState
					authorizationState = authorizationStateQuery.AuthorizationState;

				_login = authorizationState.Login;
				_password = authorizationState.Password;
				_isRememberMe = authorizationState.IsRememberMe;

				if (_login == string.Empty)
					_loginIsSetFocus = true;

				if (_password == string.Empty)
					PasswordIsSetFocus = true;
			}
			else
			{
				DialogWindow.Selector(authorizationStateQuery.Error);

				_loginIsSetFocus = true;
			}
		}

		public VMAuthorization(string login, string password, InfrastructureArgs infrastructureArgs)
		: base(infrastructureArgs)
		{
			_login = login;
			_password = password;
		}
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

				_logIn.RaiseCanExecuteChanged();

				if (_isRememberMe)
					SetOrResetAuthorizationState();
			}
		}

		public string Password
		{
			get =>
				_password;

			set
			{
				_password = value;

				_logIn.RaiseCanExecuteChanged();

				if (_isRememberMe)
					SetOrResetAuthorizationState();
			}
		}

		public bool IsRememberMe
		{
			get =>
				_isRememberMe;

			set
			{
				if (value)
				{
					if
					(
						!(
							string.IsNullOrEmpty(_login)
							&& string.IsNullOrEmpty(_password)
						)
					)
					{
						DTOAuthorizationState
							authorizationState = new DTOAuthorizationState(_login, _password, value);

						DTOError
							error = MDatabaseRepositoriesFacade.SetAuthorizationState(authorizationState);

						if (error != null)
						{
							DialogWindow.Selector(error);

							return;
						}
					}
				}
				else
				{
					DTOError
						error = MDatabaseRepositoriesFacade.ResetAuthorizationState();

					if (error != null)
					{
						DialogWindow.Selector(error);

						return;
					}
				}

				_isRememberMe = value;
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

		public bool PasswordIsSetFocus { get; set; }
		#endregion


		#region Properties commands infrastructure for the View interface
		public ICommand LogIn =>
			_logIn
			?? (_logIn = new RelayCommand(Execute_LogIn, CanExecute_LogIn));

		public ICommand RegisteringNewAccount =>
			_registeringNewAccount
			?? (_registeringNewAccount = new RelayCommand(Execute_RegisteringNewAccount));
		#endregion
		#endregion


		#region Methods
		#region Methods internal
		private async void SetOrResetAuthorizationState()
		{
			DTOError
				error;

			if
			(
				string.IsNullOrEmpty(_login)
				&& string.IsNullOrEmpty(_password)
			)
			{
				await
					Task.Run
					(
						() =>
							{
								error = MDatabaseRepositoriesFacade.ResetAuthorizationState();

								if (error != null)
									DialogWindow.Selector(error);
							}
					);
			}
			else
			{
				await
					Task.Run
					(
						() =>
							{
								DTOAuthorizationState
									authorizationState = new DTOAuthorizationState(_login, _password, _isRememberMe);

								error = MDatabaseRepositoriesFacade.SetAuthorizationState(authorizationState);

								if (error != null)
									DialogWindow.Selector(error);
							}
					);
			}
		}
		#endregion


		#region Methods commands for the View internal
		//'LogIn'.
		private void Execute_LogIn(object commandParameter)
		{
			DTOError
				errorFindAccountAmoungRegistered =
					MDatabaseRepositoriesFacade.FindAccountAmoungRegistered(_login, _password);

			if (errorFindAccountAmoungRegistered == null)
			{
				DTOAccountDataQuery
					accountDataQuery = MDatabaseRepositoriesFacade.GetAccountData(_login, _password);

				if (accountDataQuery.Error == null)
				{
					InfrastructureArgs
						infrastructureArgs =
							new InfrastructureArgs(MDatabaseRepositoriesFacade, ChangingContentService);

					VMMain
						vmMain = new VMMain(accountDataQuery.AccountData, infrastructureArgs);

					ChangingContentService.SetCurrentContent(vmMain);
				}
				else
					DialogWindow.Selector(errorFindAccountAmoungRegistered);
			}
			else
			{
				DialogWindow.Selector(errorFindAccountAmoungRegistered);

				if (errorFindAccountAmoungRegistered.Type == ErrorTypes.Attention)
					LoginIsSetFocus = true;
			}
		}

		private bool CanExecute_LogIn(object commandParameter) =>
			!(
				string.IsNullOrEmpty(_login)
				|| string.IsNullOrEmpty(_password)
			);

		//'RegisteringNewAccount'.
		private void Execute_RegisteringNewAccount(object commandParameter)
		{
			InfrastructureArgs
				infrastructureArgs = new InfrastructureArgs(MDatabaseRepositoriesFacade, ChangingContentService);

			VMRegistration
				vmRegistration = new VMRegistration(infrastructureArgs);

			ChangingContentService.SetCurrentContent(vmRegistration);
		}
		#endregion
		#endregion
	}
}