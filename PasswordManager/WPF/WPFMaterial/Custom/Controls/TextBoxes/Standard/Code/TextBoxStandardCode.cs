#region Namespaces Microsoft
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
#endregion


namespace WPFMaterial.Custom.Controls.TextBoxes.Standard.Code
{
	public class TextBoxStandardCode : TextBox
	{
		#region Fields
		#region Fields static interface
		public static readonly DependencyProperty
			IconProperty,
			HintFloatingProperty,
			HintIfTextIsEmptyIsEnabledProperty,
			HintIfTextIsEmptyVisibilityProperty,
			HintIfTextIsEmptyProperty,
			CounterIsEnabledProperty,
			CounterBrushProperty,
			CounterProperty,
			ClearIsEnabledProperty,
			IsSetFocusProperty,
			InvalidCharactersIsEnabledProperty;
		#endregion


		#region Fields static internal
		private static readonly DependencyPropertyKey
			HintIfTextIsEmptyVisibilityPropertyKey,
			CounterBrushPropertyKey,
			CounterPropertyKey;
		#endregion


		#region Fields internal
		protected TextBox
			_partTextBoxInputOutput;

		protected Button
			_partButtonClear;

		protected Brush
			_brushLowFilled = new SolidColorBrush(Color.FromRgb(0x00, 0xD8, 0x02)),
			_brushMadiumFilled = new SolidColorBrush(Color.FromRgb(0xDA, 0xA2, 0x0F)),
			_brushHighFilled = new SolidColorBrush(Color.FromRgb(0xFF, 0x00, 0x00));
		#endregion
		#endregion


		#region Constructors
		#region Constructor static internal
		static TextBoxStandardCode()
		{
			#region Dependency properties adding metadata
			TextProperty.OverrideMetadata
			(
				typeof(TextBoxStandardCode),
				new FrameworkPropertyMetadata
				(
					string.Empty,
					FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
					TextChangedCallback,
					null,
					true,
					UpdateSourceTrigger.PropertyChanged
				)
			);

			VisibilityProperty.OverrideMetadata
			(
				typeof(TextBoxStandardCode),
				new FrameworkPropertyMetadata(VisibilityChangedCallback)
			);

			DefaultStyleKeyProperty.OverrideMetadata
			(
				typeof(TextBoxStandardCode),
				new FrameworkPropertyMetadata(typeof(TextBoxStandardCode))
			);

			IsEnabledProperty.OverrideMetadata
			(
				typeof(TextBoxStandardCode),
				new UIPropertyMetadata(IsEnabledChangedCallback)
			);

			MaxLengthProperty.OverrideMetadata
			(
				typeof(TextBoxStandardCode),
				new FrameworkPropertyMetadata(MaxLengthChangedCallback)
			);
			#endregion


			#region Dependency properties registration
			IconProperty =
				DependencyProperty.Register
				(
					nameof(Icon),
					typeof(PathGeometry),
					typeof(TextBoxStandardCode)
				);

			HintFloatingProperty =
				DependencyProperty.Register
				(
					nameof(HintFloating),
					typeof(string),
					typeof(TextBoxStandardCode),
					new PropertyMetadata(string.Empty)
				);

			HintIfTextIsEmptyIsEnabledProperty =
				DependencyProperty.Register
				(
					nameof(HintIfTextIsEmptyIsEnabled),
					typeof(bool),
					typeof(TextBoxStandardCode),
					new PropertyMetadata(HintIfTextIsEmptyIsEnabledChangedCallback)
				);

			HintIfTextIsEmptyVisibilityPropertyKey =
				DependencyProperty.RegisterReadOnly
				(
					nameof(HintIfTextIsEmptyVisibility),
					typeof(Visibility),
					typeof(TextBoxStandardCode),
					new PropertyMetadata(Visibility.Hidden)
				);

			HintIfTextIsEmptyVisibilityProperty = HintIfTextIsEmptyVisibilityPropertyKey.DependencyProperty;

			HintIfTextIsEmptyProperty =
				DependencyProperty.Register
				(
					nameof(HintIfTextIsEmpty),
					typeof(string),
					typeof(TextBoxStandardCode),
					new PropertyMetadata("This field must not be empty!")
				);

			CounterIsEnabledProperty =
				DependencyProperty.Register
				(
					nameof(CounterIsEnabled),
					typeof(bool),
					typeof(TextBoxStandardCode),
					new PropertyMetadata(CounterIsEnabledChangedCallback)
				);

			CounterBrushPropertyKey =
				DependencyProperty.RegisterReadOnly
				(
					nameof(CounterBrush),
					typeof(Brush),
					typeof(TextBoxStandardCode),
					new PropertyMetadata()
				);

			CounterBrushProperty = CounterBrushPropertyKey.DependencyProperty;

			CounterPropertyKey =
				DependencyProperty.RegisterReadOnly
				(
					nameof(Counter),
					typeof(string),
					typeof(TextBoxStandardCode),
					new PropertyMetadata(string.Empty)
				);

			CounterProperty = CounterPropertyKey.DependencyProperty;

			IsSetFocusProperty =
				DependencyProperty.Register
				(
					nameof(IsSetFocus),
					typeof(bool),
					typeof(TextBoxStandardCode),
					new FrameworkPropertyMetadata
					(
						false,
						FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
						IsSetFocusChangedCallback
					)
				);

			InvalidCharactersIsEnabledProperty =
				DependencyProperty.Register
				(
					nameof(InvalidCharactersIsEnabled),
					typeof(bool),
					typeof(TextBoxStandardCode)
				);
			#endregion
		}
		#endregion


