using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using commutr.Models;
using GeoLocation = Xamarin.Essentials.Location;
using Location = commutr.Models.Location;

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

        public async Task<List<Location>> GetNearByPlaces()
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

            var result = await restService.GetAsync<GooglePlaceResult>(url);

            var places = new List<Location>();
            result.candidates.ForEach(x => places.Add(new Location
            {
                PlaceId = x.place_id,
                Address = x.formatted_address,
                Name = x.name
            }));

            return places;
        }

        private class GooglePlaceResult
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

        private string FindPlaceUrl =>
            $"https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input=gas&inputtype=textquery&fields=formatted_address,name,place_id&key={apiKey}";
    }
}