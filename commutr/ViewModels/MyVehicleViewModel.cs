using System.Threading.Tasks;
using commutr.Models;
using commutr.Services;

namespace commutr.ViewModels
{
    public class MyVehicleViewModel : BaseViewModel
    {
        private readonly IDataStore<Vehicle> dataStore;
        public MyVehicleViewModel(IDataStore<Vehicle> dataStore)
        {
            this.dataStore = dataStore;
        }
                
        public int VehicleId { get; set; }
        
        public Vehicle Vehicle { get; set; }

        public async Task GetVehicle()
        {
            Vehicle = await dataStore.GetItemAsync(VehicleId);
        }
    }
}