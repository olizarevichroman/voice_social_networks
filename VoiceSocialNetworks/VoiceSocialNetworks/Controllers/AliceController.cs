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
        //[HttpPost]
        public async Task<IActionResult> Index(RequestWrapper request)
        {
            var result = new ResponseWrapper();

            if (request.AccountLinkingCompleted == null)
            {
                result.StartAccountLinking = new object();
            }

            result.Session = request.Session;
            result.Version = request.Version;

            return await Task.FromResult(Ok(result));
        }
    }
}