using System.Collections.Generic;
using System.Linq;
using Courier.Model;
using Courier.View;
using GalaSoft.MvvmLight.Command;
using Realms;
using Realms.Exceptions;
using Xamarin.Forms;

namespace Courier.ViewMoodel
{
    public class StartPageViewModel : BaseViewModel
    {
	    public async void Init()
	    {
		    
			Realm realm;
		    try
		    {
			    realm = Realm.GetInstance(App.DbName);
		    }
		    catch (RealmMigrationNeededException r)
		    {
			    Realm.DeleteRealm(new RealmConfiguration(App.DbName));
			    realm = Realm.GetInstance(App.DbName);
		    }
			Organizations = realm.All<Company>().ToList();
			OnPropertyChanged(nameof(Organizations));
		    if (Organizations.Count == 0)
		    {
			    var confirmation = await Application.Current.MainPage.DisplayAlert("Начало работы", "Для начала работы необходимо добавить компанию в настройках.", "Добавить", "Отмена");
			    if (confirmation)
			    {
				   await Application.Current.MainPage.Navigation.PushAsync(new CompanyPage {BindingContext = new CompanyPageViewModel(null)});
			    }
			}
		}

		public IList<Company> Organizations { get; set; }

        public RelayCommand<Company> ChoiceFolderCommand => new RelayCommand<Company>(ChoiceFolderCommandExecute);

        public RelayCommand OpenSettingsCommand => new RelayCommand(() => Application.Current.MainPage.Navigation.PushAsync(new SettingsPage()));

		private void ChoiceFolderCommandExecute(object param)
	    {
		    if (param is Company company)
		    {
			    Application.Current.MainPage.Navigation.PushAsync(new ChoiceFolderPage
				    {BindingContext = new ChoiceFolderViewModel(company) });
		    }
	    }
	}
}