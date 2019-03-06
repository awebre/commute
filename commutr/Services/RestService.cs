using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace commutr.Services
{
    public class RestService
    {
        private HttpClient httpClient;
        public RestService()
        {
            httpClient = new HttpClient();
            httpClient.MaxResponseContentBufferSize = 256000;
        }

        public async Task<T> GetAsync<T>(string getUrl) where T : new()
        {
            var uri = new Uri(string.Format(getUrl, string.Empty));

            var response = await httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<T>(content);

                return items;
            }

            return new T();
        }
    }
}