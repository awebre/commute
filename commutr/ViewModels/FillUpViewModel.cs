using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using commutr.Services;
using commutr.Models;
using commutr.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace commutr.ViewModels
{
    public class FillUpViewModel : BaseViewModel
    {
        private readonly IDataStore<FillUp> dataStore;
        public FillUpViewModel(IDataStore<FillUp> dataStore)
        {
            this.dataStore = dataStore;
            FillUps = new ObservableCollection<FillUp>();
            AddFillUpCommand = new Command(ExecuteAddFillUpCommand);
            LoadFillUpsCommand = new Command(async () => await ExecuteLoadFillUpsCommand());
        }
        
        public Vehicle CurrentVehicle { get; set; }

        public string Title => "Fill Ups";

        public ObservableCollection<FillUp> FillUps { get; set; }
        
        public FillUp FillUp { get; set; }

        public ICommand AddFillUpCommand { get; }
        
        public ICommand LoadFillUpsCommand { get; }

        private void ExecuteAddFillUpCommand()
        {
            Application.Current.MainPage.Navigation.PushAsync(new AddFillUpPage());
        }

        private async Task ExecuteLoadFillUpsCommand()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                FillUps.Clear();
            
                var fillUps = (await dataStore.GetItemsAsync()).ToList();
                fillUps.ForEach(x => FillUps.Add(x));
                fillUps = fillUps.OrderByDescending(x => x.Date).ToList();
                FillUps = new ObservableCollection<FillUp>(fillUps);
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            FillUp = null;
            LoadFillUpsCommand.Execute(null);
        }
    }
}
