namespace PasswordManager.DTO.DTOs
{
	public sealed class DTOAccountDataQuery
	{
		#region Constructors interface
		public DTOAccountDataQuery(DTOError error, DTOAccountData accountData)
		{
			Error = error;
			AccountData = accountData;
		}
		#endregion


		#region Properties interface
		public DTOError Error { get; }

		public DTOAccountData AccountData { get; }
		#endregion
	}
}