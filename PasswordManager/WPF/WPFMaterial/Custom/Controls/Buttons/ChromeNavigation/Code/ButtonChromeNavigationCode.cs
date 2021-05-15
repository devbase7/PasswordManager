#region Namespaces Microsoft
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#endregion


namespace WPFMaterial.Custom.Controls.Buttons.ChromeNavigation.Code
{
	public class ButtonChromeNavigationCode : Button
	{
		#region Fields
		#region Fields static interface
		public static readonly DependencyProperty
			CornerRadiusProperty,
			IconProperty,
			IsEnabledHotkeysProperty,
			HotkeysProperty;
		#endregion


		#region Fields static internal
		private static readonly DependencyPropertyKey
			ReceivedIsDefaultedPropertyKey,
			ReceivedIsFocusedPropertyKey;

		private static readonly DependencyPropertyKey
			HotkeysPropertyKey;
		#endregion
		#endregion


		#region Constructor static internal
		static ButtonChromeNavigationCode()
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

			ReceivedIsFocusedPropertyKey =
				(DependencyPropertyKey)typeof(UIElement).GetField
				(
					"IsFocusedPropertyKey",
					BindingFlags.DeclaredOnly
					| BindingFlags.NonPublic
					| BindingFlags.Static
				).GetValue(null);
			#endregion


			#region Dependency properties adding metadata
			IsCancelProperty.OverrideMetadata
			(
				typeof(ButtonChromeNavigationCode),
				new FrameworkPropertyMetadata(IsCancelChangeCallback)
			);

			DefaultStyleKeyProperty.OverrideMetadata
			(
				typeof(ButtonChromeNavigationCode),
				new FrameworkPropertyMetadata(typeof(ButtonChromeNavigationCode))
			);

			ReceivedIsDefaultedPropertyKey.OverrideMetadata
			(
				typeof(ButtonChromeNavigationCode),
				new FrameworkPropertyMetadata(ReceivedIsDefaultedChangeCallback)
			);

			ReceivedIsFocusedPropertyKey.OverrideMetadata
			(
				typeof(ButtonChromeNavigationCode),
				new PropertyMetadata(ReceivedIsFocusedChangeCallback)
			);
			#endregion


			#region Dependency properties registration
			CornerRadiusProperty =
				DependencyProperty.Register
				(
					nameof(CornerRadius),
					typeof(CornerRadius),
					typeof(ButtonChromeNavigationCode)
				);

			IconProperty =
				DependencyProperty.Register
				(
					nameof(Icon),
					typeof(PathGeometry),
					typeof(ButtonChromeNavigationCode)
				);

			IsEnabledHotkeysProperty =
				DependencyProperty.Register
				(
					nameof(IsEnabledHotkeys),
					typeof(bool),
					typeof(ButtonChromeNavigationCode),
					new PropertyMetadata
					(
						true,
						IsEnabledHotkeysChangeCallback
					)
				);

			HotkeysPropertyKey =
				DependencyProperty.RegisterReadOnly
				(
					nameof(Hotkeys),
					typeof(string),
					typeof(ButtonChromeNavigationCode),
					new PropertyMetadata()
				);

			HotkeysProperty = HotkeysPropertyKey.DependencyProperty;
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

		public PathGeometry Icon
		{
			get =>
				(PathGeometry)GetValue(IconProperty);

			set =>
				SetValue(IconProperty, value);
		}

		public bool IsEnabledHotkeys
		{
			get =>
				(bool)GetValue(IsEnabledHotkeysProperty);

			set =>
				SetValue(IsEnabledHotkeysProperty, value);
		}

		public string Hotkeys
		{
			get =>
				(string)GetValue(HotkeysProperty);

			protected set =>
				SetValue(HotkeysPropertyKey, value);
		}
		#endregion


		#region Methods
		#region Methods internal
		protected virtual void UpdateValueHotkeysDP()
		{
			string
				hotkeysValue = string.Empty;

			if (!IsEnabledHotkeys)
			{
				Hotkeys = string.Empty;

				return;
			}

			if (IsCancel)
				hotkeysValue +=
					IsDefaulted
					|| IsFocused
						? "(Esc"
						: "(Esc)";

			if (IsDefaulted)
				hotkeysValue +=
					IsCancel
						? "/Enter)"
						: "(Enter)";

			if (IsFocused)
				hotkeysValue +=
					IsCancel
						? "/Enter/Space)"
						: "(Enter/Space)";

			Hotkeys = hotkeysValue;
		}
		#endregion


		#region Methods callback in dependency properties internal
		protected static void IsCancelChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
			((ButtonChromeNavigationCode)d).UpdateValueHotkeysDP();

		protected static void ReceivedIsDefaultedChangeCallback
			(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
				((ButtonChromeNavigationCode)d).UpdateValueHotkeysDP();

		protected static void ReceivedIsFocusedChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
			((ButtonChromeNavigationCode)d).UpdateValueHotkeysDP();

		protected static void IsEnabledHotkeysChangeCallback
			(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
				((ButtonChromeNavigationCode)d).UpdateValueHotkeysDP();
		#endregion
		#endregion
	}
}