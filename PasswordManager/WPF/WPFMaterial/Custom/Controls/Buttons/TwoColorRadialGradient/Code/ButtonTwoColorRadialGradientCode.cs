#region Namespaces Microsoft
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#endregion


namespace WPFMaterial.Custom.Controls.Buttons.TwoColorRadialGradient.Code
{
	public class ButtonTwoColorRadialGradientCode : Button
	{
		#region Fields
		#region Fields static interface
		public static readonly DependencyProperty
			IconProperty,
			InnerColorRadialGradientBackgroundProperty,
			OuterColorRadialGradientBackgroundProperty,
			IsEnabledHotkeysToolTipProperty,
			HotkeysProperty;
		#endregion


		#region Fields static internal
		private static readonly DependencyPropertyKey
			ReceivedIsDefaultedPropertyKey,
			ReceivedIsFocusedPropertyKey;

		private static readonly DependencyPropertyKey
			HotkeysPropertyKey;
		#endregion


		#region Fields internal
		protected char
			_hotkeyCharacter;

		private bool
			_isHotkeyCharacterNotDefault;
		#endregion
		#endregion


		#region Constructor static internal
		static ButtonTwoColorRadialGradientCode()
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
			ContentProperty.OverrideMetadata
			(
				typeof(ButtonTwoColorRadialGradientCode),
				new FrameworkPropertyMetadata(SearchHotkeyCharacterInContentAndUpdateValueHotkeysDPCallback)
			);

			DefaultStyleKeyProperty.OverrideMetadata
			(
				typeof(ButtonTwoColorRadialGradientCode),
				new FrameworkPropertyMetadata(typeof(ButtonTwoColorRadialGradientCode))
			);

			ReceivedIsDefaultedPropertyKey.OverrideMetadata
			(
				typeof(ButtonTwoColorRadialGradientCode),
				new FrameworkPropertyMetadata(UpdateValueHotkeysDPCallback)
			);

			ReceivedIsFocusedPropertyKey.OverrideMetadata
			(
				typeof(ButtonTwoColorRadialGradientCode),
				new PropertyMetadata(UpdateValueHotkeysDPCallback)
			);
			#endregion


			#region Dependency properties registration
			IconProperty =
				DependencyProperty.Register
				(
					nameof(Icon),
					typeof(PathGeometry),
					typeof(ButtonTwoColorRadialGradientCode)
				);

			InnerColorRadialGradientBackgroundProperty =
				DependencyProperty.Register
				(
					nameof(InnerColorRadialGradientBackground),
					typeof(Color),
					typeof(ButtonTwoColorRadialGradientCode)
				);

			OuterColorRadialGradientBackgroundProperty =
				DependencyProperty.Register
				(
					nameof(OuterColorRadialGradientBackground),
					typeof(Color),
					typeof(ButtonTwoColorRadialGradientCode)
				);

			IsEnabledHotkeysToolTipProperty =
				DependencyProperty.Register
				(
					nameof(IsEnabledHotkeysToolTip),
					typeof(bool),
					typeof(ButtonTwoColorRadialGradientCode),
					new PropertyMetadata
					(
						true,
						UpdateValueHotkeysDPCallback
					)
				);

			HotkeysPropertyKey =
				DependencyProperty.RegisterReadOnly
				(
					nameof(Hotkeys),
					typeof(string),
					typeof(ButtonTwoColorRadialGradientCode),
					new PropertyMetadata()
				);

			HotkeysProperty = HotkeysPropertyKey.DependencyProperty;
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

		public Color InnerColorRadialGradientBackground
		{
			get =>
				(Color)GetValue(InnerColorRadialGradientBackgroundProperty);

			set =>
				SetValue(InnerColorRadialGradientBackgroundProperty, value);
		}

		public Color OuterColorRadialGradientBackground
		{
			get =>
				(Color)GetValue(OuterColorRadialGradientBackgroundProperty);

			set =>
				SetValue(OuterColorRadialGradientBackgroundProperty, value);
		}

		public bool IsEnabledHotkeysToolTip
		{
			get =>
				(bool)GetValue(IsEnabledHotkeysToolTipProperty);

			set =>
				SetValue(IsEnabledHotkeysToolTipProperty, value);
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
		protected virtual void SearchHotkeyCharacterInContent()
		{
			_hotkeyCharacter = default;

			if (Content != null)
			{
				string
					arrayOfCharacters = Content.ToString();

				int
					n = arrayOfCharacters.IndexOf('_');

				if (n > -1)
					_hotkeyCharacter = char.ToUpper(arrayOfCharacters[n + 1]);
			}

            _isHotkeyCharacterNotDefault = _hotkeyCharacter != default;
		}

		protected virtual void UpdateValueHotkeysDP() =>
			Hotkeys =
				IsEnabledHotkeysToolTip
					? IsFocused
						? _isHotkeyCharacterNotDefault
							? $" (Enter/Space/Alt+{_hotkeyCharacter})"
							: " (Enter/Space)"
						: IsDefaulted
							? _isHotkeyCharacterNotDefault
								? $" (Enter/Alt+{_hotkeyCharacter})"
								: " (Enter)"
							: _isHotkeyCharacterNotDefault
								? $" (Alt+{_hotkeyCharacter})"
								: string.Empty
					: string.Empty;
		#endregion


		#region Methods callback in dependency properties internal
		protected static void SearchHotkeyCharacterInContentAndUpdateValueHotkeysDPCallback
			(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var
				thisControl = (ButtonTwoColorRadialGradientCode)d;

			thisControl.SearchHotkeyCharacterInContent();
			thisControl.UpdateValueHotkeysDP();
		}

		protected static void UpdateValueHotkeysDPCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
			((ButtonTwoColorRadialGradientCode)d).UpdateValueHotkeysDP();
		#endregion
		#endregion
	}
}