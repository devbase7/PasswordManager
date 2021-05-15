#region Namespaces
#region Namespaces Microsoft
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
#endregion


#region Namespaces others
using WPFMaterial.Custom.Controls.Windows.Common.Auxiliary;
#endregion
#endregion


#region Aliases
#region Aliases namespaces
using controls = System.Windows.Controls;
#endregion


#region Aliases types
using inputMouseEventArgs = System.Windows.Input.MouseEventArgs;
using windowsPoint = System.Windows.Point;
#endregion
#endregion


namespace WPFMaterial.Custom.Controls.Windows.Standard.Code
{
	public class WindowStandardCode : Window
	{
		#region Fields
		#region Fields static interface
		public static readonly DependencyProperty
			CommandBackProperty,
			CommandClosingProperty,
			CommandClosedProperty;
		#endregion


		#region Fields internal
		protected Grid
			_partGridRootContainer;

		protected Border
			_partBorderChrome;

		protected controls.Image
			_partImageIcon;

		protected controls.Button
			_partButtonMinimize,
			_partButtonMaximizeNormalize,
			_partButtonClose;

		private double
			_widthBeforeMaximize,
			_heightBeforeMaximize;

		private windowsPoint
			_previousScreenBounds,
			_positionBeforeDrag,
			_mouseDownPosition;

		private WindowState
			_previousState;

		private bool
			_isMouseButtonDown;
		//_isManualDrag;
		#endregion
		#endregion


		#region Constructors
		#region Constructor static internal
		static WindowStandardCode()
		{
			#region Dependency properties adding metadata
			DefaultStyleKeyProperty.OverrideMetadata
			(
				typeof(WindowStandardCode),
				new FrameworkPropertyMetadata(typeof(WindowStandardCode))
			);
			#endregion


			#region Dependency properties registration
			CommandBackProperty =
				DependencyProperty.Register
				(
					nameof(CommandBack),
					typeof(ICommand),
					typeof(WindowStandardCode),
					new FrameworkPropertyMetadata(null)
				);

			CommandClosingProperty =
				DependencyProperty.Register
				(
					nameof(CommandClosing),
					typeof(ICommand),
					typeof(WindowStandardCode),
					new FrameworkPropertyMetadata
					(
						null,
						OnCommandClosingChanged
					)
				);

			CommandClosedProperty =
				DependencyProperty.Register
				(
					nameof(CommandClosed),
					typeof(ICommand),
					typeof(WindowStandardCode),
					new FrameworkPropertyMetadata
					(
						null,
						OnCommandClosedChanged
					)
				);
			#endregion
		}
		#endregion


		#region Constructors interface
		public WindowStandardCode()
		{
			Screen
				screen = Screen.FromHandle(new WindowInteropHelper(this).Handle);

			int
				heightWorkingAreaOnScreen = screen.WorkingArea.Height;

			double
				currentDPIScaleFactor = SystemScreenValues.GetCurrentDPIScaleFactor();

			MaxHeight = (heightWorkingAreaOnScreen + 16) / currentDPIScaleFactor;

			Loaded += OnLoaded;
			SizeChanged += OnSizeChanged;
			StateChanged += OnStateChanged;
			MouseMove += OnMouseMove;
			MouseLeftButtonUp += OnMouseLeftButtonUp;

			SystemEvents.DisplaySettingsChanged += OnDisplaySettingsChanged;
		}
		#endregion
		#endregion


		#region Properties infrastructure of registered dependency properties interface
		public ICommand CommandBack
		{
			get =>
				(ICommand)GetValue(CommandBackProperty);

			set =>
				SetValue(CommandBackProperty, value);
		}

		public ICommand CommandClosing
		{
			get =>
				(ICommand)GetValue(CommandClosingProperty);

			set =>
				SetValue(CommandClosingProperty, value);
		}

		public ICommand CommandClosed
		{
			get =>
				(ICommand)GetValue(CommandClosedProperty);

			set =>
				SetValue(CommandClosedProperty, value);
		}
		#endregion


		#region Methods
		#region Methods internal
		protected virtual Thickness GetDefaultMarginForDPI()
		{
			int
				currentDPI = SystemScreenValues.GetCurrentDPI();

			Thickness
				thickness;

			switch (currentDPI)
			{
				case 120:
					thickness = new Thickness(7, 7, 4, 5);
					break;

				case 144:
					thickness = new Thickness(7, 7, 3, 1);
					break;

				case 168:
					thickness = new Thickness(6, 6, 2, 0);
					break;

				case 192:
				case 240:
					thickness = new Thickness(6, 6, 0, 0);
					break;

				default:
					thickness = new Thickness(8);
					break;
			}

			return thickness;
		}

		protected virtual Thickness GetFromMinimizedMarginForDPI()
		{
			int
				currentDPI = SystemScreenValues.GetCurrentDPI();

			Thickness
				thickness;

			switch (currentDPI)
			{
				case 120:
					thickness = new Thickness(6, 6, 4, 6);
					break;

				case 144:
					thickness = new Thickness(7, 7, 4, 4);
					break;

				case 168:
				case 192:
					thickness = new Thickness(6, 6, 2, 2);
					break;

				case 240:
					thickness = new Thickness(6, 6, 0, 0);
					break;

				default:
					thickness = new Thickness(7, 7, 5, 7);
					break;
			}

			return thickness;
		}

