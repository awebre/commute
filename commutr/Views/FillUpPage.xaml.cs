using System;
using System.Collections.Generic;
using commutr.ViewModels;
using Xamarin.Forms;

namespace commutr.Views
{
    public partial class FillUpPage : ContentPage
    {
        private readonly FillUpViewModel viewModel;
        public FillUpPage()
        {
            viewModel = App.Resolver.Resolve<FillUpViewModel>();
            
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}
