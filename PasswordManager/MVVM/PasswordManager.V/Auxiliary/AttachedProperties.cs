#region Namespaces Microsoft
using System.Windows;
using System.Windows.Input;
#endregion


namespace PasswordManager.V.Auxiliary
{
	internal class AttachedProperties
	{
		#region Fields static internal
		private static readonly DependencyProperty
			InputBindingsProperty;
		#endregion


		#region Constructor static internal
		static AttachedProperties()
		{
			#region Dependency properties attached registration
			InputBindingsProperty =
				DependencyProperty.RegisterAttached
				(
					"InputBindings",
					typeof(InputBindingCollection),
					typeof(AttachedProperties),
					new FrameworkPropertyMetadata
					(
						new InputBindingCollection(),
						(sender, e) =>
							{
								var
									element = sender as UIElement;

								if (element == null)
									return;

								element.InputBindings.Clear();
								element.InputBindings.AddRange((InputBindingCollection)e.NewValue);
							}
					)
				);
			#endregion
		}
		#endregion


		#region Methods static interface
		internal static InputBindingCollection GetInputBindings(UIElement element) =>
			(InputBindingCollection)element.GetValue(InputBindingsProperty);

		internal static void SetInputBindings(UIElement element , InputBindingCollection inputBindings) =>
			element.SetValue(InputBindingsProperty, inputBindings);
		#endregion
	}
}