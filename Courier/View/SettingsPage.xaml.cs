using Courier.ViewMoodel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Courier.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			var vm = BindingContext as SettingsPageViewModel;
			vm?.Init();

		}
	}
}