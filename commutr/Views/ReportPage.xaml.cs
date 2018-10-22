using System;
using System.Collections.Generic;
using commutr.ViewModels;
using Xamarin.Forms;

namespace commutr.Views
{
    public partial class ReportPage : ContentPage
    {
        private readonly ReportViewModel viewModel;
        public ReportPage()
        {
            InitializeComponent();

            BindingContext = viewModel = App.Resolver.Resolve<ReportViewModel>();
            viewModel.Title = "Reports";
        }
    }
}
