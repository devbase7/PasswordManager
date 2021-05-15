#region Namespaces Microsoft
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
#endregion


namespace WPFMaterial.Custom.Controls.Buttons.ChromeWindowManagement.Code
{
	public class ButtonChromeWindowManagementCode : Button
	{
		#region Fields
		#region Fields static interface
		public static readonly DependencyProperty
			CornerRadiusProperty,
			BackgroundVisualStateMouseOverProperty,
			BackgroundVisualStatePressedProperty,
			IconProperty,
			ToolTipTextProperty,
			ToolTipHotkeysProperty,
			ToolTipPaddingProperty,
			ToolTipContentProperty;
		#endregion


		#region Fields static internal
		private static readonly DependencyPropertyKey
			ReceivedIsDefaultedPropertyKey;

		private static readonly DependencyPropertyKey
			ToolTipContentPropertyKey;
		#endregion


		#region Fields internal
		protected TextBlock
			_textBlockToolTipText;

		protected Run
			_runToolTipHotkeys;
		#endregion
		#endregion


		#region Constructor static internal
		static ButtonChromeWindowManagementCode()
		{
			#region Dependency properties getting keys
			ReceivedIsDefaultedPropertyKey =
				(DependencyPropertyKey)typeof(Button).GetField
				(
					"IsDefaultedPropertyKey",
					BindingFlags.DeclaredOnly
					| BindingFlags.NonPublic
					| BindingFlags.Static
				).GetValue(null);
			#endregion


			#region Dependency properties adding metadata
			DefaultStyleKeyProperty.OverrideMetadata
			(
				typeof(ButtonChromeWindowManagementCode),
				new FrameworkPropertyMetadata(typeof(ButtonChromeWindowManagementCode))
			);
			#endregion


			#region Dependency properties registration
			CornerRadiusProperty =
				DependencyProperty.Register
				(
					nameof(CornerRadius),
					typeof(CornerRadius),
					typeof(ButtonChromeWindowManagementCode)
				);

			BackgroundVisualStateMouseOverProperty =
				DependencyProperty.Register
				(
					nameof(BackgroundVisualStateMouseOver),
					typeof(Brush),
					typeof(ButtonChromeWindowManagementCode)
				);

			BackgroundVisualStatePressedProperty =
				DependencyProperty.Register
				(
					nameof(BackgroundVisualStatePressed),
					typeof(Brush),
					typeof(ButtonChromeWindowManagementCode)
				);

			IconProperty =
				DependencyProperty.Register
				(
					nameof(Icon),
					typeof(PathGeometry),
					typeof(ButtonChromeWindowManagementCode)
				);

			ToolTipTextProperty =
				DependencyProperty.Register
				(
					nameof(ToolTipText),
					typeof(string),
					typeof(ButtonChromeWindowManagementCode),
					new PropertyMetadata(CallbackMethodChangeToolTipText)
				);

			ToolTipHotkeysProperty =
				DependencyProperty.Register
				(
					nameof(ToolTipHotkeys),
					typeof(string),
					typeof(ButtonChromeWindowManagementCode),
					new PropertyMetadata(CallbackMethodChangeToolTipHotkeys)
				);

			ToolTipPaddingProperty =
				DependencyProperty.Register
				(
					nameof(ToolTipPadding),
					typeof(Thickness),
					typeof(ButtonChromeWindowManagementCode)
				);

			ToolTipContentPropertyKey =
				DependencyProperty.RegisterReadOnly
				(
					nameof(ToolTipContent),
					typeof(object),
					typeof(ButtonChromeWindowManagementCode),
					new PropertyMetadata()
				);

			ToolTipContentProperty = ToolTipContentPropertyKey.DependencyProperty;
			#endregion
		}
		#endregion


		#region Properties infrastructure of registered dependency properties interface
		public CornerRadius CornerRadius
		{
			get =>
				(CornerRadius)GetValue(CornerRadiusProperty);

			set =>
				SetValue(CornerRadiusProperty, value);
		}

		public Brush BackgroundVisualStateMouseOver
		{
			get =>
				(Brush)GetValue(BackgroundVisualStateMouseOverProperty);

			set =>
				SetValue(BackgroundVisualStateMouseOverProperty, value);
		}

