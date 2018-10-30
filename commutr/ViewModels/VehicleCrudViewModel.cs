using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commutr.Models;
using commutr.Services;
using Xamarin.Forms;

namespace commutr.ViewModels
{
    public class VehicleCrudViewModel : BaseViewModel
    {
        private readonly IDataStore<Vehicle> dataStore;
        public VehicleCrudViewModel(IDataStore<Vehicle> dataStore)
        {
            this.dataStore = dataStore;
            Years = GetYears();
        }
        
        public List<int> Years { get; }
        
        public Vehicle Item { get; set; }

        public async Task Save()
        {
            await dataStore.UpdateItemAsync(Item);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private static List<int> GetYears()
        {
            var years = new List<int>();
            var nextYear = DateTime.Now.Year + 1;
            var year = 1900;
            while (year <= nextYear)
            {
                years.Add(year);
                year++;
            }

            return years.OrderByDescending(x => x).ToList();
        }
    }
}