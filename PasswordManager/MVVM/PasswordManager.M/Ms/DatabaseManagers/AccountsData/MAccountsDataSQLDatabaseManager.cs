#region Namespaces
#region Namespaces Microsoft
using System;
#endregion


#region Namespaces others
using PasswordManager.DTO.DTOs;
using PasswordManager.M.Repositories.Databases;
#endregion
#endregion


namespace PasswordManager.M.Ms.DatabaseManagers.AccountsData
{
	public sealed class MAccountsDataSQLDatabaseManager : IAccountsDataRepository
	{
		#region Implementing IAccountsDataRepository members
		public DTOError RegisterNewAccount(string login, string password)
		{
			return null;
		}

		public DTOError FindAccountAmoungRegistered(string login, string password)
		{
			return null;
		}

		public DTOAccountDataQuery GetAccountData(string login, string password)
		{
			return null;
		}

		public DTOError SetAccountData(string login, string password, DTOAccountData accountData)
		{
			return null;
		}
		#endregion


		#region Implementing IDisposable members
		public void Dispose() =>
			GC.SuppressFinalize(this);
		#endregion
	}
}