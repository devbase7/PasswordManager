#region Namespaces Microsoft
using System.Windows;
using System.Windows.Controls;
#endregion


namespace WPFMaterial.Custom.Panels.WrapPanels.Break
{
	public class WrapPanelBreak : Panel
	{
		#region Fields static interface
		public static DependencyProperty
			LineBreakBeforeProperty;
		#endregion


		#region Constructor static internal
		static WrapPanelBreak()
		{
			#region Dependency properties attached registration
			LineBreakBeforeProperty =
				DependencyProperty.RegisterAttached
				(
					nameof(LineBreakBefore),
					typeof(bool),
					typeof(WrapPanelBreak),
					new FrameworkPropertyMetadata
					{
						AffectsArrange = true,
						AffectsMeasure = true
					}
				);
			#endregion
		}
		#endregion


		#region Properties infrastructure of registered dependency properties interface
		public bool LineBreakBefore
		{
			get =>
				(bool)GetValue(LineBreakBeforeProperty);

			set =>
				SetValue(LineBreakBeforeProperty, value);
		}
		#endregion
	}
}