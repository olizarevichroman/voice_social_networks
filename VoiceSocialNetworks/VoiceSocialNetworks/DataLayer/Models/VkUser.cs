using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoiceSocialNetworks.DataLayer.Models
{
    public class VkUser
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey(nameof(YandexUserId))]
        public User YandexUser { get; set; }
        public string YandexUserId { get; set; }
        public string AccessToken { get; set; }
    }
}
