using System;
using System.Collections.Generic;
using System.Windows.Input;
using commutr.Models;
using commutr.Services;
using PropertyChanged;
using Xamarin.Forms;

namespace commutr.ViewModels
{
    public class AddFillUpViewModel : BaseViewModel
    {
        private readonly IDataStore<FillUp> dataStore;
        private FillUp fillUp;

        public AddFillUpViewModel(IDataStore<FillUp> dataStore)
        {
            this.dataStore = dataStore;

            SaveFillUpCommand = new Command(ExecuteSaveFillUpCommand);
            if (fillUp == null)
            {
                fillUp = new FillUp();
            }
            fillUp.Date = DateTime.Today;
        }

        public string Title => "Fill Up";

        public FillUp FillUp
        {
            get => fillUp;
            set
            {
                fillUp = value;
            }
            
        }

        public ICommand SaveFillUpCommand { get; }

        private void ExecuteSaveFillUpCommand()
        {
            if (fillUp.Id == 0)
            {
                dataStore.AddItemAsync(fillUp);
            }
            else
            {
                dataStore.UpdateItemAsync(fillUp);
            }

            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}