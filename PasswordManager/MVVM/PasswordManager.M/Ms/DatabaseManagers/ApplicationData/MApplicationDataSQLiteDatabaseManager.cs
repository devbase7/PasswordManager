#region Namespaces
#region Namespaces Microsoft
using System;
using System.Data;
using System.Data.SQLite;
#endregion


#region Namespaces others
using PasswordManager.DTO.Auxiliary;
using PasswordManager.DTO.DTOs;
using PasswordManager.M.Auxiliary;
#endregion
#endregion


namespace PasswordManager.M.Ms.DatabaseManagers.ApplicationData
{
	public sealed class MApplicationDataSQLiteDatabaseManager
	{
		#region Constants internal
		private const string
			TableNameAuthorizationState = "AuthorizationState",
			TableAuthorizationStateLogin = "Login",
			TableAuthorizationStatePassword = "Password",
			TableAuthorizationStateIsRememberMe = "IsRememberMe";

		private const string
			TableNameCurrentSession = "CurrentSession",
			TableCurrentSessionLogin = "Login",
			TableCurrentSessionPassword = "Password";
		#endregion


		#region Fields internal
		private readonly string
			_urlAndDirectoryNameDb = @".\LocalDatabases",
			_urlAndDBFileName;
		#endregion


		#region Constructors interface
		public MApplicationDataSQLiteDatabaseManager() =>
			_urlAndDBFileName = $@"{_urlAndDirectoryNameDb}\ApplicationData.sqlite";
		#endregion


		#region Methods
		#region Methods interface
		public DTOAuthorizationStateQuery GetAuthorizationState()
		{
			DTOError
				error = DatabaseDirectory.CreateIfNotExist(_urlAndDirectoryNameDb);

			if (error != null)
				return
					new DTOAuthorizationStateQuery
					(
						error,
						null
					);

			DataTable
				dataTable = new DataTable();

			using
			(
				SQLiteConnection
					sqliteConnection = new SQLiteConnection($"Data Source={_urlAndDBFileName}; version=3;")
			)
			{
				error = CreateTableAuthorizationStateIfNotExist();

				if (error != null)
					return
						new DTOAuthorizationStateQuery
						(
							error,
							null
						);

				string
					commandTextSelectFrom = $"SELECT * FROM {TableNameAuthorizationState}";

				try
				{
					SQLiteDataAdapter
						sqliteDataAdapter = new SQLiteDataAdapter(commandTextSelectFrom, sqliteConnection);

					sqliteDataAdapter.Fill(dataTable);
				}
				catch (Exception exception)
				{
					return
						new DTOAuthorizationStateQuery
						(
							new DTOError
							(
								ErrorTypes.Warning,
								$"Таблица в базе данных URL: \"{_urlAndDBFileName}\", хранящая значения заполнения " +
								$"формы авторизации, не была найдена, возникшая ошибка: \"{exception.Message}\"!\n" +
								"Не все функции приложения работают корректно!"
							),
							null
						);
				}
			}

			int
				dataTableRowsCount = dataTable.Rows.Count;

			if (dataTableRowsCount > 0)
				return
					new DTOAuthorizationStateQuery
					(
						null,
						new DTOAuthorizationState
						(
							$"{dataTable.Rows[0][TableAuthorizationStateLogin]}",
							$"{dataTable.Rows[0][TableAuthorizationStatePassword]}",
							$"{dataTable.Rows[0][TableAuthorizationStateIsRememberMe]}" == "1"
						)
					);
			else
				return
					new DTOAuthorizationStateQuery
					(
						null,
						null
					);
		}

		public DTOError SetAuthorizationState(DTOAuthorizationState authorizationState)
		{
			DTOError
				error = DatabaseDirectory.CreateIfNotExist(_urlAndDirectoryNameDb);

			if (error != null)
				return error;

			using
			(
				SQLiteConnection
					sqliteConnection = new SQLiteConnection($"Data Source={_urlAndDBFileName}; version=3;")
			)
			{
				sqliteConnection.Open();

				error = CreateTableAuthorizationStateIfNotExist();

				if (error != null)
				{
					sqliteConnection.Dispose();

					return error;
				}

				string
					commandTextSelectCountFrom = $"SELECT COUNT(*) FROM {TableNameAuthorizationState};";

				object
					result;

				(error, result) = ExecutingSQLiteCommands.Scalar(sqliteConnection, commandTextSelectCountFrom);

				if (error != null)
				{
					sqliteConnection.Dispose();

					return error;
				}

				int
					numberRowsInAuthorizationState = int.Parse(result.ToString());

				string
					commandTextDeleteFrom = $"DELETE FROM {TableNameAuthorizationState};",
					commandTextInsertIntoValues =
						$"INSERT INTO {TableNameAuthorizationState} VALUES (" +
						$"'{authorizationState.Login}', " +
						$"'{authorizationState.Password}', " +
						$"{(authorizationState.IsRememberMe ? 1 : 0)});",
					commandText =
						numberRowsInAuthorizationState > 0
							? commandTextDeleteFrom + commandTextInsertIntoValues
							: commandTextInsertIntoValues;

				error = ExecutingSQLiteCommands.NonQuery(sqliteConnection, commandText);
			}

			return
				error != null
					? new DTOError
					(
						ErrorTypes.Warning,
						$"Данные заполнения формы авторизации не были сохранены, " +
						$"возникшая ошибка: \"{error.Message}\"!\n" +
						"Не все функции приложения работают корректно!"
					)
					: null;
		}

