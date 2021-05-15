#region Namespaces others
using PasswordManager.M.Ms.DatabaseRepositoriesFacade;
using PasswordManager.VM.Infrastructure.AVMs;
using PasswordManager.VM.Infrastructure.ChangingContent;
#endregion


namespace PasswordManager.VM.Infrastructure.Other
{
	public sealed class InfrastructureArgs
	{
		#region Constructors interface
		public InfrastructureArgs
			(MDatabaseRepositoriesFacade mDatabaseRepositoriesFacade,
			IChangingContentService<AVMBase> changingContentService)
		{
			MDatabaseRepositoriesFacade = mDatabaseRepositoriesFacade;
			ChangingContentService = changingContentService;
		}
		#endregion


		#region Properties interface
		public MDatabaseRepositoriesFacade MDatabaseRepositoriesFacade { get; }

		public IChangingContentService<AVMBase> ChangingContentService { get; }
		#endregion
	}
}