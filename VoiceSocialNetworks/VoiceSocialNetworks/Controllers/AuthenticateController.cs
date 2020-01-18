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
                RedirectUri = "https://google.com"
            };

            return Challenge(authProperties, "Vk");
        }
    }
}