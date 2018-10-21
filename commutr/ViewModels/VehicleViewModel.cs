using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using commutr.Views;
using commutr.Models;
using commutr.Services;
using Xamarin.Forms;

namespace commutr.ViewModels
{
    public class VehicleViewModel : BaseViewModel
    {
        public ObservableCollection<Vehicle> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        private readonly IDataStore<Vehicle> dataStore;
        private Vehicle selectedVehicle;

        public VehicleViewModel(IDataStore<Vehicle> dataStore)
        {
            this.dataStore = dataStore;

            Title = "Vehicles";
            Items = new ObservableCollection<Vehicle>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewVehiclePage, Vehicle>(this, "AddVehicle", async (obj, item) =>
            {
                var newItem = item;
                Items.Add(newItem);
                await dataStore.AddItemAsync(newItem);
            });

            DeleteVehicleCommand = new Command<Vehicle>(DeleteVehicle);
            MakePrimaryCommand = new Command<Vehicle>(MakePrimary);
        }

        public ICommand DeleteVehicleCommand { get; }

        public ICommand MakePrimaryCommand { get; }

        public Vehicle SelectedVehicle
        {
            get => selectedVehicle;
            set
            {
                selectedVehicle = value;

                if (selectedVehicle != null)
                {
                    var detailsPage =
                        new NavigationPage(new VehicleDetailsPage(new VehicleDetailsViewModel(selectedVehicle)));
                    Application.Current.MainPage = detailsPage;
                }
            }
        }

        public async void DeleteVehicle(Vehicle vehicle)
        {
            var result = await dataStore.DeleteItemAsync(vehicle.Id);

            if (result == 1)
            {
                Items.Remove(vehicle);
            }
        }

        public async void MakePrimary(Vehicle newPrimary)
        {
            var vehicles = await dataStore.GetItemsAsync();
            var previousPrimary = vehicles.FirstOrDefault(x => x.IsPrimary);
            if (previousPrimary != null)
            {
                previousPrimary.IsPrimary = false;
                await dataStore.UpdateItemAsync(previousPrimary);
            }

            newPrimary.IsPrimary = true;
            await dataStore.UpdateItemAsync(newPrimary);
            await ExecuteLoadItemsCommand();
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await dataStore.GetItemsAsync();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            SelectedVehicle = null;
            if (Items.Count == 0)
            {
                LoadItemsCommand.Execute(null);
            }
        }
    }
}