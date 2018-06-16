using Newtonsoft.Json;
using System.Collections.Generic;

namespace UnsplashApp.Models
{
    public class PhotoQueryResult
    {
        [JsonProperty("results")]
        public IEnumerable<Photo> Results { get; set; }
    }
}
