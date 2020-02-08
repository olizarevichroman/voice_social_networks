using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VoiceSocialNetworks.ControllerModels;

namespace VoiceSocialNetworks.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AliceController : ControllerBase
    {
        public AliceController()
        {

        }
        [HttpPost]
        public async Task<IActionResult> Index(RequestWrapper request)
        {
            return await Task.FromResult(Ok());
        }
    }
}