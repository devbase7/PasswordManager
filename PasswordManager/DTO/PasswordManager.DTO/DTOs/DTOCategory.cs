namespace PasswordManager.DTO.DTOs
{
	public sealed class DTOCategory
	{
		#region Constructors interface
		public DTOCategory(string iconPath, string iconColor, string name)
		{
			IconPath = iconPath;
			IconColor = iconColor;
			Name = name;
		}
		#endregion


		#region Properties interface
		public string IconPath { get; }

		public string IconColor { get; }

		public string Name { get; }
		#endregion
	}
}