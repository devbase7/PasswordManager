#region Namespaces
#region Namespaces Microsoft
using System;
#endregion


#region Namespaces others
using PasswordManager.DTO.DTOs;
using PasswordManager.M.Ms.DatabaseManagers.ApplicationData;
using PasswordManager.M.Repositories.Databases;
#endregion
#endregion


namespace PasswordManager.M.Ms.DatabaseRepositoriesFacade
{
	public sealed class MDatabaseRepositoriesFacade
	{
		#region Fields internal
		private readonly MApplicationDataSQLiteDatabaseManager
			_mApplicationDataRepository = new MApplicationDataSQLiteDatabaseManager();

		private IAccountsDataRepository
			_mAccountsDataRepository;
		#endregion


		#region Constructors interface
		public MDatabaseRepositoriesFacade(IAccountsDataRepository mAccountsDataRepository) =>
			_mAccountsDataRepository =
				mAccountsDataRepository
				?? throw GetMessageArgumentNullException(nameof(mAccountsDataRepository));
		#endregion


		#region Properties interface
		public IAccountsDataRepository MAccountsDataRepository
		{
			set =>
				_mAccountsDataRepository =
					value
					?? throw GetMessageArgumentNullException(nameof(value));
		}
		#endregion


		#region Methods
		#region Methods interface
		#region Manage local SQLite database application data
		public DTOAuthorizationStateQuery GetAuthorizationState() =>
			_mApplicationDataRepository.GetAuthorizationState();

		public DTOError SetAuthorizationState(DTOAuthorizationState authorizationState) =>
			_mApplicationDataRepository.SetAuthorizationState(authorizationState);

		public (DTOError error, bool isRememberMe) GetAuthorizationStateIsRememberMe() =>
			_mApplicationDataRepository.GetAuthorizationStateIsRememberMe();

		public DTOError ResetAuthorizationState() =>
			_mApplicationDataRepository.ResetAuthorizationState();

		public (DTOError error, string login, string password) GetSessionAccount() =>
			_mApplicationDataRepository.GetSessionAccount();

		public DTOError SetSessionAccount(string login, string password) =>
			_mApplicationDataRepository.SetSessionAccount(login, password);

		public DTOError ResetSessionAccount(string login, string password) =>
			_mApplicationDataRepository.ResetSessionAccount(login, password);
		#endregion


		#region Manage local/web SQLite/SQL database accounts data
		public DTOError RegisterNewAccount(string login, string password) =>
			_mAccountsDataRepository.RegisterNewAccount(login, password);

		public DTOError FindAccountAmoungRegistered(string login, string password) =>
			_mAccountsDataRepository.FindAccountAmoungRegistered(login, password);

		public DTOAccountDataQuery GetAccountData(string login, string password) =>
			_mAccountsDataRepository.GetAccountData(login, password);

		public DTOError SetAccountData(string login, string password, DTOAccountData accountData) =>
			_mAccountsDataRepository.SetAccountData(login, password, accountData);
		#endregion
		#endregion


		#region Methods internal
		private ArgumentNullException GetMessageArgumentNullException(string parameterNameEqualNull) =>
			new ArgumentNullException($"The argument of the \"{parameterNameEqualNull}\" parameter can't be \"null\"!");
		#endregion
		#endregion
	}
}