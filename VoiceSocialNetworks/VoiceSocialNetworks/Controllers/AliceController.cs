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
            var result = new ResponseWrapper
            {
                Session = request.Session,
                Version = request.Version
            };

            if (request.AccountLinkingCompleted == null || request.Session.New)
            {
                result.StartAccountLinking = new object();

                return await Task.FromResult(Ok(result));
            }

            result.Response = new Response
            {
                Text = "Ты авторизован, поздравляю!"
            };

            return await Task.FromResult(Ok(result));
        }
    }
}