using System;
using commutr.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace commutr.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewVehiclePage : ContentPage
    {
        public Vehicle Item { get; set; }

        public NewVehiclePage()
        {
            InitializeComponent();

            Item = new Vehicle
            {
                Make = "Item name",
                Model = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddVehicle", Item);
            await Navigation.PopModalAsync();
        }
    }
}