using System.ComponentModel;
using System.Runtime.CompilerServices;
using Autofac;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace Courier.ViewMoodel
{
    /// <summary>
    /// Базовый класс для ViewModel.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected BaseViewModel()
        {

        }

	    public RelayCommand OpenMenuCommand => new RelayCommand(() =>
	    {
		    var menuPage = App.Container.Resolve(typeof(ContentPage));
		    Application.Current.MainPage.Navigation.PushAsync(menuPage as ContentPage);
	    });

		public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}