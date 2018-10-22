using System;

using Xamarin.Forms;

namespace commutr.Views
{
    public class ReportsPage : ContentPage
    {
        public ReportsPage()
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

