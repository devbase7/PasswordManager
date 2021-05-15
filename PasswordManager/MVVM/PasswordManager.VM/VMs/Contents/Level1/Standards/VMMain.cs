#region Namespaces
#region Namespaces Microsoft
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
#endregion


#region Namespaces others
using PasswordManager.DTO.DTOs;
using PasswordManager.VM.Infrastructure.AVMs;
using PasswordManager.VM.Infrastructure.ChangingContent;
using PasswordManager.VM.Infrastructure.Other;
using PasswordManager.VM.VMs.Components;
using PasswordManager.VM.VMs.Contents.Level1.Nodes;
using PasswordManager.VM.VMs.Contents.Level2.Standards;
#endregion
#endregion


namespace PasswordManager.VM.VMs.Contents.Level1.Standards
{
	public sealed class VMMain : AVMInfrastructure, IContentNavigation
	{
		#region Constants internal
		//Временное решение.
		private const string
			VectorIconPathAll = "M128,21.33v85.33A21.33,21.33,0,0,1,106.67,128H21.33A21.33,21.33,0,0,1,0," +
				"106.67V21.33A21.33,21.33,0,0,1,21.33,0h85.33A21.33,21.33,0,0,1,128,21.33ZM298.67,0H213.33A21.33," +
				"21.33,0,0,0,192,21.33v85.33A21.33,21.33,0,0,0,213.33,128h85.33A21.33,21.33,0,0,0,320," +
				"106.67V21.33A21.33,21.33,0,0,0,298.67,0Zm192,0H405.33A21.33,21.33,0,0,0,384,21.33v85.33A21.33," +
				"21.33,0,0,0,405.33,128h85.33A21.33,21.33,0,0,0,512,106.67V21.33A21.33,21.33,0,0,0,490.67,0Zm-384," +
				"192H21.33A21.33,21.33,0,0,0,0,213.33v85.33A21.33,21.33,0,0,0,21.33,320h85.33A21.33,21.33,0,0,0,128," +
				"298.67V213.33A21.33,21.33,0,0,0,106.67,192Zm192,0H213.33A21.33,21.33,0,0,0,192,213.33v85.33A21.33," +
				"21.33,0,0,0,213.33,320h85.33A21.33,21.33,0,0,0,320,298.67V213.33A21.33,21.33,0,0,0,298.67,192Zm192," +
				"0H405.33A21.33,21.33,0,0,0,384,213.33v85.33A21.33,21.33,0,0,0,405.33,320h85.33A21.33,21.33,0,0,0," +
				"512,298.67V213.33A21.33,21.33,0,0,0,490.67,192Zm-384,192H21.33A21.33,21.33,0,0,0,0," +
				"405.33v85.33A21.33,21.33,0,0,0,21.33,512h85.33A21.33,21.33,0,0,0,128,490.67V405.33A21.33,21.33,0,0," +
				"0,106.67,384Zm192,0H213.33A21.33,21.33,0,0,0,192,405.33v85.33A21.33,21.33,0,0,0,213.33," +
				"512h85.33A21.33,21.33,0,0,0,320,490.67V405.33A21.33,21.33,0,0,0,298.67,384Zm192,0H405.33A21.33," +
				"21.33,0,0,0,384,405.33v85.33A21.33,21.33,0,0,0,405.33,512h85.33A21.33,21.33,0,0,0,512," +
				"490.67V405.33A21.33,21.33,0,0,0,490.67,384Z",
			VectorIconPathFavourite = "M29.895," +
				"12.52c-0.235-0.704-0.829-1.209-1.549-1.319l-7.309-1.095l-3.29-6.984C17.42,2.43,16.751,2,16," +
				"2  s-1.42,0.43-1.747,1.122l-3.242,6.959l-7.357,1.12c-0.72,0.11-1.313,0.615-1.549,1.319c-0.241," +
				"0.723-0.063,1.507,0.465,2.046  l5.321,5.446l-1.257,7.676c-0.125,0.767,0.185,1.518,0.811," +
				"1.959c0.602,0.427,1.376,0.469,2.02,0.114l6.489-3.624l6.581,3.624  c0.646,0.355,1.418,0.311," +
				"2.02-0.114c0.626-0.441,0.937-1.192,0.811-1.959l-1.259-7.686l5.323-5.436  C29.958,14.027,30.136," +
				"13.243,29.895,12.52z",
			VectorIconPathNoCategory = "M13.46,12L19,17.54V19H17.54L12,13.46L6.46,19H5V17.54L10.54,12L5," +
				"6.46V5H6.46L12,10.54L17.54,5H19V6.46L13.46,12Z";
		#endregion


