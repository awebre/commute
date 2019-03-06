using System.Collections.Generic;
using System.Threading.Tasks;

namespace commutr.Services
{
    public interface IPlacesService
    {
        Task<List<Place>> GetNearByPlaces();
    }

    public class Place
    {
        public string Name { get; set; }
        
        public string Address { get; set; }
    }
}