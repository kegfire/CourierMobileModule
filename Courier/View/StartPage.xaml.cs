using Autofac;
using Courier.ViewMoodel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Courier.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartPage : ContentPage
	{
		public StartPage ()
		{
			InitializeComponent ();
		}

		public StartPage(IContainer container)
		{
			App.Container = container;
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			var vm = BindingContext as StartPageViewModel;
			vm?.Init();
		}
	}
}