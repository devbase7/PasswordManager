#region Namespaces
#region Namespaces Microsoft
using System;
using System.Data.SQLite;
#endregion


#region Namespaces others
using PasswordManager.DTO.Auxiliary;
using PasswordManager.DTO.DTOs;
#endregion
#endregion


namespace PasswordManager.M.Auxiliary
{
	internal static class ExecutingSQLiteCommands
	{
		#region Methods static interface
		internal static DTOError NonQuery(SQLiteConnection sqliteConnection, string commandText)
		{
			SQLiteCommand
				sqliteCommand = new SQLiteCommand(commandText, sqliteConnection);

			try
			{
				sqliteCommand.ExecuteNonQuery();
			}
			catch (Exception exception)
			{
				return
					new DTOError
					(
						ErrorTypes.Fatal,
						exception.Message
					);
			}

			return null;
		}

		internal static (DTOError error, object result) Scalar(SQLiteConnection sqliteConnection, string commandText)
		{
			SQLiteCommand
				sqliteCommand = new SQLiteCommand(commandText, sqliteConnection);

			object
				result;

			try
			{
				result = sqliteCommand.ExecuteScalar();
			}
			catch (Exception exception)
			{
				return
					(
						new DTOError
						(
							ErrorTypes.Fatal,
							exception.Message
						),
						null
					);
			}

			return
				(
					null,
					result
				);
		}
		#endregion
	}
}