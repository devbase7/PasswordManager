#region Namespaces
#region Namespaces Microsoft
using System.Windows;
#endregion


#region Namespaces others
using PasswordManager.DTO.DTOs;
using PasswordManager.M.Ms.DatabaseManagers.AccountsData;
using PasswordManager.M.Ms.DatabaseRepositoriesFacade;
using PasswordManager.M.Repositories.Databases;
using PasswordManager.V.Vs.RootNodes;
using PasswordManager.VM.Infrastructure.AVMs;
using PasswordManager.VM.Infrastructure.ChangingContent;
using PasswordManager.VM.Infrastructure.Other;
using PasswordManager.VM.VMs.Contents.Level1.Standards;
using PasswordManager.VM.VMs.RootNodes;
#endregion
#endregion


namespace PasswordManager.V
{
	public partial class App : Application
	{
		#region Methods event handlers overridden internal
		protected override void OnStartup(StartupEventArgs e)
		{
			MessageBoxResult //Временное решение.
				messageBoxResult = MessageBox.Show
				(
					"Какой тип подключения СУБД, для работы с данными использовать?\n\n\n" +
					"\"Да\" – SQLite тип подключения.\n\n" +
					"\"Нет\" – SQL тип подключения.\n" +
					"*Внимание!!! Данный тип подключения будет выполнен, но само взаимодействие с базой данных SQL " +
					"ещё не реализовано!",
					"Question",
					MessageBoxButton.YesNo,
					MessageBoxImage.Question,
					MessageBoxResult.OK
				);

			IAccountsDataRepository
				mAccountsDataRepository =
					messageBoxResult == MessageBoxResult.Yes
					|| messageBoxResult == MessageBoxResult.None
						? new MAccountsDataSQLiteDatabaseManager()
						: (IAccountsDataRepository)new MAccountsDataSQLDatabaseManager();

			MDatabaseRepositoriesFacade
				mDatabaseRepositoriesFacade = new MDatabaseRepositoriesFacade(mAccountsDataRepository);

			ChangingContent<AVMBase>
				changingContent = new ChangingContent<AVMBase>();

			InfrastructureArgs
				infrastructureArgs = new InfrastructureArgs(mDatabaseRepositoriesFacade, changingContent);

			DTOAuthorizationStateQuery
				authorizationStateQuery = mDatabaseRepositoriesFacade.GetAuthorizationState();

			VMAuthorization
				vmAuthorization =
					authorizationStateQuery.Error == null
					&& authorizationStateQuery.AuthorizationState == null
						? new VMAuthorization(infrastructureArgs)
						: new VMAuthorization(authorizationStateQuery, infrastructureArgs);

			string
				connection =
					mAccountsDataRepository is MAccountsDataSQLiteDatabaseManager
						? "Local"
						: "Web";

			VApplication
				vApplication = new VApplication
				{
					DataContext = new VMApplication(connection, changingContent)
				};

			changingContent.SetCurrentContent(vmAuthorization);

			vApplication.Show();
		}
		#endregion
	}
}