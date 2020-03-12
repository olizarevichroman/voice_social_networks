using Newtonsoft.Json;

namespace VoiceSocialNetworks.ControllerModels
{
    public class RequestWrapper
    {
        [JsonProperty("account_linking_complete_event")]
        public object AccountLinkingCompleted { get; set; }

        [JsonProperty("request")]
        public Request Request { get; set; }

        [JsonProperty("session")]
        public Session Session { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
