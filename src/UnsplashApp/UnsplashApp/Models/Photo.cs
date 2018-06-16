using Newtonsoft.Json;

namespace UnsplashApp.Models
{
    public class Photo
    {
        
        public string Id { get; set; }

        
        public Urls Urls { get; set; }


        
        public User User { get; set; }

        
        public string Description { get; set; }

        
        public string PhotoUrl
        {
            get
            {
                return Urls.Thumb;
            }
        }

        [JsonIgnore]
        public string PhotoRegularUrl
        {
            get
            {
                return Urls.Regular;
            }
        }

        [JsonIgnore]
        public string UserName
        {
            get
            {
                return User.UserName;
            }

        }
    }
}
