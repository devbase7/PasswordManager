#region Namespaces Microsoft
using System;
using System.Globalization;
using System.Windows.Data;
#endregion


namespace WPFMaterial.Custom.Controls.ComboBoxes.Standard.Auxiliary
{
	internal sealed class ConverterMultiItemsPanelHeight : IMultiValueConverter
	{
		#region Implementing IMultiValueConverter members
		object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture) =>
			//values[0] – thickness of the top window border 'BorderThickness.Top'.
			//values[1] – chrome height 'WindowChrome.CaptionHeight'.
			//values[2] – thickness of the top border resize 'WindowChrome.ResizeBorderThickness.Top'.
			//values[3] – thickness of the bottom border of chrome 'BorderThickness.Bottom'.
			(double)values[1] - (double)values[0] - (double)values[3] + (double)values[2];

		object[] IMultiValueConverter.ConvertBack
			(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
				throw new NotImplementedException
					($"\"{nameof(IMultiValueConverter.ConvertBack)}\" is not implemented!");
		#endregion
	}
}