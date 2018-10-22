using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commutr.ViewModels;
using Xamarin.Forms;

namespace commutr.Views
{
    public partial class MyVehiclePage : ContentPage
    {
        private readonly MyVehicleViewModel viewModel;
        public MyVehiclePage(int vehicleId)
        {
            viewModel = App.Resolver.Resolve<MyVehicleViewModel>();
            viewModel.VehicleId = vehicleId;
            viewModel.Title = "My Vehicle";
            InitializeComponent();

            BindingContext = viewModel;
        }

        public MyVehiclePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.GetVehicle();
        }
    }
}
