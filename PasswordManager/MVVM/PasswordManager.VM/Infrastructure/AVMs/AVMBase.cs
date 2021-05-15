#region Namespaces Microsoft
using System.ComponentModel;
using System.Runtime.CompilerServices;
#endregion


namespace PasswordManager.VM.Infrastructure.AVMs
{
	public abstract class AVMBase : INotifyPropertyChanged
	{
		#region Methods internal
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		#endregion


		#region Implementing INotifyPropertyChanged members
		public event PropertyChangedEventHandler
			PropertyChanged;
		#endregion
	}
}