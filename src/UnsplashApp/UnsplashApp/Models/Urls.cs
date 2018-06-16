using Newtonsoft.Json;

namespace UnsplashApp.Models
{
    public class Urls
    {

        public string Regular { get; set; }

 

        [JsonProperty("thumb")]
        public string Thumb { get; set; }

    }
}
