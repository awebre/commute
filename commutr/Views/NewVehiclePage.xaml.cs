using System;
using System.Collections.Generic;
using System.Linq;
using commutr.Models;
using commutr.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace commutr.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewVehiclePage : ContentPage
    {        
        private readonly VehicleCrudViewModel viewModel;
        public NewVehiclePage(Vehicle vehicle = null)
        {
            viewModel = App.Resolver.Resolve<VehicleCrudViewModel>();
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

            viewModel.Item = vehicle;

            BindingContext = viewModel;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (viewModel.Item.Id == 0)
            {
                MessagingCenter.Send(this, "AddVehicle", viewModel.Item);
            }
            else
            {
                await viewModel.Save();
            }
        }
    }
}