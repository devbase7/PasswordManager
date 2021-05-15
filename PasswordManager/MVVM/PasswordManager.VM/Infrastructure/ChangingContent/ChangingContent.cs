#region Namespaces Microsoft
using System;
#endregion


namespace PasswordManager.VM.Infrastructure.ChangingContent
{
	public sealed class ChangingContent<T> : IChangingContentController<T>, IChangingContentService<T>
		where T : class
	{
		#region Implementing IChangingContentController members
		public event Action<T>
			SetContent;
		#endregion


		#region Implementing IChangingContentService members
		public void SetCurrentContent(T vmSetContent) =>
			SetContent?.Invoke(vmSetContent);
		#endregion
	}
}