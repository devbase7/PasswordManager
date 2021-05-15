#region Namespaces
#region Namespaces Microsoft
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
#endregion


#region Namespaces others
using PasswordManager.DTO.Auxiliary;
using PasswordManager.DTO.DTOs;
using PasswordManager.M.Auxiliary;
using PasswordManager.M.Repositories.Databases;
#endregion
#endregion


namespace PasswordManager.M.Ms.DatabaseManagers.AccountsData
{
	public sealed class MAccountsDataSQLiteDatabaseManager : IAccountsDataRepository
	{
		#region Constants internal
		private const string
			TableNameRegisteredAccounts = "RegisteredAccounts",
			TableRegisteredAccountsLogin = "Login",
			TableRegisteredAccountsPassword = "Password",
			TableRegisteredAccountsTableRegisteredRecords = "TableRegisteredRecords";

		private const string
			TableRecordsId = "Id",
			TableRecordsIsFavourite = "IsFavourite",
			TableRecordsCategory = "Category",
			TableRecordsService = "Service",
			TableRecordsURL = "URL",
			TableRecordsLogin = "Login",
			TableRecordsPassword = "Password",
			TableRecordsNote = "Note";
		#endregion


		#region Fields internal
		private readonly string
			_urlAndDirectoryNameDb = @".\LocalDatabases",
			_urlAndDBFileName;
		#endregion


		#region Constructors interface
		public MAccountsDataSQLiteDatabaseManager() =>
			_urlAndDBFileName = $@"{_urlAndDirectoryNameDb}\AccountsData.sqlite";
		#endregion


		#region Methods internal
		private DTOError CreateTableDataForRegisteredAccountsIfNotExist()
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
						$"CREATE TABLE IF NOT EXISTS {TableNameRegisteredAccounts} (" +
						$"{TableRegisteredAccountsLogin} TEXT NOT NULL," +
						$"{TableRegisteredAccountsPassword} TEXT NOT NULL," +
						$"{TableRegisteredAccountsTableRegisteredRecords} TEXT NOT NULL)";

				error = ExecutingSQLiteCommands.NonQuery(sqliteConnection, commandTextCreateTableIfNotExists);
			}

