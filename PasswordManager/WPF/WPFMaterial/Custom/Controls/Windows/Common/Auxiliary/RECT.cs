#region Namespaces Microsoft
using System;
using System.Windows;
#endregion


namespace WPFMaterial.Custom.Controls.Windows.Common.Auxiliary
{
	[Serializable]
	internal struct RECT
	{
		#region Fields interface
		internal int
			Left,
			Top,
			Right,
			Bottom;
		#endregion


		#region Constructors interface
		internal RECT(Rect rect)
		{
			Left = (int)rect.Left;
			Top = (int)rect.Top;
			Right = (int)rect.Right;
			Bottom = (int)rect.Bottom;
		}

		internal RECT(int left, int top, int right, int bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}
		#endregion


		#region Properties interface
		internal int Width
		{
			get =>
				Right - Left;

			set =>
				Right = Left + value;
		}

		internal int Height
		{
			get =>
				Bottom - Top;

			set =>
				Bottom = Top + value;
		}

		internal Size Size =>
			new Size(Width, Height);

		internal Point Position =>
			new Point(Left, Top);
		#endregion


		#region Methods interface
		internal void Offset(int dx, int dy)
		{
			Left += dx;
			Right += dx;
			Top += dy;
			Bottom += dy;
		}

		internal Int32Rect ToInt32Rect() =>
			new Int32Rect(Left, Top, Width, Height);
		#endregion
	}
}