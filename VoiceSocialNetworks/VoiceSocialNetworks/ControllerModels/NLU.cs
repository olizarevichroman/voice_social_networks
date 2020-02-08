using Newtonsoft.Json;

namespace VoiceSocialNetworks.ControllerModels
{
    public class NLU
    {
        [JsonProperty("tokens")]
        public string[] Tokens { get; set; }


    }
}