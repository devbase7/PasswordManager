#region Namespaces others
using PasswordManager.VM.Infrastructure.AVMs;
#endregion


namespace PasswordManager.VM.VMs.Components
{
	public sealed class VMRecord : AVMBase
	{
		#region Fields internal
		private bool
			_isFavourite;

		private string
			_header,
			_category,
			_service;
		#endregion


		#region Constructors interface
		public VMRecord
			(bool isFavourite, string header, string category, string service, string url, string login,
			string password, string note)
		{
			_isFavourite = isFavourite;
			_header = header;
			_service = service;

			_category = category;
			URL = url;
			Login = login;
			Password = password;
			Note = note;
		}
		#endregion


		#region Properties
		#region Properties interface
		public string Category
		{
			get =>
				_category;

			set
			{
				_category = value;
			}
		}

		public string URL { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public string Note { get; set; }
		#endregion


		#region Properties data infrastructure for the View interface
		public bool IsFavourite
		{
			get =>
				_isFavourite;

			set
			{
				_isFavourite = value;
			}
		}

		public string Header
		{
			get =>
				_header;

			set
			{
				_header = value;
				OnPropertyChanged();
			}
		}

		public string Service
		{
			get =>
				_service;

			set
			{
				_service = value;
				OnPropertyChanged();
			}
		}
		#endregion
		#endregion
	}
}