using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commutr.Models;
using commutr.ViewModels;
using Xamarin.Forms;

namespace commutr.Views
{
    public partial class AddFillUpPage : ContentPage
    {
        private readonly bool isNewFillUp = false;
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
                isNewFillUp = true;
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
            if (isNewFillUp)
            {
                await Task.Run(() => viewModel.GetNearbyPlaces());
            }
            else
            {
                await Task.Run(() => viewModel.GetExistingPlaces());
            }
        }
    }
}
