using System;
using commute.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using commute.Views;
using SimpleInjector;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace commute
{
    public partial class App : Application
    {

        public App(Container container)
        {
            InitializeComponent();

            MainPage = new MainPage();
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
    }
}
