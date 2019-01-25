using System;
using System.Collections.Generic;
using Autofac;
using Courier.API;
using Courier.API.Enums;
using Courier.API.Models;
using Courier.Model;
using Courier.View;
using CourierMobile.Core.Core.API.Models;
using GalaSoft.MvvmLight.Command;
using Plugin.Toast;
using Refit;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Courier.ViewMoodel
{
	public class ChoiceFolderViewModel : BaseViewModel
	{
		private readonly Company _company;


		public ChoiceFolderViewModel()
		{
		}

		public ChoiceFolderViewModel(Company company)
		{
			_company = company;
			Folders = new List<Tuple<string, Action>>
			{
				new Tuple<string, Action>("На обработку", () => Application.Current.MainPage.Navigation.PushAsync(new DocumentsPage {BindingContext = new DocumentsViewModel(company, DocumentFolder.OnProcess)})),
				new Tuple<string, Action>("Обработанные", () => Application.Current.MainPage.Navigation.PushAsync(new DocumentsPage {BindingContext = new DocumentsViewModel(company, DocumentFolder.Processed)}))
			};
		}

		public async void Init()
		{
			if (Connectivity.NetworkAccess != NetworkAccess.Internet)
			{
				CrossToastPopUp.Current.ShowToastMessage(App.NoInternet);
				return;
			}
			using (App.Container.BeginLifetimeScope())
			{
				var client = App.Container.Resolve<ICustomHttpClient>();
				try
				{
					var result = await client.CourierApiService.Logon(new Credentials
						{ Username = _company.UserLogin, Password = _company.UserPassword });
					client.AuthToken = result.Token;
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


			}
		}
		
		public List<Tuple<string, Action>> Folders { get; set; }

		public RelayCommand<Tuple<string, Action>> OpenFolderCommand => new RelayCommand<Tuple<string, Action>>(OpenFolderExecute);

		public RelayCommand OpenSettingsCommand => new RelayCommand(() => Application.Current.MainPage.Navigation.PushAsync(new SettingsPage()));

		private void OpenFolderExecute(object param)
		{
			if (param is Tuple<string, Action> mode)
			{
				mode.Item2.Invoke();
			}
		}
	}
}