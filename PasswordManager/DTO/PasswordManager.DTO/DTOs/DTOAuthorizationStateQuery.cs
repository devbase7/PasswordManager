namespace PasswordManager.DTO.DTOs
{
	public sealed class DTOAuthorizationStateQuery
	{
		#region Constructors interface
		public DTOAuthorizationStateQuery(DTOError error, DTOAuthorizationState authorizationState)
		{
			Error = error;
			AuthorizationState = authorizationState;
		}
		#endregion


		#region Properties interface
		public DTOError Error { get; }

		public DTOAuthorizationState AuthorizationState { get; }
		#endregion
	}
}