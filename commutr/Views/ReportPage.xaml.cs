using System;
using System.Collections.Generic;
using commutr.ViewModels;
using Microcharts;
using Microcharts.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Entry = Microcharts.Entry;

namespace commutr.Views
{
    public partial class ReportPage : ContentPage
    {
        private readonly ReportViewModel viewModel;
        public ReportPage(int vehicleId = 0)
        {
            InitializeComponent();

            BindingContext = viewModel = App.Resolver.Resolve<ReportViewModel>();
            viewModel.SelectedVehicleId = vehicleId;
            viewModel.Title = "Reports";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var fuelChartEntries = viewModel.GetFuelChartEntries();
            var fuelChart = new LineChart()
            {
                LabelTextSize = 35.0f,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Horizontal,
                Entries = fuelChartEntries
            };
            
            FuelChart.Chart = fuelChart;


            var pricePerEntries = viewModel.GetPricePerEntries();
            var pricePerChart = new PointChart()
            {
                LabelTextSize = 35.0f,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Horizontal,
                Entries = pricePerEntries
            };

            PricePerGallonChart.Chart = pricePerChart;
        }
    }
}
