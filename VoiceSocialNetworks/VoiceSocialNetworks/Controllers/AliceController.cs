using System.Text.Json;
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
            var jsonSerializerSettings = new JsonSerializerOptions
            {
                IgnoreNullValues = true
            };
            var resultObject = JsonSerializer.Serialize(result, jsonSerializerSettings);
            var jsonResult = new JsonResult(resultObject);

            return await Task.FromResult(jsonResult);
        }
    }
}