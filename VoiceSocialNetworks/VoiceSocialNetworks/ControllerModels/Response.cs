using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceSocialNetworks.ControllerModels
{
    public class Response
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("tts")]
        public string TTS { get; set; }

        [JsonProperty("end_session")]
        public bool EndSession { get; set; }
    }
}
