using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace commutr.Services
{
    public interface IGeocodingService
    {
        Task<Location> GetLocationFromAddressAsync(string address);
    }
}
