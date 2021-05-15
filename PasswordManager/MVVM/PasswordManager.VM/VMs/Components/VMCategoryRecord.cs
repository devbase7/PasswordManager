#region Namespaces others
using PasswordManager.VM.Infrastructure.AVMs;
#endregion


namespace PasswordManager.VM.VMs.Components
{
	public sealed class VMCategoryRecord : AVMBase
	{
		#region Constructors interface
		public VMCategoryRecord(string iconPath, string iconColor, string name)
		{
			IconPath = iconPath;
			IconColor = iconColor;
			Name = name;
		}
		#endregion


		#region Properties data infrastructure for the View interface
		public string IconPath { get; set; }

		public string IconColor { get; set; }

		public string Name { get; set; }
		#endregion
	}
}