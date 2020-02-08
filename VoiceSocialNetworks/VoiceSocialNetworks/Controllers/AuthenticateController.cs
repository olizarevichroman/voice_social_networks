using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VoiceSocialNetworks.Controllers
{
    public class AuthenticateController : Controller
    {
        [Authorize]
        public IActionResult Slack()
        {
            var authProperties = new AuthenticationProperties
            {
                RedirectUri = "/"
            };

            return Challenge(authProperties, "Slack");
        }

        public IActionResult Yandex()
        {
            var authProperties = new AuthenticationProperties
            {
                RedirectUri = "/"
            };

            return Challenge(authProperties, "Yandex");
        }
    }
}