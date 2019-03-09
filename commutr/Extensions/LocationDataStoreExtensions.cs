using System;
using System.Threading.Tasks;
using commutr.Models;
using commutr.Services;

namespace commutr.Extensions
{
    public static class LocationDataStoreExtensions
    {
        public static async Task RefreshLocations(this IDataStore<Location> locationStore)
        {
            var locations = await locationStore.GetItemsAsync();
            foreach (var location in locations)
            {
                /* 
                 * TODO: refresh place ID via Google Place API
                 * see how at 
                 * https://developers.google.com/places/web-service/details#PlaceDetailsRequests
                 */
            }
        }
    }
}


