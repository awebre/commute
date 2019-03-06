using commutr.Services;
using Xamarin.Essentials;

namespace commutr.iOS.Services
{
    public class GeolocationService : IGeolocationService
    {
        public Location GetLocation(GeolocationAccuracy accuracy = GeolocationAccuracy.Medium)
        {
            throw new System.NotImplementedException();
        }
    }
}