#region Namespaces Microsoft
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#endregion


namespace WPFMaterial.Custom.Controls.ComboBoxes.Standard.Code
{
	public class ComboBoxStandardCode : ComboBox
	{
		#region Fields
		#region Fields static interface
		public static readonly DependencyProperty
			IconProperty,
			IconBrushProperty,
			HintFloatingProperty,
			HintIfTextIsEmptyIsEnabledProperty,
			HintIfTextIsEmptyVisibilityProperty,
			HintIfTextIsEmptyProperty,
			ClearIsEnabledProperty,
			IsSetFocusProperty,
			CaretBrushProperty,
			InvalidCharactersIsEnabledProperty,
			InvalidCharactersProperty;
		#endregion


		#region Fields static internal
		private static readonly DependencyPropertyKey
			HintIfTextIsEmptyVisibilityPropertyKey;
		#endregion
		#endregion


		#region Constructor static internal
		static ComboBoxStandardCode()
		{
			#region Dependency properties adding metadata
			DefaultStyleKeyProperty.OverrideMetadata
			(
				typeof(ComboBoxStandardCode),
				new FrameworkPropertyMetadata(typeof(ComboBoxStandardCode))
			);
			#endregion


			#region Dependency properties registration
			IconProperty =
				DependencyProperty.Register
				(
					nameof(Icon),
					typeof(PathGeometry),
					typeof(ComboBoxStandardCode)
				);

			IconBrushProperty =
				DependencyProperty.Register
				(
					nameof(IconBrush),
					typeof(Brush),
					typeof(ComboBoxStandardCode)
				);

			HintFloatingProperty =
				DependencyProperty.Register
				(
					nameof(HintFloating),
					typeof(string),
					typeof(ComboBoxStandardCode),
					new PropertyMetadata(string.Empty)
				);

			HintIfTextIsEmptyIsEnabledProperty =
				DependencyProperty.Register
				(
					nameof(HintIfTextIsEmptyIsEnabled),
					typeof(bool),
					typeof(ComboBoxStandardCode),
					new PropertyMetadata(HintIfTextIsEmptyIsEnabledChangedCallback)
				);

			HintIfTextIsEmptyVisibilityPropertyKey =
				DependencyProperty.RegisterReadOnly
				(
					nameof(HintIfTextIsEmptyVisibility),
					typeof(Visibility),
					typeof(ComboBoxStandardCode),
					new PropertyMetadata(Visibility.Hidden)
				);

			HintIfTextIsEmptyVisibilityProperty = HintIfTextIsEmptyVisibilityPropertyKey.DependencyProperty;

			HintIfTextIsEmptyProperty =
				DependencyProperty.Register
				(
					nameof(HintIfTextIsEmpty),
					typeof(string),
					typeof(ComboBoxStandardCode),
					new PropertyMetadata("This field must not be empty!")
				);

			IsSetFocusProperty =
				DependencyProperty.Register
				(
					nameof(IsSetFocus),
					typeof(bool),
					typeof(ComboBoxStandardCode),
					new FrameworkPropertyMetadata
					(
						false,
						FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
						IsSetFocusChangedCallback
					)
				);

			CaretBrushProperty =
				DependencyProperty.Register
				(
					nameof(CaretBrush),
					typeof(Brush),
					typeof(ComboBoxStandardCode),
					new FrameworkPropertyMetadata()
				);
			#endregion
		}
		#endregion


		#region Properties infrastructure of registered dependency properties interface
		public PathGeometry Icon
		{
			get =>
				(PathGeometry)GetValue(IconProperty);

			set =>
				SetValue(IconProperty, value);
		}

		public Brush IconBrush
		{
			get =>
				(Brush)GetValue(IconBrushProperty);

			set =>
				SetValue(IconBrushProperty, value);
		}

		public string HintFloating
		{
			get =>
				(string)GetValue(HintFloatingProperty);

			set =>
				SetValue(HintFloatingProperty, value);
		}

		public bool HintIfTextIsEmptyIsEnabled
		{
			get =>
				(bool)GetValue(HintIfTextIsEmptyIsEnabledProperty);

			set =>
				SetValue(HintIfTextIsEmptyIsEnabledProperty, value);
		}

		public Visibility HintIfTextIsEmptyVisibility
		{
			get =>
				(Visibility)GetValue(HintIfTextIsEmptyVisibilityProperty);

			protected set =>
				SetValue(HintIfTextIsEmptyVisibilityPropertyKey, value);
		}

		public string HintIfTextIsEmpty
		{
			get =>
				(string)GetValue(HintIfTextIsEmptyProperty);

			set =>
				SetValue(HintIfTextIsEmptyProperty, value);
		}

		public bool IsSetFocus
		{
			get =>
				(bool)GetValue(IsSetFocusProperty);

			set =>
				SetValue(IsSetFocusProperty, value);
		}

		public Brush CaretBrush
		{
			get =>
				(Brush)GetValue(HintFloatingProperty);

			set =>
				SetValue(HintFloatingProperty, value);
		}
		#endregion


		#region Methods
		#region Methods internal
		protected virtual void HintTextIsEmptyVisibilityUpdateValue() =>
			HintIfTextIsEmptyVisibility =
				HintIfTextIsEmptyIsEnabled
				&& string.IsNullOrEmpty(Text)
				&& IsEnabled
					? Visibility.Visible
					: Visibility.Hidden;

		protected virtual void SetTextBoxInputOutputFocus()
		{ }
		#endregion


		#region Methods callback in dependency properties internal
		protected static void HintIfTextIsEmptyIsEnabledChangedCallback
			(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
				((ComboBoxStandardCode)d).HintTextIsEmptyVisibilityUpdateValue();

		protected static void IsSetFocusChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
			((ComboBoxStandardCode)d).SetTextBoxInputOutputFocus();
		#endregion
		#endregion
	}
}