		#region Fields internal
		private ObservableCollection<VMRecord>
			_recordsSelectedCategory;

		private VMCategory
			_selectedCategory;

		private VMRecord
			_selectedRecord;

		private VMCategoryRecord
			_selectedCategoryRecord;

		private int
			_indexSelectedFixedCategory,
			_indexSelectedCustomCategory = -1,
			_indexSelectedRecord = -1,
			_indexSelectedCategoryRecord = -1;

		private string
			_header,
			_category,
			_service,
			_url,
			_login,
			_password,
			_note;

		private bool
			_recordingInputOutputPanelIsEnabled,
			_headerIsSetFocus,
			_addRecordIsVisible;

		private RelayCommand
			_logOut,
			_changingLoginPassword,
			_addRecord,
			_newRecordTransformCancel;

		private VMRecord
			_storageSelectedRecord;
		#endregion


		#region Constructors interface
		public VMMain(DTOAccountData accountData, InfrastructureArgs infrastructureArgs)
		: base(infrastructureArgs)
		{
			if (accountData != null)
			{
				if (accountData.CustomCategories != null)
					SetCustomCategories(accountData.CustomCategories);

				if (accountData.Records != null)
					FillingRecordsInCategories(accountData.Records);
			}
		}
		#endregion


		#region Properties
		#region Properties interface
		public VMRecord StorageSelectedRecord
		{
			get =>
				_storageSelectedRecord;

			set
			{
				_storageSelectedRecord = value;

				if (value != null)
				{
					Header = value.Header;
					Category = value.Category;
					Service = value.Service;
					URL = value.URL;
					Login = value.Login;
					Password = value.Password;
					Note = value.Note;
				}
			}
		}
		#endregion


		#region Properties data infrastructure for the View interface
		public List<VMCategory> FixedCategories { get; } = new List<VMCategory>
		{
			new VMCategory(VectorIconPathAll, "Black", "All"),
			new VMCategory(VectorIconPathFavourite, "Yellow", "Favourite"),
			new VMCategory(VectorIconPathNoCategory, "Red", "No category")
		};

		public ObservableCollection<VMCategory> CustomCategories { get; set; }

		public ObservableCollection<VMCategoryRecord> CustomCategoriesRecord { get; set; }

		public ObservableCollection<VMRecord> RecordsSelectedCategory
		{
			get =>
				_recordsSelectedCategory;

			set
			{
				_recordsSelectedCategory = value;
				OnPropertyChanged();

				if (value.Contains(_storageSelectedRecord))
					SelectedRecord = _storageSelectedRecord;
			}
		}

		public VMCategory SelectedCategory
		{
			get =>
				_selectedCategory;

			set
			{
				_selectedCategory = value;

				if (value != null)
					RecordsSelectedCategory = value.Records;
			}
		}

		public VMRecord SelectedRecord
		{
			get =>
				_selectedRecord;

			set
			{
				_selectedRecord = value;
				OnPropertyChanged();

				if (value != null)
				{
					StorageSelectedRecord = value;

					AddRecordIsVisible = false;
					RecordingInputOutputPanelIsEnabled = true;

					Header = value.Header;
					Category = value.Category;
					Service = value.Service;
					URL = value.URL;
					Login = value.Login;
					Password = value.Password;
					Note = value.Note;
				}
			}
		}

