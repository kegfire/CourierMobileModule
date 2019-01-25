using Autofac;
using Courier.ViewMoodel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Courier.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoiceFolderPage : ContentPage
    {
        public ChoiceFolderPage()
        {
            InitializeComponent();
        }

	    protected override void OnAppearing()
	    {
		    base.OnAppearing();
		    var vm = BindingContext as ChoiceFolderViewModel;
		    vm?.Init();
	    }
	}
}