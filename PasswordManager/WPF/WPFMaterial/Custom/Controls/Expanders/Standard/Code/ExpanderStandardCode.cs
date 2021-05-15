#region Namespaces Microsoft
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#endregion


namespace WPFMaterial.Custom.Controls.Expanders.Standard.Code
{
	public class ExpanderStandardCode : Expander
	{
		#region Fields static interface
		public static readonly DependencyProperty
			HeaderIconProperty,
			HeaderToolTipContentProperty,
			HeaderToolTipPaddingProperty;
		#endregion


		#region Constructor static internal
		static ExpanderStandardCode()
		{
			#region Dependency properties adding metadata
			DefaultStyleKeyProperty.OverrideMetadata
			(
				typeof(ExpanderStandardCode),
				new FrameworkPropertyMetadata(typeof(ExpanderStandardCode))
			);
			#endregion


			#region Dependency properties registration
			HeaderIconProperty =
				DependencyProperty.Register
				(
					nameof(HeaderIcon),
					typeof(PathGeometry),
					typeof(ExpanderStandardCode)
				);

			HeaderToolTipContentProperty =
				DependencyProperty.Register
				(
					nameof(HeaderToolTipContent),
					typeof(object),
					typeof(ExpanderStandardCode),
					new PropertyMetadata()
				);

			HeaderToolTipPaddingProperty =
				DependencyProperty.Register
				(
					nameof(HeaderToolTipPadding),
					typeof(Thickness),
					typeof(ExpanderStandardCode)
				);
			#endregion
		}
		#endregion


		#region Properties infrastructure of registered dependency properties interface
		public PathGeometry HeaderIcon
		{
			get =>
				(PathGeometry)GetValue(HeaderIconProperty);

			set =>
				SetValue(HeaderIconProperty, value);
		}

		public object HeaderToolTipContent
		{
			get =>
				GetValue(HeaderToolTipContentProperty);

			set =>
				SetValue(HeaderToolTipContentProperty, value);
		}

		public Thickness HeaderToolTipPadding
		{
			get =>
				(Thickness)GetValue(HeaderToolTipPaddingProperty);

			set =>
				SetValue(HeaderToolTipPaddingProperty, value);
		}
		#endregion
	}
}