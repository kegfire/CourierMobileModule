using Autofac;
using Courier.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Courier
{
    public partial class App : Application
    {
        public App()
        {
            // Initialize Live Reload.
#if DEBUG
            LiveReload.Init();
#endif

            InitializeComponent();
            MainPage = new NavigationPage(new ChoiceFolderPage());
        }

        public static IContainer Container { get; set; }

	    public static string DbName = "retail360.courier";

	    public static string NoInternet = "Ошибка подключения";

		public static string ServerAddress = "https://courier-demo.esphere.ru/api";

		protected override void OnStart()
        {
			// Handle when your app starts
		}

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