		public VMCategoryRecord SelectedCategoryRecord
		{
			get =>
				_selectedCategoryRecord;

			set
			{
				_selectedCategoryRecord = value;
				OnPropertyChanged();
			}
		}

		public int IndexSelectedFixedCategory
		{
			get =>
				_indexSelectedFixedCategory;

			set
			{
				if (value > -1)
					IndexSelectedCustomCategory = -1;

				_indexSelectedFixedCategory = value;
				OnPropertyChanged();
			}
		}

		public int IndexSelectedCustomCategory
		{
			get =>
				_indexSelectedCustomCategory;

            set
            {
                if (value > -1)
                    IndexSelectedFixedCategory = -1;

                _indexSelectedCustomCategory = value;
                OnPropertyChanged();
            }
		}

		public int IndexSelectedRecord
		{
			get =>
				_indexSelectedRecord;

			set
			{
				_indexSelectedRecord = value;
				OnPropertyChanged();
			}
		}

		public int IndexSelectedCategoryRecord
		{
			get =>
				_indexSelectedCategoryRecord;

			set
			{
				_indexSelectedCategoryRecord = value;
				OnPropertyChanged();
			}
		}

		public string Header
		{
			get =>
				_header;

			set
			{
				if (_storageSelectedRecord != null)
					_storageSelectedRecord.Header = value;

				_header = value;
				OnPropertyChanged();
			}
		}

		public string Category
		{
			get =>
				_category;

			set
			{
				if (_storageSelectedRecord != null)
					_storageSelectedRecord.Category = value;

				_category = value;
				OnPropertyChanged();
			}
		}

		public string Service
		{
			get =>
				_service;

			set
			{
				if (_storageSelectedRecord != null)
					_storageSelectedRecord.Service = value;

				_service = value;
				OnPropertyChanged();

				_addRecord.RaiseCanExecuteChanged();
			}
		}

		public string URL
		{
			get =>
				_url;

			set
			{
				if (_storageSelectedRecord != null)
					_storageSelectedRecord.URL = value;

				_url = value;
				OnPropertyChanged();
			}
		}

		public string Login
		{
			get =>
				_login;

			set
			{
				if (_storageSelectedRecord != null)
					_storageSelectedRecord.Login = value;

				_login = value;
				OnPropertyChanged();
			}
		}

		public string Password
		{
			get =>
				_password;

			set
			{
				if (_storageSelectedRecord != null)
					_storageSelectedRecord.Password = value;

				_password = value;
				OnPropertyChanged();
			}
		}

		public string Note
		{
			get =>
				_note;

			set
			{
				if (_storageSelectedRecord != null)
					_storageSelectedRecord.Note = value;

				_note = value;
				OnPropertyChanged();
			}
		}

		public bool RecordingInputOutputPanelIsEnabled
		{
			get =>
				_recordingInputOutputPanelIsEnabled;

			set
			{
				_recordingInputOutputPanelIsEnabled = value;
				OnPropertyChanged();
			}
		}

		public bool HeaderIsSetFocus
		{
			get =>
				_headerIsSetFocus;

			set
			{
				_headerIsSetFocus = value;
				OnPropertyChanged();
			}
		}

		public bool AddRecordIsVisible
		{
			get =>
				_addRecordIsVisible;

			set
			{
				_addRecordIsVisible = value;
				OnPropertyChanged();
			}
		}
		#endregion


		#region Properties commands infrastructure for the View interface
		public ICommand LogOut =>
			_logOut
			?? (_logOut = new RelayCommand(Execute_LogOut));

		public ICommand ChangingLoginPassword =>
			_changingLoginPassword
			?? (_changingLoginPassword = new RelayCommand(Execute_ChangingLoginPassword));

