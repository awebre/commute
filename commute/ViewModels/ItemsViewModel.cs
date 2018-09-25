using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using commutr.Views;
using commutr.Models;
using commutr.Services;
using Xamarin.Forms;

namespace commutr.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Vehicle> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        private readonly IDataStore<Vehicle> dataStore;

        public ItemsViewModel(IDataStore<Vehicle> dataStore)
        {
            this.dataStore = dataStore;
            
            Title = "Browse";
            Items = new ObservableCollection<Vehicle>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Vehicle>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Vehicle;
                Items.Add(newItem);
                await dataStore.AddItemAsync(newItem);
            });
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
    }
}