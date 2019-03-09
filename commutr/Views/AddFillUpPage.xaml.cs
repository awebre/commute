using System;
using System.Collections.Generic;
using commutr.Models;
using commutr.ViewModels;
using Xamarin.Forms;

namespace commutr.Views
{
    public partial class AddFillUpPage : ContentPage
    {
        public AddFillUpPage(int vehicleId, FillUp fillUp = null)
        {
            InitializeComponent();

            AddFillUpViewModel vm;
            BindingContext = vm = App.Resolver.Resolve<AddFillUpViewModel>();
            if (fillUp != null)
            {
                vm.FillUp = fillUp;
            }
            else
            {
                vm.FillUp = new FillUp
                {
                    VehicleId = vehicleId,
                    Date = DateTime.Now
                };
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = (AddFillUpViewModel)BindingContext;
            if (viewModel.FillUp == null)
            {
                await viewModel.GetNearbyPlaces();
            }
            else
            {
                await viewModel.GetExistingPlaces();
            }
        }
    }
}
