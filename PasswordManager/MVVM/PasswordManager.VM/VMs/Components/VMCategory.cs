#region Namespaces
#region Namespaces Microsoft
using System.Collections.ObjectModel;
using System.Collections.Specialized;
#endregion


#region Namespaces others
using PasswordManager.VM.Infrastructure.AVMs;
#endregion
#endregion


namespace PasswordManager.VM.VMs.Components
{
	public sealed class VMCategory : AVMBase
	{
		#region Fields internal
		private string
			_iconPath,
			_iconColor,
			_name;

		private int
			_counter;
		#endregion


		#region Constructors interface
		public VMCategory(string iconPath, string iconColor, string name)
		{
			Records.CollectionChanged += Records_OnCollectionChanged;

			_iconPath = iconPath;
			_iconColor = iconColor;
			_name = name;
		}
		#endregion


		#region Properties
		#region Properties interface
		public ObservableCollection<VMRecord> Records { get; } = new ObservableCollection<VMRecord>();
		#endregion


		#region Properties data infrastructure for the View interface
		public string IconPath
		{
			get =>
				_iconPath;

			set
			{
				_iconPath = value;
				OnPropertyChanged();
			}
		}

		public string IconColor
		{
			get =>
				_iconColor;

			set
			{
				_iconColor = value;
				OnPropertyChanged();
			}
		}

		public string Name
		{
			get =>
				_name;

			set
			{
				_name = value;
				OnPropertyChanged();
			}
		}

		public int Counter
		{
			get =>
				_counter;

			set
			{
				_counter = value;
				OnPropertyChanged();
			}
		}
		#endregion
		#endregion


		#region Methods event handlers internal
		private void Records_OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
				case NotifyCollectionChangedAction.Remove:
				case NotifyCollectionChangedAction.Reset:
					Counter = Records.Count;
					break;
			}
		}
		#endregion
	}
}