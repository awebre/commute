using System;

using Xamarin.Forms;

namespace commutr.Views
{
    public class LocationsMapPage : ContentPage
    {
        public LocationsMapPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

