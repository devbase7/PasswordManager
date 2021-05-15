namespace PasswordManager.DTO.DTOs
{
	public sealed class DTORecord
	{
		#region Constructors interface
		public DTORecord
			(ulong id, bool isFavourite, string header, string category, string service, string url, string login,
			string password, string note)
		{
			Id = id;
			IsFavourite = isFavourite;
			Header = header;
			Category = category;
			Service = service;
			URL = url;
			Login = login;
			Password = password;
			Note = note;
		}
		#endregion


		#region Properties interface
		public ulong Id { get; }

		public bool IsFavourite { get; }

		public string Header { get; }

		public string Category { get; }

		public string Service { get; }

		public string URL { get; }

		public string Login { get; }

		public string Password { get; }

		public string Note { get; }
		#endregion
	}
}