			return
				error != null
					? new DTOError
					(
						ErrorTypes.Fatal,
						$"Таблица в базе данных URL: \"{_urlAndDBFileName}\", хранящая данные " +
						$"зарегистрированных аккаунтов, не была создана, возникшая ошибка: \"{error.Message}\"!\n" +
						"Дальнейшая корректная работа приложения невозможна!"
					)
					: null;
		}

		private (DTOError error, List<DTOCategory> categories) GetCategoriesAccount()
		{
			//DTOError
			//	error;

			string
				iconPathTest1 = "M22,18V22H18V19H15V16H12L9.74,13.74C9.19,13.91 8.61,14 8,14A6,6 0 0,1 2,8A6,6 0 0,1 8," +
				"2A6,6 0 0,1 14,8C14,8.61 13.91,9.19 13.74,9.74L22,18M7,5A2,2 0 0,0 5,7A2,2 0 0,0 7,9A2,2 0 0,0 9," +
				"7A2,2 0 0,0 7,5Z",
				iconPathTest2 = "M16.36,14C16.44,13.34 16.5,12.68 16.5,12C16.5,11.32 16.44,10.66 16.36,10H19.74C19.9," +
				"10.64 20,11.31 20,12C20,12.69 19.9,13.36 19.74,14M14.59,19.56C15.19,18.45 15.65,17.25 15.97," +
				"16H18.92C17.96,17.65 16.43,18.93 14.59,19.56M14.34,14H9.66C9.56,13.34 9.5,12.68 9.5,12C9.5," +
				"11.32 9.56,10.65 9.66,10H14.34C14.43,10.65 14.5,11.32 14.5,12C14.5,12.68 14.43,13.34 14.34,14M12," +
				"19.96C11.17,18.76 10.5,17.43 10.09,16H13.91C13.5,17.43 12.83,18.76 12,19.96M8,8H5.08C6.03,6.34 7.57," +
				"5.06 9.4,4.44C8.8,5.55 8.35,6.75 8,8M5.08,16H8C8.35,17.25 8.8,18.45 9.4,19.56C7.57,18.93 6.03," +
				"17.65 5.08,16M4.26,14C4.1,13.36 4,12.69 4,12C4,11.31 4.1,10.64 4.26,10H7.64C7.56,10.66 7.5," +
				"11.32 7.5,12C7.5,12.68 7.56,13.34 7.64,14M12,4.03C12.83,5.23 13.5,6.57 13.91,8H10.09C10.5,6.57 11.17," +
				"5.23 12,4.03M18.92,8H15.97C15.65,6.75 15.19,5.55 14.59,4.44C16.43,5.07 17.96,6.34 18.92,8M12,2C6.47," +
				"2 2,6.5 2,12A10,10 0 0,0 12,22A10,10 0 0,0 22,12A10,10 0 0,0 12,2Z";

			return
				(
					null,
					new List<DTOCategory>
					{
						new DTOCategory(iconPathTest2, "Blue", "Test category1"),
						new DTOCategory(iconPathTest1, "Sienna", "Test category2"),
						new DTOCategory(iconPathTest1, "DarkOrange", "Test category3"),
						new DTOCategory(iconPathTest2, "DimGray", "Test category4"),
						new DTOCategory(iconPathTest1, "DarkMagenta", "Test category5"),
						new DTOCategory(iconPathTest1, "Green", "Test category6"),
						new DTOCategory(iconPathTest1, "DarkKhaki", "Test category7")
					}
				);
		}

		private DTOError SetCategoriesAccount(List<DTOCategory> categories)
		{
			//DTOError
			//	error;

			return null;
		}

		private (DTOError error, List<DTORecord> records) GetRecordsAccount(string login, string password)
		{
			using
			(
				SQLiteConnection
					sqliteConnection = new SQLiteConnection($"Data Source={_urlAndDBFileName}; version=3;")
			)
			{
				List<DTORecord>
					records = new List<DTORecord>()
					{
						new DTORecord
							(0, true, "Test header1", "Test category1", "Test service1", "https://www.testurl1.com",
							"TestLogin1", "TestPassword1", "Test note1, test note1, test note1, test note1, " +
							"test note1, test note1, test note1, test note1, test note1, test note1, test note1."),
						new DTORecord
							(1, false, "Test header2", "Test category2", "Test service2", "https://www.testurl2.com",
							"TestLogin2", "TestPassword2", "Test note2, test note2, test note2, test note2, " +
							"test note2, test note2, test note2, test note2, test note2, test note2, test note2."),
						new DTORecord
							(2, false, "Test header3", "Test category3", "Test service3", "https://www.testurl3.com",
							"TestLogin3", "TestPassword3", "Test note3, test note3, test note3, test note3, " +
							"test note3, test note3, test note3, test note3, test note3, test note3, test note3."),
						new DTORecord
							(3, true, "Test header4", "Test category3", "Test service4", "https://www.testurl4.com",
							"TestLogin4", "TestPassword4", "Test note4, test note4, test note4, test note4, " +
							"test note4, test note4, test note4, test note4, test note4, test note4, test note4."),
						new DTORecord
							(4, false, "Test header5", string.Empty, "Test service5", "https://www.testurl5.com",
							"TestLogin5", "TestPassword5", "Test note5, test note5, test note5, test note5, " +
							"test note5, test note5, test note5, test note5, test note5, test note5, test note5."),
						new DTORecord
							(5, false, "Test header6", string.Empty, "Test service6", "https://www.testurl6.com",
							"TestLogin6", "TestPassword6", "Test note6, test note6, test note6, test note6, " +
							"test note6, test note6, test note6, test note6, test note6, test note6, test note6."),
						new DTORecord
							(6, true, "Test header7", "Test category7", "Test service7", "https://www.testurl7.com",
							"TestLogin7", "TestPassword7", "Test note7, test note7, test note7, test note7, " +
							"test note7, test note7, test note7, test note7, test note7, test note7, test note7.")
					};

				//DataTable
				//	dataTable = new DataTable();

				//$"SELECT * FROM TableName WHERE FieldName NOT NULL" + //Выборка записей в таблице где поле не null
				//SQLiteDataAdapter
				//	sqliteDataAdapter = new SQLiteDataAdapter($"SELECT * FROM {login}", sqliteConnection);

				//sqliteDataAdapter.Fill(dataTable);

				//int
				//	dataTableRowsCount = dataTable.Rows.Count;

				//if (dataTableRowsCount > 0)
				//	for (int index = 0; index < dataTableRowsCount; index++)
				//		collection.Add
				//		(
				//			new DTORecord
				//			(
				//				int.Parse(dataTable.Rows[index][TableRecordsId].ToString()),
				//				$"{dataTable.Rows[index][TableRecordsIsFavourite]}" == "1",
				//				$"{dataTable.Rows[index][TableRecordsCategory]}",
				//				$"{dataTable.Rows[index][TableRecordsURL]}",
				//				$"{dataTable.Rows[index][TableRecordsService]}",
				//				$"{dataTable.Rows[index][TableRecordsLogin]}",
				//				$"{dataTable.Rows[index][TableRecordsPassword]}",
				//				$"{dataTable.Rows[index][TableRecordsNote]}"
				//			)
				//		);

				return
					(
						null,
						records
					);
			}
		}

		private DTOError SetRecordsAccount(List<DTORecord> records)
		{
			//DTOError
			//	error;

			return null;
		}
		//Переименование таблицы: ALTER TABLE TableName RENAME TO NewTableName;
		//Удаление таблицы: DROP TABLE TableName;
		#endregion


		#region Implementing IAccountsDataRepository members
		public DTOError RegisterNewAccount(string login, string password)
		{
			DTOError
				error = DatabaseDirectory.CreateIfNotExist(_urlAndDirectoryNameDb);

			if (error != null)
				return error;

			DataTable
				dataTable = new DataTable();

			using
			(
				SQLiteConnection
					sqliteConnection = new SQLiteConnection($"Data Source={_urlAndDBFileName}; version=3;")
			)
			{
				//sqliteConnection.Open();

				error = CreateTableDataForRegisteredAccountsIfNotExist();

				if (error != null)
					return error;

				string //Не задействовано
					commandTextSelectFromWhereAnd =
						$"SELECT * FROM {TableNameRegisteredAccounts} WHERE " +
						$"{TableRegisteredAccountsLogin}={login} AND " +
						$"{TableRegisteredAccountsPassword}={password};",
					commandTextSelectFrom = $"SELECT * FROM {TableNameRegisteredAccounts};";

				try
				{
					SQLiteDataAdapter
						sqliteDataAdapter = new SQLiteDataAdapter(commandTextSelectFrom, sqliteConnection);

					sqliteDataAdapter.Fill(dataTable);
				}
				catch (Exception exception)
				{
					return
						new DTOError
						(
							ErrorTypes.Fatal,
							$"Таблица в базе данных URL: \"{_urlAndDBFileName}\", хранящая данные зарегистрированных " +
							$"аккаунтов, не была найдена, возникшая ошибка: \"{exception.Message}\"\n" +
							"Дальнейшая корректная работа приложения невозможна!"
						);
				}
			}

			int
				dataTableRowsCount = dataTable.Rows.Count;

			if (dataTableRowsCount > 0)
				for (int index = 0; index < dataTableRowsCount; index++)
				{
					if ($"{dataTable.Rows[index][TableRegisteredAccountsLogin]}" == login)
						return
							new DTOError
							(
								ErrorTypes.Attention,
								$"Аккаунт с логином: \"{login}\" уже зарегистрирован!\n" +
								"Измените логин и повторите регистрацию \"Register\""
							);
				}

			using
			(
				SQLiteConnection
					sqliteConnection = new SQLiteConnection($"Data Source={_urlAndDBFileName}; version=3;")
			)
			{
				sqliteConnection.Open();

				string
					commandTextInsertIntoValues =
						$"INSERT INTO {TableNameRegisteredAccounts} (" +
						$"{TableRegisteredAccountsLogin}, " +
						$"{TableRegisteredAccountsPassword}, " +
						$"{TableRegisteredAccountsTableRegisteredRecords}) " +
						"VALUES (" +
						$"'{login}', " +
						$"'{password}', " +
						$"'{login}');";

				error = ExecutingSQLiteCommands.NonQuery(sqliteConnection, commandTextInsertIntoValues);
			}

			if (error != null)
				return
					new DTOError
					(
						ErrorTypes.Fatal,
						$"В таблицу в базе данных URL: \"{_urlAndDBFileName}\", хранящую данные зарегистрированных " +
						"аккаунтов, не были добавлены данные нового регистрируемого " +
						$"аккаунта, возникшая ошибка: \"{error.Message}\"!\n" +
						"Дальнейшая корректная работа приложения невозможна!"
					);

			using
			(
				SQLiteConnection
					sqliteConnection = new SQLiteConnection($"Data Source={_urlAndDBFileName}; version=3;")
			)
			{
				sqliteConnection.Open();

				string
					commandTextCreateTable =
						$"CREATE TABLE {login} (" +
						$"{TableRecordsService} TEXT," +
						$"{TableRecordsLogin} TEXT," +
						$"{TableRecordsPassword} TEXT," +
						$"{TableRecordsNote} TEXT)";

				error = ExecutingSQLiteCommands.NonQuery(sqliteConnection, commandTextCreateTable);

				if (error != null)
				{
					string
						errorMessage = error.Message;

					string
						commandTextDeleteFrom =
							$"DELETE FROM {TableNameRegisteredAccounts} WHERE " +
							$"{TableRegisteredAccountsLogin} = '{login}'";

					error = ExecutingSQLiteCommands.NonQuery(sqliteConnection, commandTextDeleteFrom);

					sqliteConnection.Dispose();

					return
						new DTOError
						(
							ErrorTypes.Fatal,
							$"Таблица в базе данных URL: \"{_urlAndDBFileName}\", хранящая список записей " +
							$"регистрируемого аккаунта, не была создана, аккаунт с логином: \"{login}\" " +
							$"не был зарегистрирован, возникшая ошибка: \"{errorMessage}\"!\n" +
							"Дальнейшая корректная работа приложения невозможна!"
						);
				}
			}

			return null;
		}

		public DTOError FindAccountAmoungRegistered(string login, string password)
		{
			DTOError
				error = DatabaseDirectory.CreateIfNotExist(_urlAndDirectoryNameDb);

			if (error != null)
				return error;

			DataTable
				dataTable = new DataTable();

			using
			(
				SQLiteConnection
					sqliteConnection = new SQLiteConnection($"Data Source={_urlAndDBFileName}; version=3;")
			)
			{
				sqliteConnection.Open();

				error = CreateTableDataForRegisteredAccountsIfNotExist();

				string
					commandTextSelectFrom = $"SELECT * FROM {TableNameRegisteredAccounts}";

				try
				{
					SQLiteDataAdapter
						sqliteDataAdapter = new SQLiteDataAdapter(commandTextSelectFrom, sqliteConnection);

					sqliteDataAdapter.Fill(dataTable);
				}
				catch (Exception exception)
				{
					return
						new DTOError
						(
							ErrorTypes.Fatal,
							$"Таблица в базе данных URL: \"{_urlAndDBFileName}\", хранящая данные всех " +
							$"зарегистрированных пользователей, не была найдена, возникшая ошибка: \"{exception.Message}\"!\n" +
							"Дальнейшая корректная работа приложения невозможна!"
						);
				}
			}

			int
				dataTableRowsCount = dataTable.Rows.Count;

			if (dataTableRowsCount > 0)
				for (int index = 0; index < dataTableRowsCount; index++)
				{
					if
					(
						$"{dataTable.Rows[index][TableRegisteredAccountsLogin]}" == login
						&& $"{dataTable.Rows[index][TableRegisteredAccountsPassword]}" == password
					)
						return null;
				}

			return
				new DTOError
				(
					ErrorTypes.Attention,
					"Аккаунт, с таким сочетанием Логина и Пароля, не найден среди зарегистрированных!\n" +
					"Во избежание ошибки при заполнении — повторите ввод.\n" +
					"Если аккаунт не был зарегистрирован, пройдите простую процедуру регистрации " +
					"\"Registering a new account\"."
				);
		}

		public DTOAccountDataQuery GetAccountData(string login, string password)
		{
			(DTOError errorGetCategoriesAccount, List<DTOCategory> categories) =
				GetCategoriesAccount();

			(DTOError errorGetRecordsAccount, List<DTORecord> records) =
				GetRecordsAccount(login, password);

			return
				new DTOAccountDataQuery
				(
					null,
					new DTOAccountData
					(
						categories,
						records
					)
				);
		}

		public DTOError SetAccountData(string login, string password, DTOAccountData accountData)
		{
			DTOError
				error =
					SetCategoriesAccount(accountData.CustomCategories)
					?? SetRecordsAccount(accountData.Records);

			return null;
		}
		#endregion


		#region Implementing IDisposable members
		public void Dispose() =>
			GC.SuppressFinalize(this);
		#endregion
	}
}