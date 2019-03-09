using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using commutr.Models;
using commutr.Services;
using PropertyChanged;
using Xamarin.Forms;

namespace commutr.ViewModels
{
    public class AddFillUpViewModel : BaseViewModel
    {
        private readonly IDataStore<FillUp> fillUpsDataStore;
        private readonly IDataStore<Vehicle> vehicleDataStore;
        private readonly IDataStore<Location> locationDataStore;
        private readonly IPlacesService placesService;
        private FillUp fillUp;
        private List<Location> locations;
        private Location location;

        public AddFillUpViewModel(
            IDataStore<FillUp> fillUpsDataStore,
            IDataStore<Vehicle> vehicleDataStore,
            IDataStore<Location> locationDataStore,
            IPlacesService placesService)
        {
            this.fillUpsDataStore = fillUpsDataStore;
            this.vehicleDataStore = vehicleDataStore;
            this.locationDataStore = locationDataStore;
            this.placesService = placesService;

            SaveFillUpCommand = new Command(async () => await ExecuteSaveFillUpCommand());
            if (fillUp == null)
            {
                fillUp = new FillUp();
            }
            fillUp.Date = DateTime.Today;
            Title = "Fill Up";
        }


        public FillUp FillUp
        {
            get => fillUp;
            set => fillUp = value;
        }

        public List<Location> Locations
        {
            get => locations;
            set => locations = value;
        }

        public Location Location
        {
            get => location;
            set => location = value;
        }

        public async Task GetNearbyPlaces()
        {
            Locations = await placesService.GetNearByPlaces();
        }

        public async Task GetExistingPlaces()
        {
            if (fillUp != null && fillUp.LocationId.HasValue)
            {
                var existingLocation = await locationDataStore.GetItemAsync(fillUp.LocationId.Value);
                Locations = await locationDataStore.GetItemsAsync();
                Location = existingLocation;
            }
        }

        public ICommand SaveFillUpCommand { get; }

        private async Task ExecuteSaveFillUpCommand()
        {
            if (location != null)
            {
                var location = locationDataStore.GetItemsAsync().Result.FirstOrDefault(x => x.PlaceId == this.location.PlaceId);
                if (location == null)
                {
                    fillUp.LocationId = await locationDataStore.AddItemAsync(new Location
                    {
                        PlaceId = this.location.PlaceId,
                        Name = this.location.Name,
                        Address = this.location.Address
                    });
                }
                else
                {
                    fillUp.LocationId = location.Id;
                }
            }

            if (fillUp.Id == 0)
            {
                var vehicle = vehicleDataStore.GetItemsAsync().Result.FirstOrDefault(x => x.Id == fillUp.VehicleId);
                if (vehicle != null)
                {
                    vehicle.Odometer += fillUp.Distance;
                    await vehicleDataStore.UpdateItemAsync(vehicle);
                }

                await fillUpsDataStore.AddItemAsync(fillUp);
            }
            else
            {
                await fillUpsDataStore.UpdateItemAsync(fillUp);
            }

            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}