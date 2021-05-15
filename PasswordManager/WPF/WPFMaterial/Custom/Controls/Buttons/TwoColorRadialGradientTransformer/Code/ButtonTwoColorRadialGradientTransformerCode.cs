#region Namespaces Microsoft
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#endregion


namespace WPFMaterial.Custom.Controls.Buttons.TwoColorRadialGradientTransformer.Code
{
	public class ButtonTwoColorRadialGradientTransformerCode : Button
	{
		#region Fields
		#region Fields static interface
		public static readonly DependencyProperty
			PrimarySideContentProperty,
			PrimarySideIconProperty,
			PrimarySideInnerColorRadialGradientBackgroundProperty,
			PrimarySideOuterColorRadialGradientBackgroundProperty,
			PrimarySideBorderBrushProperty,
			SecondarySideContentProperty,
			SecondarySideIconProperty,
			SecondarySideInnerColorRadialGradientBackgroundProperty,
			SecondarySideOuterColorRadialGradientBackgroundProperty,
			SecondarySideBorderBrushProperty,
			IconProperty,
			InnerColorRadialGradientBackgroundProperty,
			OuterColorRadialGradientBackgroundProperty,
			IsShowSecondarySideProperty,
			IsEnabledHotkeysToolTipProperty,
			HotkeysProperty;

		new public static readonly DependencyProperty
			BorderBrushProperty;
		#endregion


		#region Fields static internal
		private static readonly DependencyPropertyKey
			ReceivedIsDefaultedPropertyKey,
			ReceivedIsFocusedPropertyKey;

		private static readonly DependencyPropertyKey
			IconPropertyKey,
			InnerColorRadialGradientBackgroundPropertyKey,
			OuterColorRadialGradientBackgroundPropertyKey,
			BorderBrushPropertyKey,
			HotkeysPropertyKey;
		#endregion


		#region Fields internal
		protected char
			_hotkeyCharacter;
		#endregion
		#endregion


		#region Constructor static internal
		static ButtonTwoColorRadialGradientTransformerCode()
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
				typeof(ButtonTwoColorRadialGradientTransformerCode),
				new FrameworkPropertyMetadata(SearchHotkeyCharacterInContentAndUpdateValueHotkeysDP)
			);

			DefaultStyleKeyProperty.OverrideMetadata
			(
				typeof(ButtonTwoColorRadialGradientTransformerCode),
				new FrameworkPropertyMetadata(typeof(ButtonTwoColorRadialGradientTransformerCode))
			);

			ReceivedIsDefaultedPropertyKey.OverrideMetadata
			(
				typeof(ButtonTwoColorRadialGradientTransformerCode),
				new FrameworkPropertyMetadata(UpdateValueHotkeysDP)
			);

			ReceivedIsFocusedPropertyKey.OverrideMetadata
			(
				typeof(ButtonTwoColorRadialGradientTransformerCode),
				new PropertyMetadata(UpdateValueHotkeysDP)
			);
			#endregion


			#region Dependency properties registration
			PrimarySideContentProperty =
				DependencyProperty.Register
				(
					nameof(PrimarySideContent),
					typeof(object),
					typeof(ButtonTwoColorRadialGradientTransformerCode)
				);

			PrimarySideIconProperty =
				DependencyProperty.Register
				(
					nameof(PrimarySideIcon),
					typeof(PathGeometry),
					typeof(ButtonTwoColorRadialGradientTransformerCode)
				);

			PrimarySideInnerColorRadialGradientBackgroundProperty =
				DependencyProperty.Register
				(
					nameof(PrimarySideInnerColorRadialGradientBackground),
					typeof(Color),
					typeof(ButtonTwoColorRadialGradientTransformerCode)
				);

			PrimarySideOuterColorRadialGradientBackgroundProperty =
				DependencyProperty.Register
				(
					nameof(PrimarySideOuterColorRadialGradientBackground),
					typeof(Color),
					typeof(ButtonTwoColorRadialGradientTransformerCode)
				);

			PrimarySideBorderBrushProperty =
				DependencyProperty.Register
				(
					nameof(PrimarySideBorderBrush),
					typeof(Brush),
					typeof(ButtonTwoColorRadialGradientTransformerCode)
				);

			SecondarySideContentProperty =
				DependencyProperty.Register
				(
					nameof(SecondarySideContent),
					typeof(object),
					typeof(ButtonTwoColorRadialGradientTransformerCode)
				);

			SecondarySideIconProperty =
				DependencyProperty.Register
				(
					nameof(SecondarySideIcon),
					typeof(PathGeometry),
					typeof(ButtonTwoColorRadialGradientTransformerCode)
				);