		protected virtual void OpenSystemContextMenuUnderChrome(MouseButtonEventArgs e)
		{
			IntPtr
				handle = new WindowInteropHelper(this).Handle,
				systemMenu = SystemNativeUtilities.GetSystemMenu(handle, false);

			bool
				windowStateIsEqualNormal = WindowState == WindowState.Normal;

			int
				num =
					SystemNativeUtilities.TrackPopupMenuEx
					(
						systemMenu,
						SystemNativeUtilities.TpmLeftAlign
						| SystemNativeUtilities.TpmReturnCMD,
						Convert.ToInt32
						(
							windowStateIsEqualNormal
								? Left + BorderThickness.Left
								: 0
						),
						Convert.ToInt32
						(
							windowStateIsEqualNormal
								? Top + 30
								: 29
						),
						handle,
						IntPtr.Zero
					);

            if (num == 0)
                return;

			SystemNativeUtilities.PostMessage(handle, 274, new IntPtr(num), IntPtr.Zero);
		}

		protected virtual void OpenSystemContextMenuWhereMousePointer(MouseButtonEventArgs e)
		{
			windowsPoint
				position = e.GetPosition(this),
				screen = PointToScreen(position);

			int
				num = 36;

			if (position.Y < num)
			{
				IntPtr
					handle = new WindowInteropHelper(this).Handle,
					systemMenu = SystemNativeUtilities.GetSystemMenu(handle, false);

				if (WindowState != WindowState.Maximized)
					SystemNativeUtilities.EnableMenuItem(systemMenu, 61488, 0);
				else
					SystemNativeUtilities.EnableMenuItem(systemMenu, 61488, 1);

				int
					num1 =
						SystemNativeUtilities.TrackPopupMenuEx
						(
							systemMenu,
							SystemNativeUtilities.TpmLeftAlign
							| SystemNativeUtilities.TpmReturnCMD,
							Convert.ToInt32(screen.X),
							Convert.ToInt32(screen.Y + 2),
							handle,
							IntPtr.Zero
						);

				if (num1 == 0)
					return;

				SystemNativeUtilities.PostMessage(handle, 274, new IntPtr(num1), IntPtr.Zero);
			}
		}

		protected virtual void UpdateValueCommandClosingDP(DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue != null)
				Closing += OnClosing;
			else
				Closing -= OnClosing;
		}

