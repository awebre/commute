using System;
using System.Collections.Generic;
using commutr.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace commutr.Views
{
    public partial class MaintenanceLogPage : ContentPage
    {
        private readonly MaintenanceLogViewModel viewModel;
        
        public MaintenanceLogPage(int vehicleId)
        {
            InitializeComponent();

            BindingContext = viewModel = App.Resolver.Resolve<MaintenanceLogViewModel>();
            viewModel.SelectedVehicleId = vehicleId;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}
