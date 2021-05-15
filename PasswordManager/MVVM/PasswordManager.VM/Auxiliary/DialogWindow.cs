#region Namespaces
#region Namespaces Microsoft
using System.Media;
using System.Windows;
#endregion


#region Namespaces others
using PasswordManager.DTO.Auxiliary;
using PasswordManager.DTO.DTOs;
#endregion
#endregion


namespace PasswordManager.VM.Auxiliary
{
	internal static class DialogWindow
	{ //PresentationFramework.dll
		#region Methods static interface
		internal static void Selector(DTOError error)
		{
			switch (error.Type)
			{
				case ErrorTypes.Attention:
					SystemSounds.Hand.Play();

					MessageBox.Show
					(
						error.Message,
						"Attention!",
						MessageBoxButton.OK,
						MessageBoxImage.Warning
					);
					//Не найден с таким сочетанием Логина и Пароля аккаунт.
					break;

				case ErrorTypes.Warning:
					SystemSounds.Hand.Play();

					MessageBox.Show
					(
						error.Message,
						"Warning",
						MessageBoxButton.OK,
						MessageBoxImage.Warning
					);
					//Не создана таблица заполнения формы авторизации/Не установлены значения по умолчанию
					// в таблице заполнения формы авторизации.

					//Не создана таблица заполнения формы авторизации/Не установлены значения по умолчанию
					// в таблице заполнения формы авторизации/Не обновлены значения в таблице заполнения
					// формы авторизации.

					//Не создана директория для размещения базы данных/Не создана сама база данных/Не создана
					// таблица, хранящая данные всех зарегистрированных аккаунтов.
					break;

				case ErrorTypes.Fatal:
					SystemSounds.Hand.Play();

					MessageBox.Show
					(
						error.Message,
						"Fatal error!",
						MessageBoxButton.OK,
						MessageBoxImage.Error
					);
					//Не создана директория для размещения базы данных/Не создана сама база данных.
					//Не создана таблица, хранящая данные всех зарегистрированных аккаунтов.
					break;
			}
		}
		#endregion
	}
}