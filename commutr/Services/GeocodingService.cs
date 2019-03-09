using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Linq;

namespace commutr.Services
{
    public class GeocodingService : IGeocodingService
    {
        public async Task<Location> GetLocationFromAddressAsync(string address)
        {
            try
            {
                var locations = await Geocoding.GetLocationsAsync(address);
                var location = locations.FirstOrDefault();

                return location;
            }
            catch (Exception e)
            {
                return new Location();
            }
        }
    }
}
