using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace VoiceSocialNetworks.Controllers
{
    public class AuthenticateController : Controller
    {
        public IActionResult Vk()
        {
            var authProperties = new AuthenticationProperties
            {
                RedirectUri = "/"
            };

            return Challenge(authProperties, "Vk");
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