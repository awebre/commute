using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using commutr.Models;
using commutr.Services;
using Xamarin.Forms;

namespace commutr.ViewModels
{
    public class MaintenanceLogViewModel : BaseViewModel
    {
        private readonly IDataStore<MaintenanceLog> dataStore;

        public MaintenanceLogViewModel(IDataStore<MaintenanceLog> dataStore)
        {
            this.dataStore = dataStore;

            LoadMaintenanceLogsCommand = new Command(async () => await ExecuteLoadMaintenanceLogs());
        }
        
        public ObservableCollection<MaintenanceLog> MaintenanceLogs { get; set; } = new ObservableCollection<MaintenanceLog>();
        
        public int SelectedVehicleId { get; set; }
        
        public ICommand LoadMaintenanceLogsCommand { get; set; }

        public async Task ExecuteLoadMaintenanceLogs()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                MaintenanceLogs.Clear();
            
                var maintenanceLogs = (await dataStore.GetItemsAsync()).Where(x => x.VehicleId == SelectedVehicleId).ToList();
                maintenanceLogs.ForEach(x => MaintenanceLogs.Add(x));
                maintenanceLogs = maintenanceLogs.OrderByDescending(x => x.Date).ToList();
                MaintenanceLogs = new ObservableCollection<MaintenanceLog>(maintenanceLogs);
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            LoadMaintenanceLogsCommand.Execute(null);
        }
    }
}