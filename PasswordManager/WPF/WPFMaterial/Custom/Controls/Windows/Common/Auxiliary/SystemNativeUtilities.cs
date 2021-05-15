#region Namespaces Microsoft
using System;
using System.Runtime.InteropServices;
#endregion


namespace WPFMaterial.Custom.Controls.Windows.Common.Auxiliary
{
	internal static class SystemNativeUtilities
	{
		#region Fields interface
		internal static uint
			TpmLeftAlign,
			TpmReturnCMD;
		#endregion


		#region Constructor static internal
		static SystemNativeUtilities()
		{
			TpmLeftAlign = 0;
			TpmReturnCMD = 256;
		}
		#endregion


		#region Methods interface
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
		internal static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

		[DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
		internal static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

		[DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
		internal static extern int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

		[DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
		internal static extern IntPtr PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
		#endregion
	}
}