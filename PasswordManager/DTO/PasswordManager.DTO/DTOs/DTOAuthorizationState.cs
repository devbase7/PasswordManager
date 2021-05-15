namespace PasswordManager.DTO.DTOs
{
	public sealed class DTOAuthorizationState
	{
		#region Constructors interface
		public DTOAuthorizationState(string login, string password, bool isRememberMe)
		{
			Login = login;
			Password = password;
			IsRememberMe = isRememberMe;
		}
		#endregion


		#region Properties interface
		public string Login { get; }

		public string Password { get; }

		public bool IsRememberMe { get; }
		#endregion
	}
}