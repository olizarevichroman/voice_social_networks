using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceSocialNetworks.ControllerModels
{
    public class Session
    {
        [JsonProperty("new")]
        public bool New { get; set; }

        [JsonProperty("message_id")]
        public int MessageId { get; set; }

        [JsonProperty("session_id")]
        public string SessionId { get; set; }

        [JsonProperty("skill_id")]
        public string SkillId { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}
