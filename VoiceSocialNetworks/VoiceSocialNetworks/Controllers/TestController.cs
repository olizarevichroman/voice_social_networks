using Microsoft.AspNetCore.Mvc;

namespace VoiceSocialNetworks.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public IActionResult Ping()
        {
            var _ = new
            {
                message = "ping success"
            };

            return Ok(_);
        }
    }
}