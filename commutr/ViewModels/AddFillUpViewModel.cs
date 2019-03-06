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
        private readonly IPlacesService placesService;
        private FillUp fillUp;
        private List<Place> places;

        public AddFillUpViewModel(
            IDataStore<FillUp> fillUpsDataStore, 
            IDataStore<Vehicle> vehicleDataStore, 
            IPlacesService placesService)
        {
            this.fillUpsDataStore = fillUpsDataStore;
            this.vehicleDataStore = vehicleDataStore;
            this.placesService = placesService;

            SaveFillUpCommand = new Command(ExecuteSaveFillUpCommand);
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

        public List<Place> Places
        {
            get => places;
            set => places = value;
        }

        public async Task GetNearbyPlaces()
        {
            places = await placesService.GetNearByPlaces();
        }

        public ICommand SaveFillUpCommand { get; }

        private void ExecuteSaveFillUpCommand()
        {
            if (fillUp.Id == 0)
            {
                var vehicle = vehicleDataStore.GetItemsAsync().Result.FirstOrDefault(x => x.Id == fillUp.VehicleId);
                if (vehicle != null)
                {
                    vehicle.Odometer += fillUp.Distance;
                    vehicleDataStore.UpdateItemAsync(vehicle);
                }
                
                fillUpsDataStore.AddItemAsync(fillUp);
            }
            else
            {         
                fillUpsDataStore.UpdateItemAsync(fillUp);
            }

            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}