#region Namespaces Microsoft
using System.Reflection;
using System.Windows;
#endregion


namespace WPFMaterial.Custom.Controls.Windows.Common.Auxiliary
{
	internal static class SystemScreenValues
	{
		#region Methods static interface
		internal static int GetCurrentDPI() =>
			(int)typeof(SystemParameters).GetProperty
			(
				"Dpi",
				BindingFlags.Static
				| BindingFlags.NonPublic
			).GetValue(null, null);

		internal static double GetCurrentDPIScaleFactor() =>
			(double)GetCurrentDPI() / 96;
		#endregion
	}
}