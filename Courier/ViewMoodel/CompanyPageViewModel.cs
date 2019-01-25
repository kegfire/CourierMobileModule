using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Courier.API;
using Courier.API.Models;
using Courier.Model;
using CourierMobile.Core.Core.API.Models;
using GalaSoft.MvvmLight.Command;
using Plugin.Toast;
using Realms;
using Refit;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Courier.ViewMoodel
{
	public class CompanyPageViewModel : BaseViewModel
	{
		private readonly Company _existingCompany;
		private bool _isProcessing;
		private bool _showInnKpp;

		public CompanyPageViewModel() { }

		public CompanyPageViewModel(Company company)
		{
			_existingCompany = company;
			Company = company ?? new Company();
			if (!string.IsNullOrEmpty(_existingCompany?.Inn))
			{
				ShowInnKpp = true;
			}
			//Branches = new List<Company>();
		}

		public string CompanyName => string.IsNullOrEmpty(Company?.CompanyName) ? "Новая компания" : Company.CompanyName;

		public Company Company { get; set; }

		/// <summary>
		/// Показывать строки с ИНН и КПП компании.
		/// </summary>
		public bool ShowInnKpp
		{
			get => _showInnKpp;
			set
			{
				_showInnKpp = value;
				OnPropertyChanged(nameof(ShowInnKpp));
				Application.Current.MainPage.ForceLayout();
			}
		}

		/// <summary>
		/// Признак работы авторизации.
		/// </summary>
		public bool IsProcessing
		{
			get => _isProcessing;
			set
			{
				_isProcessing = value;
				OnPropertyChanged(nameof(IsProcessing));
				OnPropertyChanged(nameof(ButtonVisibility));
			}
		}

		/// <summary>
		/// Видимость кнопки "Авторизоваться".
		/// </summary>
		public bool ButtonVisibility => !IsProcessing;

		/// <summary>
		/// Признак наличия филиалов.
		/// </summary>
		public bool HasBranches { get; set; }

		/// <summary>
		/// Список филиалов.
		/// </summary>
		public List<Company> Branches { get; set; }

		public RelayCommand DeleteCommand => new RelayCommand(DeleteCommandExecute);

		public RelayCommand LogonCommand => new RelayCommand(LogonCommandExecute, LogonCommandCanExecute);

		public bool LogonCommandCanExecute => !string.IsNullOrEmpty(Company?.UserLogin) && !string.IsNullOrEmpty(Company?.UserPassword);

		public bool DeleteButtonVisibility => _existingCompany != null;

		private void LogonCommandExecute()
		{
			if (Connectivity.NetworkAccess != NetworkAccess.Internet)
			{
				CrossToastPopUp.Current.ShowToastMessage(App.NoInternet);
				return;
			}
			IsProcessing = true;
			var realm = Realm.GetInstance(App.DbName);
			if (_existingCompany == null)
			{
				var existingCompany = realm.All<Company>().FirstOrDefault(x => x.UserLogin == Company.UserLogin && x.UserPassword == Company.UserPassword);
				if (existingCompany != null)
				{
					Application.Current.MainPage.DisplayAlert("Ошибка", "Такой аккаунт уже заведён", "Ок");
					IsProcessing = false;
					return;
				}
			}
			
			realm.Write(() => realm.Add(Company, true));
			var companyThreadSafe = ThreadSafeReference.Create(Company);
			Task.Run(() =>
			{
				var realmThread = Realm.GetInstance(App.DbName);
				var company = realmThread.ResolveReference(companyThreadSafe);
				using (App.Container.BeginLifetimeScope())
				{
					var client = App.Container.Resolve<ICustomHttpClient>();
					try
					{
						var result = client.CourierApiService.Logon(new Credentials { Username = company.UserLogin, Password = company.UserPassword }).Result;
						client.AuthToken = result.Token;
						var myCompany = client.CourierApiService.GetInfoBySelf(client.AuthToken).Result;
						realmThread.Write(() =>
						{
							company.CompanyName = myCompany.Name;
							company.Inn = myCompany.Inn;
							company.Kpp = myCompany.Kpp;
						});
						OnPropertyChanged(nameof(CompanyName));
						ShowInnKpp = true;
						Device.BeginInvokeOnMainThread(() => CrossToastPopUp.Current.ShowToastMessage("Успешно. Компания сохранена"));
					}
					catch (Exception e)
					{
						var message = e.Message;
						if (e.InnerException is ApiException exception)
						{
							var error = exception.GetContentAs<ExceptionDetails>();
							message = error.Message;
						}
						Device.BeginInvokeOnMainThread(() => Application.Current.MainPage.DisplayAlert("Ошибка", message, "Закрыть"));
					}
					IsProcessing = false;
				}
			});
		}

		private async void DeleteCommandExecute()
		{
			var realm = Realm.GetInstance(App.DbName);
			if (_existingCompany != null)
			{

				var confirmation = await Application.Current.MainPage.DisplayAlert("Удаление", "Удалить текущую компанию?", "Да", "Нет");
				if (confirmation)
				{
					realm.Write(() => realm.Remove(_existingCompany));
					Application.Current.MainPage.SendBackButtonPressed();
				}
			}
		}
	}
}