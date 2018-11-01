using System.Threading.Tasks;
using System.Windows.Input;
using commutr.Models;
using commutr.Services;
using commutr.Views;
using Xamarin.Forms;

namespace commutr.ViewModels
{
    public class MyVehicleViewModel : BaseViewModel
    {
        private readonly IDataStore<Vehicle> dataStore;
        public MyVehicleViewModel(IDataStore<Vehicle> dataStore)
        {
            this.dataStore = dataStore;

            EditVehicleCommand = new Command(EditVehicle);
        }
        
        public int VehicleId { get; set; }
        
        public Vehicle Vehicle { get; set; }
        
        public ICommand EditVehicleCommand { get; }

        public async Task GetVehicle()
        {
            Vehicle = await dataStore.GetItemAsync(VehicleId);
            
        }

        private async void EditVehicle()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NewVehiclePage(Vehicle));
        }
    }
}