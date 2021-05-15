#region Namespaces Microsoft
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#endregion


namespace WPFMaterial.Custom.Controls.Labels.ConnectorCorrespond.Code
{
	public class LabelConnectorCorrespondCode : Label
	{
		#region Fields
		#region Fields static interface
		public static readonly DependencyProperty
			FirstStringValueToCorrespondProperty,
			SecondStringValueToCorrespondProperty,
			CorrespondIconProperty,
			NotCorrespondIconProperty,
			IconProperty,
			CorrespondIconFillProperty,
			NotCorrespondIconFillProperty,
			IconFillProperty,
			ConnectionBrushProperty,
			IsEnabledStatusHintProperty,
			CorrespondStatusHintProperty,
			NotCorrespondStatusHintProperty,
			StatusHintProperty;
		#endregion


		#region Fields static internal
		private static readonly DependencyPropertyKey
			IconPropertyKey,
			IconFillPropertyKey,
			StatusHintPropertyKey;
		#endregion
		#endregion


		#region Constructor static internal
		static LabelConnectorCorrespondCode()
		{
			#region Dependency properties adding metadata
			VisibilityProperty.OverrideMetadata
			(
				typeof(LabelConnectorCorrespondCode),
				new PropertyMetadata(Visibility.Hidden)
			);

			DefaultStyleKeyProperty.OverrideMetadata
			(
				typeof(LabelConnectorCorrespondCode),
				new FrameworkPropertyMetadata(typeof(LabelConnectorCorrespondCode))
			);
			#endregion


			#region Dependency properties registration
			FirstStringValueToCorrespondProperty =
				DependencyProperty.Register
				(
					nameof(FirstStringValueToCorrespond),
					typeof(string),
					typeof(LabelConnectorCorrespondCode),
					new PropertyMetadata
					(
						string.Empty,
						UpdateValuesIconPathGeometryDPAndIconFillDPAndStatusHintDPAndVisibilityDPCallback
					)
				);

			SecondStringValueToCorrespondProperty =
				DependencyProperty.Register
				(
					nameof(SecondStringValueToCorrespond),
					typeof(string),
					typeof(LabelConnectorCorrespondCode),
					new PropertyMetadata
					(
						string.Empty,
						UpdateValuesIconPathGeometryDPAndIconFillDPAndStatusHintDPAndVisibilityDPCallback
					)
				);

			CorrespondIconProperty =
				DependencyProperty.Register
				(
					nameof(CorrespondIcon),
					typeof(PathGeometry),
					typeof(LabelConnectorCorrespondCode),
					new PropertyMetadata
					(
						new PathGeometry
						{
							Figures =
								PathFigureCollection.Parse
								(
									"M12 2C6.5 2 2 6.5 2 12S6.5 22 12 22 22 17.5 22 12 17.5 2 12 2M10 17L5 12L6.41 " +
									"10.59L10 14.17L17.59 6.58L19 8L10 17Z"
								)
						}
					)
				);

			NotCorrespondIconProperty =
				DependencyProperty.Register
				(
					nameof(NotCorrespondIcon),
					typeof(PathGeometry),
					typeof(LabelConnectorCorrespondCode),
					new PropertyMetadata
					(
						new PathGeometry
						{
							Figures =
								PathFigureCollection.Parse
								(
									"M13,13H11V7H13M13,17H11V15H13M12,2A10,10 0 0,0 2,12A10,10 0 0, 0 12, 22A10, 10 " +
									"0 0, 0 22, 12A10, 10 0 0, 0 12, 2Z"
								)
						}
					)
				);

			IconPropertyKey =
				DependencyProperty.RegisterReadOnly
				(
					nameof(Icon),
					typeof(PathGeometry),
					typeof(LabelConnectorCorrespondCode),
					new PropertyMetadata()
				);

			IconProperty = IconPropertyKey.DependencyProperty;

			CorrespondIconFillProperty =
				DependencyProperty.Register
				(
					nameof(CorrespondIconFill),
					typeof(Brush),
					typeof(LabelConnectorCorrespondCode),
					new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(0x64, 0xD9, 0x2D)))
				);