		#region Constructors interface
		public TextBoxStandardCode() =>
			Focusable = false;
		#endregion
		#endregion


		#region Properties infrastructure of registered dependency properties interface
		public PathGeometry Icon
		{
			get =>
				(PathGeometry)GetValue(IconProperty);

			set =>
				SetValue(IconProperty, value);
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

		public bool CounterIsEnabled
		{
			get =>
				(bool)GetValue(CounterIsEnabledProperty);

			set =>
				SetValue(CounterIsEnabledProperty, value);
		}

		public Brush CounterBrush
		{
			get =>
				(Brush)GetValue(CounterBrushProperty);

			protected set =>
				SetValue(CounterBrushPropertyKey, value);
		}

		public string Counter
		{
			get =>
				(string)GetValue(CounterProperty);

			protected set =>
				SetValue(CounterPropertyKey, value);
		}

		public bool IsSetFocus
		{
			get =>
				(bool)GetValue(IsSetFocusProperty);

			set =>
				SetValue(IsSetFocusProperty, value);
		}

		public bool InvalidCharactersIsEnabled
		{
			get =>
				(bool)GetValue(InvalidCharactersIsEnabledProperty);

			set =>
				SetValue(InvalidCharactersIsEnabledProperty, value);
		}
		#endregion


		#region Methods
		#region Methods internal
		protected virtual void IsEnabledSetTextBoxInputOutputFocus()
		{
			if (IsEnabled)
				SetTextBoxInputOutputFocus();
		}

		protected virtual void SetTextBoxInputOutputFocus()
		{
			if (IsSetFocus)
				_partTextBoxInputOutput.Focus();
		}

		protected virtual void HintTextIsEmptyVisibilityUpdateValue() =>
			HintIfTextIsEmptyVisibility =
				HintIfTextIsEmptyIsEnabled
				&& string.IsNullOrEmpty(Text)
				&& IsEnabled
					? Visibility.Visible
					: Visibility.Hidden;

		protected virtual void UpdateValueCounterDP() =>
			Counter = $"{Text.Length}/{MaxLength}";

		protected virtual void UpdateValueCounterStatusColorDP()
		{
			float
				percentageTextLengthToMaxLength = (float)Text.Length / MaxLength * 100;

			CounterBrush =
				percentageTextLengthToMaxLength <= 50
					? _brushLowFilled
					: percentageTextLengthToMaxLength <= 75
						? _brushMadiumFilled
						: _brushHighFilled;
		}

		protected virtual void UpdateValuesCounterDPAndCounterStatusColorDP()
		{
			if (CounterIsEnabled)
			{
				if (string.IsNullOrEmpty(Text))
				{
					Counter = string.Empty;
					CounterBrush = _brushLowFilled;
				}
				else
				{
					UpdateValueCounterDP();
					UpdateValueCounterStatusColorDP();
				}
			}
		}
		#endregion


		#region Methods callback in dependency properties internal
		protected static void TextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var
				thisControl = (TextBoxStandardCode)d;

			thisControl.HintTextIsEmptyVisibilityUpdateValue();
			thisControl.UpdateValuesCounterDPAndCounterStatusColorDP();
		}

		protected static void VisibilityChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{ }

		protected static void IsEnabledChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var
				thisControl = (TextBoxStandardCode)d;

			thisControl.IsEnabledSetTextBoxInputOutputFocus();
			thisControl.HintTextIsEmptyVisibilityUpdateValue();
			thisControl.UpdateValuesCounterDPAndCounterStatusColorDP();
		}

		protected static void HintIfTextIsEmptyIsEnabledChangedCallback
			(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
				((TextBoxStandardCode)d).HintTextIsEmptyVisibilityUpdateValue();

		protected static void CounterIsEnabledChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
			((TextBoxStandardCode)d).UpdateValuesCounterDPAndCounterStatusColorDP();

		protected static void IsSetFocusChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
			((TextBoxStandardCode)d).SetTextBoxInputOutputFocus();

		protected static void MaxLengthChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
			((TextBoxStandardCode)d).UpdateValuesCounterDPAndCounterStatusColorDP();
		#endregion


		#region Methods event handlers overridden interface
		public override void OnApplyTemplate()
		{
			_partTextBoxInputOutput = GetTemplateChild("PART_TextBox_InputOutput") as TextBox;
			_partButtonClear = GetTemplateChild("PART_Button_Clear") as Button;

			if (_partTextBoxInputOutput != null)
			{
				_partTextBoxInputOutput.GotFocus += TextBoxInputOutput_OnGotFocus;
				_partTextBoxInputOutput.LostFocus += TextBoxInputOutput_OnLostFocus;
			}

			if (_partButtonClear != null)
				_partButtonClear.Click += ButtonClear_OnClick;

			if (IsSetFocus)
				_partTextBoxInputOutput.Focus();
		}
		#endregion


		#region Methods event handlers internal
		protected virtual void TextBoxInputOutput_OnGotFocus(object sender, RoutedEventArgs e) =>
			IsSetFocus = true;

		protected virtual void TextBoxInputOutput_OnLostFocus(object sender, RoutedEventArgs e) =>
			IsSetFocus = false;

		protected virtual void ButtonClear_OnClick(object sender, RoutedEventArgs e)
		{
			if (_partTextBoxInputOutput != null)
			{
				_partTextBoxInputOutput.Clear();
				_partTextBoxInputOutput.Focus();
			}
		}
		#endregion
		#endregion
	}
}