using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VoiceSocialNetworks.ControllerModels;
using VoiceSocialNetworks.DataLayer.Abstractions;
using VoiceSocialNetworks.Flow.Actions;
using VoiceSocialNetworks.Flow.Layers;
using VoiceSocialNetworks.SDK.Clients;

namespace VoiceSocialNetworks.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AliceController : ControllerBase
    {
        private readonly IUserCreator _userCreator;
        private readonly IUnitOfWork _unitOfWork;
        public AliceController(IUserCreator userCreator, IUnitOfWork unitOfWork)
        {
            Console.WriteLine($"{nameof(AliceController)} constructor");
            _userCreator = userCreator;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<IActionResult> Index(RequestWrapper request)
        {
            Console.WriteLine($"Current user authenticated : {HttpContext.User.Identity.IsAuthenticated}");
            var result = new ResponseWrapper
            {
                Session = request.Session,
                Version = request.Version
            };

            Console.WriteLine($"{nameof(AliceController)} handle request with command: {request?.Request?.Command}");
            var publicLayer = new PublicActionsLayer();
            foreach(var action in publicLayer.Actions)
            {
                if (action.CanHandle(request.Request))
                {
                    result.Response = await action.Handle(request.Request);

                    return Ok(result);
                }
            }
            var user = HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                result.StartAccountLinking = new object();

                return await Task.FromResult(Ok(result));
            }
            var userId = user.FindFirst("id").Value;
            var yandexUser = await _unitOfWork.UserRepository.GetEntity(userId);
            if (yandexUser == null)
            {
                await _userCreator.SyncYandexUser(user.Identity as ClaimsIdentity);
            }

            var vkUser = _unitOfWork.VkUserRepository.Find(user => user.YandexUserId == yandexUser.Id).FirstOrDefault();
            var vkClient = new VkClient();
            var getStatusAction = new GetVkStatusAction(vkClient, vkUser);
            if (getStatusAction.CanHandle(request.Request))
            {
                result.Response = await getStatusAction.Handle(request.Request);

                return Ok(result);
            }

            result.Response = new Response
            {
                Text = $"Ты авторизован, поздравляю!"
            };

            return Ok(result);
        }
    }
}