		public Brush BackgroundVisualStatePressed
		{
			get =>
				(Brush)GetValue(BackgroundVisualStatePressedProperty);

			set =>
				SetValue(BackgroundVisualStatePressedProperty, value);
		}

		public PathGeometry Icon
		{
			get =>
				(PathGeometry)GetValue(IconProperty);

			set =>
				SetValue(IconProperty, value);
		}

		public string ToolTipText
        {
            get =>
                (string)GetValue(ToolTipTextProperty);

            set =>
                SetValue(ToolTipTextProperty, value);
        }

		public string ToolTipHotkeys
		{
			get =>
				(string)GetValue(ToolTipHotkeysProperty);

			set =>
				SetValue(ToolTipHotkeysProperty, value);
		}

		public object ToolTipContent
		{
			get =>
				GetValue(ToolTipContentProperty);

			protected set =>
				SetValue(ToolTipContentPropertyKey, value);
		}

		public Thickness ToolTipPadding
		{
			get =>
				(Thickness)GetValue(ToolTipPaddingProperty);

			set =>
				SetValue(ToolTipPaddingProperty, value);
		}
		#endregion


		#region Methods
		#region Methods internal
		protected virtual void SetToolTipContentIfToolTipTextChanged()
		{
			if (string.IsNullOrEmpty(ToolTipText))
			{
				if (string.IsNullOrEmpty(ToolTipHotkeys))
				{
					GarbageCollectorReferenceType(ToolTipContent);
					GarbageCollectorReferenceType(_textBlockToolTipText);
				}
			}
			else
			{
				if (string.IsNullOrEmpty(ToolTipHotkeys))
					ToolTipContent = _textBlockToolTipText = new TextBlock
					{
						Text = ToolTipText
					};
				else
				{
					_textBlockToolTipText = new TextBlock
					{
						Text = ToolTipText
					};

					_textBlockToolTipText.Inlines.Add
					(
						_runToolTipHotkeys =
							new Run
							{
								Text = $" {ToolTipHotkeys}",
								FontFamily = new FontFamily("Segoe UI Bold"),
								Foreground = new SolidColorBrush(Colors.Red)
							}
					);

					ToolTipContent = _textBlockToolTipText;
				}
			}
		}

		protected virtual void SetToolTipContentIfToolTipHotkeysChanged()
		{
			if (string.IsNullOrEmpty(ToolTipHotkeys))
			{
				if (string.IsNullOrEmpty(ToolTipText))
				{
					GarbageCollectorReferenceType(ToolTipContent);
					GarbageCollectorReferenceType(_textBlockToolTipText);
				}
			}
			else
			{
				if (string.IsNullOrEmpty(ToolTipText))
				{
					_textBlockToolTipText = new TextBlock();

					_textBlockToolTipText.Inlines.Add
					(
						_runToolTipHotkeys =
							new Run
							{
								Text = ToolTipHotkeys,
								FontFamily = new FontFamily("Segoe UI Bold"),
								Foreground = new SolidColorBrush(Colors.Red)
							}
					);

					ToolTipContent = _textBlockToolTipText;
				}
				else
				{
					_textBlockToolTipText.Inlines.Add
					(
						_runToolTipHotkeys =
							new Run
							{
								Text = $" {ToolTipHotkeys}",
								FontFamily = new FontFamily("Segoe UI Bold"),
								Foreground = new SolidColorBrush(Colors.Red)
							}
					);
				}
			}
		}
		#endregion


		#region Methods callback in dependency properties internal
		protected static void CallbackMethodChangeToolTipText(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
			((ButtonChromeWindowManagementCode)d).SetToolTipContentIfToolTipTextChanged();

		protected static void CallbackMethodChangeToolTipHotkeys
			(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
				((ButtonChromeWindowManagementCode)d).SetToolTipContentIfToolTipHotkeysChanged();
		#endregion
		#endregion


		#region Garbage collector
		protected void GarbageCollectorReferenceType<T>(T referenceType)
			where T : class =>
				referenceType = null;
		#endregion
	}
}