		public ICommand AddRecord =>
			_addRecord
			?? (_addRecord = new RelayCommand(Execute_AddRecord, CanExecute_AddRecord));

		public ICommand NewRecordTransformCancel =>
			_newRecordTransformCancel
			?? (_newRecordTransformCancel =
				new RelayCommand(Execute_NewRecordTransformCancel, CanExecute_NewRecordTransformCancel));
		#endregion
		#endregion


		#region Methods
		#region Methods internal
		private void SetCustomCategories(List<DTOCategory> customCategories)
		{
			CustomCategories = new ObservableCollection<VMCategory>();
			CustomCategoriesRecord = new ObservableCollection<VMCategoryRecord>();

			foreach (var customCategory in customCategories)
			{
				CustomCategories.Add
				(
					new VMCategory
					(
						customCategory.IconPath,
						customCategory.IconColor,
						customCategory.Name
					)
				);

				CustomCategoriesRecord.Add
				(
					new VMCategoryRecord
					(
						customCategory.IconPath,
						customCategory.IconColor,
						customCategory.Name
					)
				);
			}
		}

		private void FillingRecordsInCategories(List<DTORecord> records)
		{
			VMRecord
				recordToAdd;

			bool
				customCategoriesIsExisted = CustomCategories != null;

			foreach (var record in records)
			{
				recordToAdd = new VMRecord
				(
					record.IsFavourite,
					record.Header,
					record.Category,
					record.Service,
					record.URL,
					record.Login,
					record.Password,
					record.Note
				);

				FixedCategories[0].Records.Add(recordToAdd);

				if (recordToAdd.IsFavourite)
					FixedCategories[1].Records.Add(recordToAdd);

				if (string.IsNullOrEmpty(recordToAdd.Category))
					FixedCategories[2].Records.Add(recordToAdd);

				if (customCategoriesIsExisted)
					foreach (var customCategory in CustomCategories)
						if (customCategory.Name == recordToAdd.Category)
						{
							customCategory.Records.Add(recordToAdd);
							break;
						}
			}
		}

		private void ClearRecordFields()
		{
			Header = string.Empty;
			Category = string.Empty;
			Service = string.Empty;
			URL = string.Empty;
			Login = string.Empty;
			Password = string.Empty;
			Note = string.Empty;
		}
		#endregion


		#region Methods commands for the View internal
		//'LogOut'.
		private void Execute_LogOut(object commandParameter)
		{
			MessageBoxResult
				messageBoxResult = MessageBox.Show
				(
					"Вы действительно хотите выйти из аккаунта?",
					"Question",
					MessageBoxButton.OKCancel,
					MessageBoxImage.Question,
					MessageBoxResult.OK
				);

			if (messageBoxResult == MessageBoxResult.OK)
			{
				VMAuthorization
					vmAuthorization;

				var
					(errorAuthorizationStateIsRememberMe, authorizationStateIsRememberMe) =
						MDatabaseRepositoriesFacade.GetAuthorizationStateIsRememberMe();

				InfrastructureArgs
					infrastructureArgs = new InfrastructureArgs(MDatabaseRepositoriesFacade, ChangingContentService);

				if
				(
					authorizationStateIsRememberMe
					&& errorAuthorizationStateIsRememberMe == null
				)
				{
					DTOAuthorizationStateQuery
						authorizationStateQuery = MDatabaseRepositoriesFacade.GetAuthorizationState();

					vmAuthorization = new VMAuthorization(authorizationStateQuery, infrastructureArgs);

					ChangingContentService.SetCurrentContent(vmAuthorization);
				}
				else
				{
					vmAuthorization = new VMAuthorization(infrastructureArgs);

					ChangingContentService.SetCurrentContent(vmAuthorization);
				}
			}
		}

