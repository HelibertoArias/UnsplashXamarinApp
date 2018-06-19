using Newtonsoft.Json;
using System.Collections.Generic;

namespace UnsplashApp.Models
{
    public class PhotoQueryResult
    {
       
        public IEnumerable<Photo> Results { get; set; }
    }
}
