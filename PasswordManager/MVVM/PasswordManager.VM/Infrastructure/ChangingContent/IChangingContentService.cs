namespace PasswordManager.VM.Infrastructure.ChangingContent
{
	public interface IChangingContentService<T>
		where T : class
	{
		#region Methods signatures
		void SetCurrentContent(T vmSetContent);
		#endregion
	}
}