#region Namespaces
#region Namespaces Microsoft
using System;
#endregion


#region Namespaces others
using PasswordManager.DTO.DTOs;
#endregion
#endregion


namespace PasswordManager.M.Repositories.Databases
{
	public interface IAccountsDataRepository : IDisposable
	{
		#region Methods signatures
		DTOError RegisterNewAccount(string login, string password);

		DTOError FindAccountAmoungRegistered(string login, string password);

		DTOAccountDataQuery GetAccountData(string login, string password);

		DTOError SetAccountData(string login, string password, DTOAccountData accountData);
		#endregion
	}
}