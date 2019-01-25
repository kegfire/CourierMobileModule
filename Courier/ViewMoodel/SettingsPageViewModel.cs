using System.Collections.Generic;
using Courier.Model;
using Courier.View;
using GalaSoft.MvvmLight.Command;
using Realms;
using Xamarin.Forms;

namespace Courier.ViewMoodel
{
    public class SettingsPageViewModel : BaseViewModel
    {
	    private Realm _realm;

        public IEnumerable<Company> Companies { get; set; }

        public Company SelectedCompany { get; set; }

        public RelayCommand OpenCompanySettings => new RelayCommand(() => Application.Current.MainPage.Navigation.PushAsync(new CompanyPage { BindingContext = new CompanyPageViewModel(SelectedCompany) }));

		public void Init()
	    {
		    _realm = Realm.GetInstance(App.DbName);
		    Companies = _realm.All<Company>();
			OnPropertyChanged(nameof(Companies));
	    }
    }
}