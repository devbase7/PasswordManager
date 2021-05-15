#region Namespaces Microsoft
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
#endregion


namespace WPFMaterial.Custom.Controls.CheckBoxes.Standard.Code
{
	public class CheckBoxStandardCode : CheckBox
	{
		#region Fields
		#region Fields static interface
		public static readonly DependencyProperty
			IsEnabledHotkeysToolTipProperty,
			HotkeysProperty;
		#endregion


		#region Fields static internal
		private static readonly DependencyPropertyKey
			ReceivedIsFocusedPropertyKey;

		private static readonly DependencyPropertyKey
			HotkeysPropertyKey;
		#endregion


		#region Fields internal
		protected char
			_hotkeyCharacter;
		#endregion
		#endregion


		#region Constructor static internal
		static CheckBoxStandardCode()
		{
			#region Dependency properties getting keys
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
				typeof(CheckBoxStandardCode),
				new FrameworkPropertyMetadata(SearchHotkeyCharacterInContentAndUpdateValueHotkeysDP)
			);

			DefaultStyleKeyProperty.OverrideMetadata
			(
				typeof(CheckBoxStandardCode),
				new FrameworkPropertyMetadata(typeof(CheckBoxStandardCode))
			);

			ReceivedIsFocusedPropertyKey.OverrideMetadata
			(
				typeof(CheckBoxStandardCode),
				new PropertyMetadata(UpdateValueHotkeysDP)
			);
			#endregion


			#region Dependency properties registration
			IsEnabledHotkeysToolTipProperty =
				DependencyProperty.Register
				(
					nameof(IsEnabledHotkeysToolTip),
					typeof(bool),
					typeof(CheckBoxStandardCode),
					new PropertyMetadata(true, UpdateValueHotkeysDP)
				);

			HotkeysPropertyKey =
				DependencyProperty.RegisterReadOnly
				(
					nameof(Hotkeys),
					typeof(string),
					typeof(CheckBoxStandardCode),
					new PropertyMetadata()
				);

			HotkeysProperty = HotkeysPropertyKey.DependencyProperty;
			#endregion
		}
		#endregion


		#region Properties infrastructure of registered dependency properties interface
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


		#region Methods callback in dependency properties internal
		protected static void SearchHotkeyCharacterInContentAndUpdateValueHotkeysDP
			(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var
				thisControl = (CheckBoxStandardCode)d;

			thisControl._hotkeyCharacter = default;

			if (thisControl.Content != null)
			{
				string
					arrayOfCharacters = thisControl.Content.ToString();

				int
					n = arrayOfCharacters.IndexOf('_');

				if (n > -1)
					thisControl._hotkeyCharacter = char.ToUpper(arrayOfCharacters[n + 1]);
			}

			UpdateValueHotkeysDP(d, e);
		}

		protected static void UpdateValueHotkeysDP(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var
				thisControl = (CheckBoxStandardCode)d;

			thisControl.Hotkeys =
				thisControl.IsEnabledHotkeysToolTip
					? thisControl.IsFocused
						? thisControl._hotkeyCharacter != default
							? $" (Space/Alt+{thisControl._hotkeyCharacter})"
							: " (Space)"
						: thisControl._hotkeyCharacter != default
							? $" (Alt+{thisControl._hotkeyCharacter})"
							: string.Empty
					: string.Empty;
		}
		#endregion
	}
}