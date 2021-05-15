#region Namespaces
#region Namespaces Microsoft
using System.Windows.Input;
#endregion


#region Namespaces others
using PasswordManager.VM.Infrastructure.AVMs;
using PasswordManager.VM.Infrastructure.ChangingContent;
using PasswordManager.VM.Infrastructure.Other;
using PasswordManager.VM.VMs.Contents.Level1.Nodes;
using PasswordManager.VM.VMs.Contents.Level1.Standards;
#endregion
#endregion


namespace PasswordManager.VM.VMs.RootNodes
{
	public sealed class VMApplication : AVMBase
	{
		#region Fields internal
		private AVMBase
			_content;

		private string
			_connection;

		private RelayCommand
			_back;

		private AVMBase
			_storageCurrentVMMain;
		#endregion


		#region Constructors interface
		public VMApplication(string connection, IChangingContentController<AVMBase> changingContentController)
		{
			_connection = connection;

			changingContentController.SetContent += OnSetContent;
		}
		#endregion


		#region Properties
		#region Properties data infrastructure for the View interface
		public AVMBase Content
		{
			get =>
				_content;

			set
			{
				SetStorageCurrentVMMain(value);

				_content = value;
				OnPropertyChanged();

				_back?.RaiseCanExecuteChanged();
			}
		}

		public string Connection
		{
			get =>
				_connection;

			set
			{
				_connection = value;
				OnPropertyChanged();
			}
		}
		#endregion


		#region Properties commands infrastructure for the View interface
		public ICommand Back =>
			_back
			?? (_back = new RelayCommand(Execute_Back, CanExecute_Back));
		#endregion
		#endregion


		#region Methods
		#region Methods internal
		private void SetStorageCurrentVMMain(AVMBase vmContentLevel1)
		{
			switch (vmContentLevel1.GetType().Name)
			{
				case nameof(VMChangingLoginPassword):
					_storageCurrentVMMain = _content;
					break;
			}
		}
		#endregion


		#region Methods commands for the View internal
		//'Back'.
		private void Execute_Back(object commandParameter)
		{
			switch (_content.GetType().Name)
			{
				case nameof(VMRegistration):
				case nameof(VMMain):
					((IContentNavigation)_content).Back();
					break;

				case nameof(VMChangingLoginPassword):
					Content = _storageCurrentVMMain;
					_storageCurrentVMMain = null;
					break;
			}
		}

		private bool CanExecute_Back(object commandParameter) =>
			!(_content is VMAuthorization);
		#endregion


		#region Methods event handlers internal
		private void OnSetContent(AVMBase vmSetContent) =>
			Content = vmSetContent;
		#endregion
		#endregion
	}
}