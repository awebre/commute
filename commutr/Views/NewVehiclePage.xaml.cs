using System;
using System.Collections.Generic;
using System.Linq;
using commutr.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace commutr.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewVehiclePage : ContentPage
    {
        public Vehicle Item { get; set; }
        
        public List<int> Years => GetYears();

        public NewVehiclePage(Vehicle vehicle = null)
        {
            InitializeComponent();

            if (vehicle == null)
            {
                Title = "New Vehicle";
                vehicle = new Vehicle();
            }
            else
            {
                Title = "Edit Vehicle";
            }

            Item = vehicle;
           

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddVehicle", Item);
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