		//'ChangingLoginPassword'.
		private void Execute_ChangingLoginPassword(object commandParameter)
		{
			ChangingContent<AVMBase>
				changingContentLevel2 = new ChangingContent<AVMBase>();

			VMChangingLoginPassword
				vmChangingLoginPassword = new VMChangingLoginPassword(changingContentLevel2);

			InfrastructureArgs
				infrastructureArgs = new InfrastructureArgs(MDatabaseRepositoriesFacade, changingContentLevel2);

			VMLoginPassword
				vmLoginPassword = new VMLoginPassword(infrastructureArgs);

			changingContentLevel2.SetCurrentContent(vmLoginPassword);

			ChangingContentService.SetCurrentContent(vmChangingLoginPassword);
		}

		//'AddRecord'.
		private void Execute_AddRecord(object commandParameter)
		{
			VMRecord
				recordToAdd = new VMRecord
				(
					false,
					_header,
					_selectedCategoryRecord != null
						? _selectedCategoryRecord.Name
						: string.Empty,
					_service,
					_url,
					_login,
					_password,
					_note
				);

			ClearRecordFields();

			FixedCategories[0].Records.Add(recordToAdd);

			if (recordToAdd.Category == string.Empty)
				FixedCategories[2].Records.Add(recordToAdd);
			else
				foreach (var customCategory in CustomCategories)
				{
                    if (customCategory.Name == recordToAdd.Category)
                    {
                        //var
                        //	queryCategories =
                        //		from item in accountData.Categories
                        //		orderby item.Name ascending
                        //		select
                        //			new
                        //			{
                        //				customCategory.IconPath,
                        //				customCategory.IconColor,
                        //				customCategory.Name
                        //			};
                        customCategory.Records.Add(recordToAdd);
                        break;
                    }
				}

			HeaderIsSetFocus = true;
		}

		private bool CanExecute_AddRecord(object commandParameter) =>
			!string.IsNullOrEmpty(_service);

		//'NewRecordTransformCancel'.
		private void Execute_NewRecordTransformCancel(object commandParameter)
		{
			_storageSelectedRecord = null;
			IndexSelectedRecord = -1;

			ClearRecordFields();

			if (_addRecordIsVisible)
			{
				AddRecordIsVisible = false;
				RecordingInputOutputPanelIsEnabled = false;
			}
			else
			{
				RecordingInputOutputPanelIsEnabled = true;
				HeaderIsSetFocus = true;
				AddRecordIsVisible = true;
			}
		}

		private bool CanExecute_NewRecordTransformCancel(object commandParameter) =>
			true;
		#endregion
		#endregion


		#region Implementing IContentNavigation members
		public void Back()
		{
			MessageBoxResult
				messageBoxResult = MessageBox.Show
				(
					"Вы действительно хотите выйти из аккаунта?",
					"Question",
					MessageBoxButton.OKCancel,
					MessageBoxImage.Question,
					MessageBoxResult.OK
				);

			if (messageBoxResult == MessageBoxResult.OK)
			{
				VMAuthorization
					vmAuthorization;

				var
					(errorAuthorizationStateIsRememberMe, authorizationStateIsRememberMe) =
						MDatabaseRepositoriesFacade.GetAuthorizationStateIsRememberMe();

				InfrastructureArgs
					infrastructureArgs = new InfrastructureArgs(MDatabaseRepositoriesFacade, ChangingContentService);

				if
				(
					authorizationStateIsRememberMe
					&& errorAuthorizationStateIsRememberMe == null
				)
				{
					DTOAuthorizationStateQuery
						authorizationStateQuery = MDatabaseRepositoriesFacade.GetAuthorizationState();

					vmAuthorization = new VMAuthorization(authorizationStateQuery, infrastructureArgs);

					ChangingContentService.SetCurrentContent(vmAuthorization);
				}
				else
				{
					vmAuthorization = new VMAuthorization(infrastructureArgs);

					ChangingContentService.SetCurrentContent(vmAuthorization);
				}
			}
		}
		#endregion
	}
}