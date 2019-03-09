using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using commutr.Models;
using GeoLocation = Xamarin.Essentials.Location;
using StationLocation = commutr.Models.StationLocation;
using Xamarin.Forms.Internals;
using System.Net.Http;
using System.Linq;

namespace commutr.Services
{
    public class GooglePlacesService : IPlacesService
    {
        private string apiKey;
        private RestService restService;

        public GooglePlacesService(string apiKey)
        {
            this.apiKey = apiKey;
            restService = new RestService();
        }

        public async Task<List<StationLocation>> GetNearByPlaces()
        {
            GeoLocation geolocation;
            try
            {
                geolocation = await Geolocation.GetLocationAsync(new GeolocationRequest());
            }
            catch (Exception e)
            {
                geolocation = null;
            }

            var url = FindPlaceUrl;
            if (geolocation != null)
            {
                url += $"&locationbias=circle:2000@{geolocation.Latitude},{geolocation.Longitude}";
            }

            var result = await restService.GetAsync<GooglePlaceSearchResult>(url);

            var places = new List<StationLocation>();
            result.candidates.ForEach(x => places.Add(new StationLocation
            {
                PlaceId = x.place_id,
                Address = x.formatted_address,
                Name = x.name
            }));

            return places;
        }

        public async Task<List<StationLocation>> RefreshPlaceIds(IEnumerable<StationLocation> locations)
        {
            List<Task<StationLocation>> locationRefreshTask = new List<Task<StationLocation>>();
            foreach (var location in locations)
            {
                locationRefreshTask.Add(RefreshPlaceId(location));
            }

            var refreshedLocations = await Task.WhenAll(locationRefreshTask);
            return locations.ToList();
        }

        public async Task<StationLocation> RefreshPlaceId(StationLocation location)
        {
            var result = await restService.GetAsync<GooglePlaceDetailResult>(PlaceDetailsUrl + location.PlaceId);
            location.PlaceId = result.result.place_id;

            return location;
        }

        private class GooglePlaceSearchResult
        {
            public List<GooglePlace> candidates { get; set; }

            public string error_message { get; set; }
        }

        private class GooglePlace
        {
            public string place_id { get; set; }

            public string formatted_address { get; set; }

            public string name { get; set; }
        }

        private class GooglePlaceDetailResult
        {
            public GooglePlace result { get; set; }
        }

        private string PlaceApiBaseUrl => "https://maps.googleapis.com/maps/api/place";

        private string FindPlaceUrl =>
            $"{PlaceApiBaseUrl}/findplacefromtext/json?input=gas&inputtype=textquery&fields=formatted_address,name,place_id&key={apiKey}";

        private string PlaceDetailsUrl => $"{PlaceApiBaseUrl}/details/json?key={apiKey}&fields=place_id&place_id=";
    }
}