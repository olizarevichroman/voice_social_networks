using System.ComponentModel.DataAnnotations;

namespace VoiceSocialNetworks.DataLayer.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string DisplayName { get; set; }
    }
}
