#region Namespaces others
using PasswordManager.M.Ms.DatabaseRepositoriesFacade;
using PasswordManager.VM.Infrastructure.ChangingContent;
using PasswordManager.VM.Infrastructure.Other;
#endregion


namespace PasswordManager.VM.Infrastructure.AVMs
{
	public abstract class AVMInfrastructure : AVMBase
	{
		#region Constructors internal
		protected AVMInfrastructure(InfrastructureArgs infrastructureArgs)
		{
			MDatabaseRepositoriesFacade = infrastructureArgs.MDatabaseRepositoriesFacade;
			ChangingContentService = infrastructureArgs.ChangingContentService;
		}
		#endregion


		#region Properties internal
		protected MDatabaseRepositoriesFacade MDatabaseRepositoriesFacade { get; }

		protected IChangingContentService<AVMBase> ChangingContentService { get; }
		#endregion
	}
}