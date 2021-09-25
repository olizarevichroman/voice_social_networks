using System;
using System.Collections.Generic;
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
            var actions = new List<IAction>
                { new SetVkStatusAction(vkClient, vkUser), new GetVkStatusAction(vkClient, vkUser) };
            var vkAction = actions.FirstOrDefault(x => x.CanHandle(request.Request));
            
            if (vkAction != null)
            {
                result.Response = await vkAction.Handle(request.Request);

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