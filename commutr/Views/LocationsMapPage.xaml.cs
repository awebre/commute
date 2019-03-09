using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commutr.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace commutr.Views
{
    public partial class LocationsMapPage : ContentPage
    {
        private readonly LocationsMapViewModel viewModel;
        private readonly int vehicleId;
        public LocationsMapPage(int vehicleId)
        {
            this.vehicleId = vehicleId;
            viewModel = App.Resolver.Resolve<LocationsMapViewModel>();
            viewModel.Title = "Locations";
            InitializeComponent();

            BindingContext = viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.IsMapReady = false;
            MoveToCurrentLocation();

            LocationMap.Pins.Clear();
            var pins = await viewModel.GetPins(vehicleId);
            foreach (var pin in pins)
            {
                LocationMap.Pins.Add(pin);
            }
            viewModel.IsMapReady = true;

        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            MoveToCurrentLocation();
        }

        private async void MoveToCurrentLocation()
        {
            var position = await viewModel.GetCurrentPosition();
            LocationMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1)));
        }
    }
}
