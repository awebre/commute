using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace commutr.Services
{
    public class GeolocationService : IGeolocationService
    {
        public async Task<Location> GetLocation(GeolocationAccuracy accuracy = GeolocationAccuracy.Medium)
        {
            try
            {
                var request = new GeolocationRequest(accuracy);
                return await Geolocation.GetLocationAsync(request);
            }
            catch (Exception e)
            {
                return new Location();
            }
        }
    }
}