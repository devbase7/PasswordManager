#region Namespaces others
using PasswordManager.VM.Infrastructure.AVMs;
using PasswordManager.VM.Infrastructure.ChangingContent;
#endregion


namespace PasswordManager.VM.VMs.Contents.Level1.Nodes
{
	public sealed class VMChangingLoginPassword : AVMBase
	{
		#region Fields internal
		private AVMBase
			_content;
		#endregion


		#region Constructors interface
		public VMChangingLoginPassword(IChangingContentController<AVMBase> changingContentController) =>
			changingContentController.SetContent += OnSetContent;
		#endregion


		#region Properties data infrastructure for the View interface
		public AVMBase Content
		{
			get =>
				_content;

			set
			{
				_content = value;
				OnPropertyChanged();
			}
		}
		#endregion


		#region Methods event handlers internal
		private void OnSetContent(AVMBase vmSetContent) =>
			Content = vmSetContent;
		#endregion
	}
}