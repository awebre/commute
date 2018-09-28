using System;
using commutr.Services;
using commutr.Models;
namespace commutr.ViewModels
{
    public class FillUpViewModel : BaseViewModel
    {
        private readonly IDataStore<FillUp> dataStore;
        public FillUpViewModel(IDataStore<FillUp> dataStore)
        {
            this.dataStore = dataStore;
        }
        
        public Vehicle CurrentVehicle { get; set; }
    }
}
