using System.Threading.Tasks;
using Xamarin.Essentials;

namespace commutr.Services
{
    public interface IGeolocationService
    {
        Task<Location> GetLocation(GeolocationAccuracy accuracy = GeolocationAccuracy.Medium);
    }
}