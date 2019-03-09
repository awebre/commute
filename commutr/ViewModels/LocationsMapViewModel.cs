using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using commutr.Services;

namespace commutr.ViewModels
{
    public class LocationsMapViewModel : BaseViewModel
    {
        private readonly IGeolocationService geolocationService;

        public LocationsMapViewModel(IGeolocationService geolocationService)
        {
            this.geolocationService = geolocationService;
        }
        public bool IsMapReady { get; set; }

        public async Task<Position> GetCurrentPosition()
        {
            var geolocation = await geolocationService.GetLocation();
            return new Position(geolocation.Latitude, geolocation.Longitude);
        }
    }
}