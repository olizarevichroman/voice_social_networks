using Newtonsoft.Json;

namespace VoiceSocialNetworks.ControllerModels
{
    public class Markup
    {
        [JsonProperty("dangerous_context")]
        public bool DangerousContext { get; set; }
    }
}