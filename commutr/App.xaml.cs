using System.Linq;
using commutr.Models;
using commutr.Services;
using commutr.ViewModels;
using commutr.Views;
using SimpleInjector;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

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
            container.Register<IGeolocationService, GeolocationService>();
            container.Register<IGeocodingService, GeocodingService>();

            container.Verify();

            Resolver = container.GetInstance<DependencyResolver>();

            var primaryVehicle = Resolver.Resolve<IDataStore<Vehicle>>().GetItemsAsync().Result
                .FirstOrDefault(x => x.IsPrimary);

            if (primaryVehicle != null)
            {
                MainPage = new NavigationPage(new VehicleDetailsPage(new VehicleDetailsViewModel(primaryVehicle)));
            }
            else
            {
                MainPage = new NavigationPage(new VehiclePage());
            }
        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            var locationDataStore = Resolver.Resolve<IDataStore<StationLocation>>();
            var placesService = Resolver.Resolve<IPlacesService>();

            await Task.Run(async () =>
            {
                List<Task<int>> locationTasks = new List<Task<int>>();
                var locations = await locationDataStore.GetItemsAsync();
                foreach (var location in locations)
                {
                    locationTasks.Add(await Task.Run(async () =>
                    {
                        var updatedLocation = await placesService.RefreshPlaceId(location);
                        return locationDataStore.UpdateItemAsync(updatedLocation);
                    }));
                }

                Task.WaitAll(locationTasks.ToArray());
            });
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