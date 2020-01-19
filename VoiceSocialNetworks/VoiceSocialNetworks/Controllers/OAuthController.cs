using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace VoiceSocialNetworks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private const string client_id = "fhVkdhNldfN45LdfL";
        private const string client_secret = "FvdgFdsfFFFsf4gGdasGb";
        private const string access_token = "d33ffdfsf03403mFkdsflFk";

        public IActionResult Authorize()
        {
            throw new NotImplementedException();
        }

        public IActionResult AccessToken()
        {
            throw new NotImplementedException();
        }
    }

    public class GetAccessTokenRequest
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty]
        public string ClientSecret { get; set; }
    }
}