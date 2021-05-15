#region Namespaces others
using PasswordManager.DTO.Auxiliary;
#endregion


namespace PasswordManager.DTO.DTOs
{
	public sealed class DTOError
	{
		#region Constructors interface
		public DTOError(ErrorTypes type, string message)
		{
			Type = type;
			Message = message;
		}
		#endregion


		#region Properties interface
		public ErrorTypes Type { get; }

		public string Message { get; }
		#endregion
	}
}