		protected virtual void UpdateValueCommandClosedDP(DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue != null)
				Closed += OnClosed;
			else
				Closed -= OnClosed;
		}

		protected void ToggleWindowState() =>
			WindowState =
				WindowState != WindowState.Maximized
					? WindowState.Maximized
					: WindowState.Normal;

        private void RefreshWindowState()
        {
            if (WindowState == WindowState.Maximized)
            {
                ToggleWindowState();
                ToggleWindowState();
            }
        }
		#endregion


		#region Methods callback in dependency properties internal
		protected static void OnCommandClosedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
			((WindowStandardCode)d).UpdateValueCommandClosedDP(e);

		protected static void OnCommandClosingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
			((WindowStandardCode)d).UpdateValueCommandClosingDP(e);
		#endregion


		#region Methods event handlers overridden interface
		public override void OnApplyTemplate()
		{
			_partGridRootContainer = GetTemplateChild("PART_Grid_RootContainer") as Grid;
			_partBorderChrome = GetTemplateChild("PART_Border_Chrome") as Border;

			_partImageIcon = GetTemplateChild("PART_Image_Icon") as controls.Image;

			_partButtonMinimize = GetTemplateChild("PART_Button_Minimize") as controls.Button;
			_partButtonMaximizeNormalize = GetTemplateChild("PART_Button_MaximizeNormalize") as controls.Button;
			_partButtonClose = GetTemplateChild("PART_Button_Close") as controls.Button;

			if
			(
				_partBorderChrome != null
				&& WindowState == WindowState.Maximized
			)
				_partBorderChrome.Margin = GetDefaultMarginForDPI();

			if (_partImageIcon != null)
			{
				_partImageIcon.MouseLeftButtonDown += ImageIcon_OnMouseLeftButtonDown;
				_partImageIcon.MouseRightButtonUp += ImageIcon_OnMouseRightButtonUp;
			}

			if (_partButtonMinimize != null)
				_partButtonMinimize.Click += ButtonMinimize_OnClick;

			if (_partButtonMaximizeNormalize != null)
				_partButtonMaximizeNormalize.Click += ButtonMaximizeNormalize_OnClick;

			if (_partButtonClose != null)
				_partButtonClose.Click += ButtonClose_OnClick;
		}
		#endregion


		#region Methods event handlers internal
		protected virtual void OnLoaded(object sender, RoutedEventArgs e)
		{
			Screen
				screen = Screen.FromHandle(new WindowInteropHelper(this).Handle);

			Rectangle
				workingArea = screen.WorkingArea;

			_previousScreenBounds = new windowsPoint(workingArea.Width, workingArea.Height);
		}

		protected virtual void OnMouseMove(object sender, inputMouseEventArgs e)
		{
			if (!_isMouseButtonDown)
				return;

			windowsPoint
				position = e.GetPosition(this),
				screen = PointToScreen(position);

			double
				currentDPIScaleFactor = SystemScreenValues.GetCurrentDPIScaleFactor(),
				x = _mouseDownPosition.X - position.X,
				y = _mouseDownPosition.Y - position.Y;

			Debug.WriteLine(position);

			if (Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)) > 1)
			{
				double
					actualWidth = _mouseDownPosition.X;

				if (_mouseDownPosition.X <= 0)
					actualWidth = 0;
				else if (_mouseDownPosition.X >= ActualWidth)
					actualWidth = _widthBeforeMaximize;

				if (WindowState == WindowState.Maximized)
				{
					ToggleWindowState();

					Top = (screen.Y - position.Y) / currentDPIScaleFactor;
					Left = (screen.X - actualWidth) / currentDPIScaleFactor;

					CaptureMouse();
				}

				//_isManualDrag = true;

				Top = (screen.Y - _mouseDownPosition.Y) / currentDPIScaleFactor;
				Left = (screen.X - actualWidth) / currentDPIScaleFactor;
			}
		}

		protected virtual void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			_isMouseButtonDown = false;
			//_isManualDrag = false;
			ReleaseMouseCapture();
		}

		protected virtual void ImageIcon_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) =>
			OpenSystemContextMenuUnderChrome(e);

		protected virtual void ImageIcon_OnMouseRightButtonUp(object sender, MouseButtonEventArgs e) =>
			OpenSystemContextMenuWhereMousePointer(e);

		protected virtual void ButtonMinimize_OnClick(object sender, RoutedEventArgs e) =>
			WindowState = WindowState.Minimized;

		protected virtual void ButtonMaximizeNormalize_OnClick(object sender, RoutedEventArgs e) =>
			WindowState =
				WindowState == WindowState.Normal
					? WindowState.Maximized
					: WindowState.Normal;

		protected virtual void ButtonClose_OnClick(object sender, RoutedEventArgs e) =>
			Close();

		protected virtual void OnSizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (WindowState == WindowState.Normal)
			{
				_heightBeforeMaximize = ActualHeight;
				_widthBeforeMaximize = ActualWidth;

				return;
			}

			if (WindowState == WindowState.Maximized)
			{
				Screen
					screen = Screen.FromHandle(new WindowInteropHelper(this).Handle);

				if
				(
					_previousScreenBounds.X != screen.WorkingArea.Width
					|| _previousScreenBounds.Y != screen.WorkingArea.Height
				)
				{
					double
						width = screen.WorkingArea.Width;

					Rectangle
						workingArea = screen.WorkingArea;

					_previousScreenBounds = new windowsPoint(width, workingArea.Height);

					RefreshWindowState();
				}
			}
		}

		protected virtual void OnStateChanged(object sender, EventArgs e)
		{
			Screen
				screen = Screen.FromHandle(new WindowInteropHelper(this).Handle);

			Thickness
				thickness = new Thickness(0);

			if (WindowState != WindowState.Maximized)
			{
				double
					currentDPIScaleFactor = SystemScreenValues.GetCurrentDPIScaleFactor();

				Rectangle
					workingArea = screen.WorkingArea;

				MaxWidth = double.PositiveInfinity;
				MaxHeight = (workingArea.Height + 16) / currentDPIScaleFactor;
			}
			else
			{
				thickness = GetDefaultMarginForDPI();

				if
				(
					_previousState == WindowState.Minimized
					|| Left == _positionBeforeDrag.X
					&& Top == _positionBeforeDrag.Y
				)
					thickness = GetFromMinimizedMarginForDPI();
			}

			_partGridRootContainer.Margin = thickness;
			_previousState = WindowState;
		}

		protected virtual void OnDisplaySettingsChanged(object sender, EventArgs e)
		{
			Screen
				screen = Screen.FromHandle(new WindowInteropHelper(this).Handle);

			Rectangle
				workingArea = screen.WorkingArea;

			double
				width = screen.WorkingArea.Width;

			_previousScreenBounds = new windowsPoint(width, workingArea.Height);

			RefreshWindowState();
		}

		protected virtual void OnClosing(object sender, CancelEventArgs e)
		{
			ICommand
				commandClosing = (ICommand)GetValue(CommandClosingProperty);

			if (commandClosing != null)
			{
				if (commandClosing.CanExecute(null))
					commandClosing.Execute(null);
				else
					e.Cancel = true;
			}
		}

		protected virtual void OnClosed(object sender, EventArgs e)
		{
			ICommand
				commandClosed = (ICommand)GetValue(CommandClosedProperty);

			if
			(
				commandClosed != null
				&& commandClosed.CanExecute(null)
			)
				commandClosed.Execute(null);
		}
		#endregion
		#endregion
	}
}