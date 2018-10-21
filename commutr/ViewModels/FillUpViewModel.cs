using System;
using System.Windows.Input;
using commutr.Services;
using commutr.Models;
using commutr.Views;
using Xamarin.Forms;

namespace commutr.ViewModels
{
    public class FillUpViewModel : BaseViewModel
    {
        private readonly IDataStore<FillUp> dataStore;
        public FillUpViewModel(IDataStore<FillUp> dataStore)
        {
            this.dataStore = dataStore;
            AddFillUpCommand = new Command(ExecuteAddFillUpCommand);
        }
        
        public Vehicle CurrentVehicle { get; set; }

        public string Title => "Fill Ups";

        public ICommand AddFillUpCommand { get; }

        private void ExecuteAddFillUpCommand()
        {
            Application.Current.MainPage.Navigation.PushAsync(new AddFillUpPage());
        }
    }
}
