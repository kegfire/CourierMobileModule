using Courier.ViewMoodel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Courier.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DocumentsPage : ContentPage
	{
		public DocumentsPage ()
		{
			InitializeComponent ();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			var vm = BindingContext as DocumentsViewModel;
			vm?.Init();
		}

		protected override void OnDisappearing()
		{
			var vm = BindingContext as DocumentsViewModel;
			vm?.OnBackButtonPressed();
			base.OnDisappearing();
		}
	}
}