#region Namespaces
#region Namespaces Microsoft
using System;
using System.IO;
#endregion


#region Namespaces others
using PasswordManager.DTO.Auxiliary;
using PasswordManager.DTO.DTOs;
#endregion
#endregion


namespace PasswordManager.M.Auxiliary
{
	internal static class DatabaseDirectory
	{
		#region Methods static interface
		internal static DTOError CreateIfNotExist(string urlAndDirectoryNameDb)
		{
			try
			{
				Directory.CreateDirectory(urlAndDirectoryNameDb);
			}
			catch (Exception exception)
			{
				return
					new DTOError
					(
						ErrorTypes.Fatal,
						$"Директория URL: \"{urlAndDirectoryNameDb}\", для размещения базы данных, " +
						$"не была создана, возникшая ошибка: \"{exception.Message}\"!\n" +
						"Дальнейшая корректная работа приложения невозможна!"
					);
			}

			return null;
		}
		#endregion
	}
}