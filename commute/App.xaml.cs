using commutr.Services;
using commutr.Views;
using SimpleInjector;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace commutr
{
    public partial class App : Xamarin.Forms.Application
    {
        public App(Container container)
        {
            InitializeComponent();

            container.Register<DependencyResolver>();
            
            container.Register(typeof(IDataStore<>), typeof(SqliteDataStore<>));
            
            var rootPage = new NavigationPage(new VehiclePage());
            container.Register(() => new NavigationService(rootPage));
            container.Verify();

            Resolver = container.GetInstance<DependencyResolver>();

            MainPage = rootPage;
        }

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

        public static DependencyResolver Resolver { get; protected set; }

    }
}