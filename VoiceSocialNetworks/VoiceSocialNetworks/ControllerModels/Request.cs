using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceSocialNetworks.ControllerModels
{
    public class Request
    {
        [JsonProperty("command")]
        public string Command { get; set; }

        [JsonProperty("original_utterance")]
        public string OriginalUtterance { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("markup")]
        public Markup Markup { get; set; }

        [JsonProperty("payload")]
        public Payload Payload { get; set; }

        [JsonProperty("nlu")]
        public NLU NLU { get; set; }
    }
}