			SecondarySideInnerColorRadialGradientBackgroundProperty =
				DependencyProperty.Register
				(
					nameof(SecondarySideInnerColorRadialGradientBackground),
					typeof(Color),
					typeof(ButtonTwoColorRadialGradientTransformerCode)
				);

			SecondarySideOuterColorRadialGradientBackgroundProperty =
				DependencyProperty.Register
				(
					nameof(SecondarySideOuterColorRadialGradientBackground),
					typeof(Color),
					typeof(ButtonTwoColorRadialGradientTransformerCode)
				);

			SecondarySideBorderBrushProperty =
				DependencyProperty.Register
				(
					nameof(SecondarySideBorderBrush),
					typeof(Brush),
					typeof(ButtonTwoColorRadialGradientTransformerCode)
				);

			IconPropertyKey =
				DependencyProperty.RegisterReadOnly
				(
					nameof(Icon),
					typeof(PathGeometry),
					typeof(ButtonTwoColorRadialGradientTransformerCode),
					new PropertyMetadata()
				);

			IconProperty = IconPropertyKey.DependencyProperty;

			InnerColorRadialGradientBackgroundPropertyKey =
				DependencyProperty.RegisterReadOnly
				(
					nameof(InnerColorRadialGradientBackground),
					typeof(Color),
					typeof(ButtonTwoColorRadialGradientTransformerCode),
					new PropertyMetadata()
				);

			InnerColorRadialGradientBackgroundProperty =
				InnerColorRadialGradientBackgroundPropertyKey.DependencyProperty;

			OuterColorRadialGradientBackgroundPropertyKey =
				DependencyProperty.RegisterReadOnly
				(
					nameof(OuterColorRadialGradientBackground),
					typeof(Color),
					typeof(ButtonTwoColorRadialGradientTransformerCode),
					new PropertyMetadata()
				);

			OuterColorRadialGradientBackgroundProperty =
				OuterColorRadialGradientBackgroundPropertyKey.DependencyProperty;

			BorderBrushPropertyKey =
				DependencyProperty.RegisterReadOnly
				(
					nameof(BorderBrush),
					typeof(Brush),
					typeof(ButtonTwoColorRadialGradientTransformerCode),
					new PropertyMetadata()
				);

			BorderBrushProperty = BorderBrushPropertyKey.DependencyProperty;

			IsShowSecondarySideProperty =
				DependencyProperty.Register
				(
					nameof(IsShowSecondarySide),
					typeof(bool),
					typeof(ButtonTwoColorRadialGradientTransformerCode),
					new PropertyMetadata(ShowOppositeSide)
				);

			IsEnabledHotkeysToolTipProperty =
				DependencyProperty.Register
				(
					nameof(IsEnabledHotkeysToolTip),
					typeof(bool),
					typeof(ButtonTwoColorRadialGradientTransformerCode),
					new PropertyMetadata(true, UpdateValueHotkeysDP)
				);

			HotkeysPropertyKey =
				DependencyProperty.RegisterReadOnly
				(
					nameof(Hotkeys),
					typeof(string),
					typeof(ButtonTwoColorRadialGradientTransformerCode),
					new PropertyMetadata()
				);

