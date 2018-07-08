using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UnsplashApp.Models;

namespace UnsplashApp.Services
{
    public class UnsplashService
    {
        public const int MinSearchLength = 5;

        private const string ClientIdParameter = "client_id=";

        private const string UrlApi = "https://api.unsplash.com";

        private HttpClient _client = new HttpClient();

        public async Task<IEnumerable<Photo>> GetPhotos(string query)
        {
            if (query.Length < MinSearchLength)
                return Enumerable.Empty<Photo>();

            var queryUrl = $"{UrlApi}/search/photos?query={query}&{ClientIdParameter}";

            var response = await _client.GetAsync(queryUrl);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return Enumerable.Empty<Photo>();

            var content = await response.Content.ReadAsStringAsync();

            var photoQueryResult = JsonConvert.DeserializeObject<PhotoQueryResult>(content);

            return photoQueryResult.Results;
        }

        public async Task<Photo> GetPhotoById(string id)
        {
            var response = await _client.GetAsync($"{UrlApi}/photos/{id}?{ClientIdParameter}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Photo>(content);
        }
    }
}