		public (DTOError error, bool isRememberMe) GetAuthorizationStateIsRememberMe()
		{
			DTOError
				error = DatabaseDirectory.CreateIfNotExist(_urlAndDirectoryNameDb);

			if (error != null)
				return
					(
						error,
						false
					);

			DataTable
				dataTable = new DataTable();

			using
			(
				SQLiteConnection
					sqliteConnection = new SQLiteConnection($"Data Source={_urlAndDBFileName}; version=3;")
			)
			{
				sqliteConnection.Open();

				error = CreateTableAuthorizationStateIfNotExist();

				if (error != null)
					return
						(
							error,
							false
						);

				string
					commandTextSelectFrom = $"SELECT * FROM {TableNameAuthorizationState}";

				try
				{
					SQLiteDataAdapter
						sqliteDataAdapter = new SQLiteDataAdapter(commandTextSelectFrom, sqliteConnection);

					sqliteDataAdapter.Fill(dataTable);
				}
				catch (Exception exception)
				{
					return
						(
							new DTOError
							(
								ErrorTypes.Warning,
								$"Таблица в базе данных URL: \"{_urlAndDBFileName}\", хранящая значения заполнения " +
								$"формы авторизации, не была найдена, возникшая ошибка: \"{exception.Message}\"!\n" +
								"Не все функции приложения работают корректно!"
							),
							false
						);
				}
			}

			int
				dataTableRowsCount = dataTable.Rows.Count;

			if (dataTableRowsCount > 0)
				return
					(
						null,
						$"{dataTable.Rows[0][TableAuthorizationStateIsRememberMe]}" == "1"
					);
			else
				return
					(
						null,
						false
					);
		}

		public DTOError ResetAuthorizationState()
		{
			DTOError
				error = DatabaseDirectory.CreateIfNotExist(_urlAndDirectoryNameDb);

			if (error != null)
				return error;

			using
			(
				SQLiteConnection
					sqliteConnection = new SQLiteConnection($"Data Source={_urlAndDBFileName}; version=3;")
			)
			{
				sqliteConnection.Open();

				error = CreateTableAuthorizationStateIfNotExist();

				if (error != null)
				{
					sqliteConnection.Dispose();

					return error;
				}

				string
					commandTextDeleteFrom = $"DELETE FROM {TableNameAuthorizationState}";

				error = ExecutingSQLiteCommands.NonQuery(sqliteConnection, commandTextDeleteFrom);

				if (error != null)
				{
					sqliteConnection.Dispose();

					return error;
				}
			}

			return null;
		}

		public (DTOError error, string login, string password) GetSessionAccount()
		{
			return
				(
					null,
					null,
					null
				);
		}

		public DTOError SetSessionAccount(string login, string password)
		{
			return null;
		}

		public DTOError ResetSessionAccount(string login, string password)
		{
			return null;
		}
		#endregion


		#region Methods internal
		private DTOError CreateTableAuthorizationStateIfNotExist()
		{
			DTOError
				error;

			using
			(
				SQLiteConnection
					sqliteConnection = new SQLiteConnection($"Data Source={_urlAndDBFileName}; version=3;")
			)
			{
				sqliteConnection.Open();

				string
					commandTextCreateTableIfNotExists =
						$"CREATE TABLE IF NOT EXISTS {TableNameAuthorizationState} (" +
						$"{TableAuthorizationStateLogin} TEXT NOT NULL," +
						$"{TableAuthorizationStatePassword} TEXT NOT NULL," +
						$"{TableAuthorizationStateIsRememberMe} INTEGER NOT NULL);";

				error = ExecutingSQLiteCommands.NonQuery(sqliteConnection, commandTextCreateTableIfNotExists);
			}

			return
				error != null
					? new DTOError
					(
						ErrorTypes.Warning,
						$"Таблица в базе данных URL: \"{_urlAndDBFileName}\", хранящая значения заполнения " +
						$"формы авторизации, не была создана, возникшая ошибка: \"{error.Message}\"!\n" +
						"Не все функции приложения работают корректно!"
					)
					: null;
		}
		#endregion
		#endregion
	}
}