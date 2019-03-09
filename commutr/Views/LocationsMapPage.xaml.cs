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
        public LocationsMapPage(int vehicleId)
        {
            viewModel = App.Resolver.Resolve<LocationsMapViewModel>();
            viewModel.Title = "Locations";
            InitializeComponent();

            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.IsMapReady = false;
            MoveToCurrentLocation();
            viewModel.IsMapReady = true;

        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            MoveToCurrentLocation();
        }

        private async void MoveToCurrentLocation()
        {
            var position = await viewModel.GetCurrentPosition();
            LocationMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(25)));
        }
    }
}
