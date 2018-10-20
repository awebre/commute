using System;
using commutr.Models;
using commutr.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace commutr.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehiclePage : ContentPage
    {
        VehicleViewModel viewModel;

        public VehiclePage()
        {
            InitializeComponent();

            BindingContext = viewModel = App.Resolver.Resolve<VehicleViewModel>();
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewVehiclePage());
        }

        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }

        private void OnPainSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            var info = args.Info;
            var surface = args.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();
            
            var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Blue,
                StrokeWidth = 5
            };
            
            canvas.DrawCircle(10, info.Height / 2, 10, paint);
        }
    }
}