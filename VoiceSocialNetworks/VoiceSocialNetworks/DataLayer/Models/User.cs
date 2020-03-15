using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace VoiceSocialNetworks.DataLayer.Models
{
    public class User
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("default_email")]
        public string Email { get; set; }

        [JsonProperty("real_name")]
        public string RealName { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }

        [Key]
        public string Id { get; set; }
    }
}
