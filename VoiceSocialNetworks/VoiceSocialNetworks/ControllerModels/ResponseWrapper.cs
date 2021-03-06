﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceSocialNetworks.ControllerModels
{
    public class ResponseWrapper
    {
        [JsonProperty("start_account_linking")]
        public object StartAccountLinking { get; set; }


        [JsonProperty("response")]
        public Response Response { get; set; }

        [JsonProperty("session")]
        public Session Session { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
