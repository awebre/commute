using commutr.Models;
using commutr.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace commutr.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehicleDetailsPage : TabbedPage
    {
        VehicleDetailsViewModel viewModel;

        public VehicleDetailsPage(VehicleDetailsViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public VehicleDetailsPage()
        {
            InitializeComponent();

            var item = new Vehicle
            {
                Make = "Item 1",
                Model = "This is an item description."
            };

            viewModel = new VehicleDetailsViewModel(item);
            BindingContext = viewModel;
        }
    }
}