			NotCorrespondIconFillProperty =
				DependencyProperty.Register
				(
					nameof(NotCorrespondIconFill),
					typeof(Brush),
					typeof(LabelConnectorCorrespondCode),
					new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(0xFF, 0xA5, 0x02)))
				);

			IconFillPropertyKey =
				DependencyProperty.RegisterReadOnly
				(
					nameof(IconFill),
					typeof(Brush),
					typeof(LabelConnectorCorrespondCode),
					new FrameworkPropertyMetadata()
				);

			IconFillProperty = IconFillPropertyKey.DependencyProperty;

			ConnectionBrushProperty =
				DependencyProperty.Register
				(
					nameof(ConnectionBrush),
					typeof(Brush),
					typeof(LabelConnectorCorrespondCode),
					new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(0x00, 0xF7, 0x02)))
				);

			IsEnabledStatusHintProperty =
				DependencyProperty.Register
				(
					nameof(IsEnabledStatusHint),
					typeof(bool),
					typeof(LabelConnectorCorrespondCode),
					new PropertyMetadata
					(
						true,
						UpdateValueStatusHintDPCallback
					)
				);

			CorrespondStatusHintProperty =
				DependencyProperty.Register
				(
					nameof(CorrespondStatusHint),
					typeof(object),
					typeof(LabelConnectorCorrespondCode),
					new PropertyMetadata("Values correspond")
				);

			NotCorrespondStatusHintProperty =
				DependencyProperty.Register
				(
					nameof(NotCorrespondStatusHint),
					typeof(object),
					typeof(LabelConnectorCorrespondCode),
					new PropertyMetadata("Values not correspond!")
				);

			StatusHintPropertyKey =
				DependencyProperty.RegisterReadOnly
				(
					nameof(StatusHint),
					typeof(object),
					typeof(LabelConnectorCorrespondCode),
					new PropertyMetadata(string.Empty)
				);

			StatusHintProperty = StatusHintPropertyKey.DependencyProperty;
			#endregion
		}
		#endregion


		#region Properties infrastructure of registered dependency properties interface
		public string FirstStringValueToCorrespond
		{
			get =>
				(string)GetValue(FirstStringValueToCorrespondProperty);

			set =>
				SetValue(FirstStringValueToCorrespondProperty, value);
		}

		public string SecondStringValueToCorrespond
		{
			get =>
				(string)GetValue(SecondStringValueToCorrespondProperty);

			set =>
				SetValue(SecondStringValueToCorrespondProperty, value);
		}

		public PathGeometry CorrespondIcon
		{
			get =>
				(PathGeometry)GetValue(CorrespondIconProperty);

			set =>
				SetValue(CorrespondIconProperty, value);
		}

		public PathGeometry NotCorrespondIcon
		{
			get =>
				(PathGeometry)GetValue(NotCorrespondIconProperty);

			set =>
				SetValue(NotCorrespondIconProperty, value);
		}

		public PathGeometry Icon
		{
			get =>
				(PathGeometry)GetValue(IconProperty);

			protected set =>
				SetValue(IconPropertyKey, value);
		}

		public Brush ConnectionBrush
		{
			get =>
				(Brush)GetValue(ConnectionBrushProperty);

			set =>
				SetValue(ConnectionBrushProperty, value);
		}

		public Brush CorrespondIconFill
		{
			get =>
				(Brush)GetValue(CorrespondIconFillProperty);

			set =>
				SetValue(CorrespondIconFillProperty, value);
		}

		public Brush NotCorrespondIconFill
		{
			get =>
				(Brush)GetValue(NotCorrespondIconFillProperty);

			set =>
				SetValue(NotCorrespondIconFillProperty, value);
		}

		public Brush IconFill
		{
            get =>
                (Brush)GetValue(IconFillProperty);

			protected set =>
				SetValue(IconFillPropertyKey, value);
		}

		public bool IsEnabledStatusHint
		{
			get =>
				(bool)GetValue(IsEnabledStatusHintProperty);

			set =>
				SetValue(IsEnabledStatusHintProperty, value);
		}

		public object CorrespondStatusHint
		{
			get =>
				GetValue(CorrespondStatusHintProperty);

			set =>
				SetValue(CorrespondStatusHintProperty, value);
		}

		public object NotCorrespondStatusHint
		{
			get =>
				GetValue(NotCorrespondStatusHintProperty);

			set =>
				SetValue(NotCorrespondStatusHintProperty, value);
		}

		public object StatusHint
		{
			get =>
				GetValue(StatusHintProperty);

			protected set =>
				SetValue(StatusHintPropertyKey, value);
		}
		#endregion


		#region Methods
		#region Methods internal
		protected virtual void UpdateValueIconPathGeometryDP() =>
			Icon =
				string.IsNullOrEmpty(FirstStringValueToCorrespond)
				&& string.IsNullOrEmpty(SecondStringValueToCorrespond)
					? null
					: FirstStringValueToCorrespond == SecondStringValueToCorrespond
						? CorrespondIcon
						: NotCorrespondIcon;

		protected virtual void UpdateValueIconFillDP() =>
			IconFill =
				string.IsNullOrEmpty(FirstStringValueToCorrespond)
				&& string.IsNullOrEmpty(SecondStringValueToCorrespond)
					? null
					: FirstStringValueToCorrespond == SecondStringValueToCorrespond
						? CorrespondIconFill
						: NotCorrespondIconFill;

		protected virtual void UpdateValueStatusHintDP() =>
			StatusHint =
				string.IsNullOrEmpty(FirstStringValueToCorrespond)
				&& string.IsNullOrEmpty(SecondStringValueToCorrespond)
					? null
					: FirstStringValueToCorrespond == SecondStringValueToCorrespond
						? CorrespondStatusHint
						: NotCorrespondStatusHint;

		protected virtual void UpdateValueVisibilityDP() =>
			Visibility =
				string.IsNullOrEmpty(FirstStringValueToCorrespond)
				&& string.IsNullOrEmpty(SecondStringValueToCorrespond)
					? Visibility.Hidden
					: Visibility.Visible;
		#endregion


		#region Methods callback in dependency properties internal
		protected static void UpdateValuesIconPathGeometryDPAndIconFillDPAndStatusHintDPAndVisibilityDPCallback
			(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var
				thisControl = (LabelConnectorCorrespondCode)d;

			thisControl.UpdateValueIconPathGeometryDP();
			thisControl.UpdateValueIconFillDP();
			thisControl.UpdateValueStatusHintDP();
			thisControl.UpdateValueVisibilityDP();
		}

		protected static void UpdateValueStatusHintDPCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
			((LabelConnectorCorrespondCode)d).UpdateValueStatusHintDP();
		#endregion
		#endregion
	}
}