using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VoiceSocialNetworks.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AliceController : ControllerBase
    {
        public AliceController()
        {

        }

        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(Ok());
        }
    }
}