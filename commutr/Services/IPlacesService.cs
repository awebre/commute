using System.Collections.Generic;
using System.Threading.Tasks;
using commutr.Models;

namespace commutr.Services
{
    public interface IPlacesService
    {
        Task<List<StationLocation>> GetNearByPlaces();

        Task<StationLocation> RefreshPlaceId(StationLocation location);
    }
}