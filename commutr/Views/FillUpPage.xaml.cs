using System;
using System.Collections.Generic;
using commutr.ViewModels;
using Xamarin.Forms;

namespace commutr.Views
{
    public partial class FillUpPage : ContentPage
    {
        private readonly FillUpViewModel viewModel;
        public FillUpPage(int vehicleId)
        {
            viewModel = App.Resolver.Resolve<FillUpViewModel>();
            viewModel.SelectedVehicleId = vehicleId;
            
            InitializeComponent();

            BindingContext = viewModel;
        }

        public FillUpPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}
