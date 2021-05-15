#region Namespaces Microsoft
using System.Collections.Generic;
#endregion


namespace PasswordManager.DTO.DTOs
{
	public sealed class DTOAccountData
	{
		#region Constructors interface
		public DTOAccountData(List<DTOCategory> customCategories, List<DTORecord> records)
		{
			CustomCategories = customCategories;
			Records = records;
		}
		#endregion


		#region Properties interface
		public List<DTOCategory> CustomCategories { get; }

		public List<DTORecord> Records { get; }
		#endregion
	}
}