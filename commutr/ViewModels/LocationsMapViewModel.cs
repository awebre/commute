using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using commutr.Services;
using commutr.Models;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace commutr.ViewModels
{
    public class LocationsMapViewModel : BaseViewModel
    {
        private readonly IDataStore<FillUp> fillUpStore;
        private readonly IDataStore<StationLocation> stationStore;
        private readonly IGeolocationService geolocationService;
        private readonly IGeocodingService geocodingService;

        public LocationsMapViewModel(IDataStore<FillUp> fillUpStore,
            IDataStore<StationLocation> stationStore,
            IGeolocationService geolocationService,
            IGeocodingService geocodingService)
        {
            this.fillUpStore = fillUpStore;
            this.stationStore = stationStore;
            this.geolocationService = geolocationService;
            this.geocodingService = geocodingService;
        }
        public bool IsMapReady { get; set; }

        public async Task<Position> GetCurrentPosition()
        {
            var geolocation = await geolocationService.GetLocation();
            return new Position(geolocation.Latitude, geolocation.Longitude);
        }

        public async Task<List<Pin>> GetPins(int vehicleId)
        {

            var fillups = await fillUpStore.Query().Where(x => x.VehicleId == vehicleId).ToListAsync();
            var locationIds = fillups.Select(x => x.LocationId).Distinct();
            var locations = await stationStore.Query().Where(x => locationIds.Contains(x.Id)).ToListAsync();

            var pins = new List<Pin>();
            foreach (var stationLocation in locations)
            {
                var pin = new Pin
                {
                    Address = stationLocation.Address,
                    Label = stationLocation.Name
                };

                var location = await geocodingService.GetLocationFromAddressAsync(stationLocation.Address);
                pin.Position = new Position(location.Latitude, location.Longitude);
                pins.Add(pin);
            }

            return pins;
        }
    }
}