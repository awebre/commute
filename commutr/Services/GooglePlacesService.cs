using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

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

        public async Task<List<Place>> GetNearByPlaces()
        {
            Location location;
            try
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest());
            }
            catch (Exception e)
            {
                location = null;
            }

            var url = FindPlaceUrl;
            if (location != null)
            {
                url += $"&locationbias=circle:2000@{location.Latitude},{location.Longitude}";
            }

            var result = await restService.GetAsync<GooglePlaceResult>(url);

            var places = new List<Place>();
            result.candidates.ForEach(x => places.Add(new Place
            {
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
            public string formatted_address { get; set; }

            public string name { get; set; }
        }

        private string FindPlaceUrl =>
            $"https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input=gas&inputtype=textquery&fields=formatted_address,name&key={apiKey}";
    }
}