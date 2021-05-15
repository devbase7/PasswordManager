#region Namespaces Microsoft
using System;
#endregion


namespace PasswordManager.VM.Infrastructure.ChangingContent
{
	public interface IChangingContentController<T>
		where T : class
	{
		#region Events signatures
		event Action<T>
			SetContent;
		#endregion
	}
}