			HotkeysProperty = HotkeysPropertyKey.DependencyProperty;
			#endregion
		}
		#endregion


		#region Properties infrastructure of registered dependency properties interface
		public object PrimarySideContent
		{
			get =>
				GetValue(PrimarySideContentProperty);

			set =>
				SetValue(PrimarySideContentProperty, value);
		}

		public PathGeometry PrimarySideIcon
		{
			get =>
				(PathGeometry)GetValue(PrimarySideIconProperty);

			set =>
				SetValue(PrimarySideIconProperty, value);
		}

		public Color PrimarySideInnerColorRadialGradientBackground
		{
			get =>
				(Color)GetValue(PrimarySideInnerColorRadialGradientBackgroundProperty);

			set =>
				SetValue(PrimarySideInnerColorRadialGradientBackgroundProperty, value);
		}

		public Color PrimarySideOuterColorRadialGradientBackground
		{
			get =>
				(Color)GetValue(PrimarySideOuterColorRadialGradientBackgroundProperty);

			set =>
				SetValue(PrimarySideOuterColorRadialGradientBackgroundProperty, value);
		}

		public Brush PrimarySideBorderBrush
		{
			get =>
				(Brush)GetValue(PrimarySideBorderBrushProperty);

			set =>
				SetValue(PrimarySideBorderBrushProperty, value);
		}

		public object SecondarySideContent
		{
			get =>
				GetValue(SecondarySideContentProperty);

			set =>
				SetValue(SecondarySideContentProperty, value);
		}

		public PathGeometry SecondarySideIcon
		{
			get =>
				(PathGeometry)GetValue(SecondarySideIconProperty);

			set =>
				SetValue(SecondarySideIconProperty, value);
		}

		public Color SecondarySideInnerColorRadialGradientBackground
		{
			get =>
				(Color)GetValue(SecondarySideInnerColorRadialGradientBackgroundProperty);

			set =>
				SetValue(SecondarySideInnerColorRadialGradientBackgroundProperty, value);
		}

		public Color SecondarySideOuterColorRadialGradientBackground
		{
			get =>
				(Color)GetValue(SecondarySideOuterColorRadialGradientBackgroundProperty);

			set =>
				SetValue(SecondarySideOuterColorRadialGradientBackgroundProperty, value);
		}

		public Brush SecondarySideBorderBrush
		{
			get =>
				(Brush)GetValue(SecondarySideBorderBrushProperty);

			set =>
				SetValue(SecondarySideBorderBrushProperty, value);
		}

		public PathGeometry Icon
		{
			get =>
				(PathGeometry)GetValue(IconProperty);

			protected set =>
				SetValue(IconPropertyKey, value);
		}

		public Color InnerColorRadialGradientBackground
		{
			get =>
				(Color)GetValue(InnerColorRadialGradientBackgroundProperty);

			protected set =>
				SetValue(InnerColorRadialGradientBackgroundPropertyKey, value);
		}

		public Color OuterColorRadialGradientBackground
		{
			get =>
				(Color)GetValue(OuterColorRadialGradientBackgroundProperty);

			protected set =>
				SetValue(OuterColorRadialGradientBackgroundPropertyKey, value);
		}

		new public Brush BorderBrush
		{
			get =>
				(Brush)GetValue(BorderBrushProperty);

			protected set =>
				SetValue(BorderBrushPropertyKey, value);
		}

		public bool IsShowSecondarySide
		{
			get =>
				(bool)GetValue(IsShowSecondarySideProperty);

			set =>
				SetValue(IsShowSecondarySideProperty, value);
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
		protected void ShowPrimarySide()
		{
			if (PrimarySideContent != null)
				Content = PrimarySideContent;

			if (PrimarySideIcon != null)
				Icon = PrimarySideIcon;

			if (PrimarySideInnerColorRadialGradientBackground != default)
				InnerColorRadialGradientBackground = PrimarySideInnerColorRadialGradientBackground;

			if (PrimarySideOuterColorRadialGradientBackground != default)
				OuterColorRadialGradientBackground = PrimarySideOuterColorRadialGradientBackground;

			if (PrimarySideBorderBrush != default)
				BorderBrush = PrimarySideBorderBrush;
		}

		protected void ShowSecondarySide()
		{
			if (SecondarySideContent != null)
				Content = SecondarySideContent;

			if (SecondarySideIcon != null)
				Icon = SecondarySideIcon;

            if (SecondarySideInnerColorRadialGradientBackground != default)
                InnerColorRadialGradientBackground = SecondarySideInnerColorRadialGradientBackground;

			if (SecondarySideOuterColorRadialGradientBackground != default)
				OuterColorRadialGradientBackground = SecondarySideOuterColorRadialGradientBackground;

			if (SecondarySideBorderBrush != default)
				BorderBrush = SecondarySideBorderBrush;
		}
		#endregion


		#region Methods callback in dependency properties internal
		//'Property'.
		protected static void SearchHotkeyCharacterInContentAndUpdateValueHotkeysDP
			(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var
				thisControl = (ButtonTwoColorRadialGradientTransformerCode)d;

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
				thisControl = (ButtonTwoColorRadialGradientTransformerCode)d;

			thisControl.Hotkeys =
				thisControl.IsEnabledHotkeysToolTip
					? thisControl.IsFocused
						? thisControl._hotkeyCharacter != default
							? $" (Enter/Space/Alt+{thisControl._hotkeyCharacter})"
							: " (Enter/Space)"
						: thisControl.IsDefaulted
							? thisControl._hotkeyCharacter != default
								? $" (Enter/Alt+{thisControl._hotkeyCharacter})"
								: " (Enter)"
							: thisControl._hotkeyCharacter != default
								? $" (Alt+{thisControl._hotkeyCharacter})"
								: string.Empty
					: string.Empty;
		}

		protected static void ShowOppositeSide(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var
				thisControl = (ButtonTwoColorRadialGradientTransformerCode)d;

			if (thisControl.IsShowSecondarySide)
				thisControl.ShowSecondarySide();
			else
				thisControl.ShowPrimarySide();
		}
		#endregion


		#region Methods event handlers overridden interface
		public override void OnApplyTemplate()
		{
			if (IsShowSecondarySide)
				ShowSecondarySide();
			else
				ShowPrimarySide();
		}
		#endregion
		#endregion
	}
}