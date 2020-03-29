using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using VoiceSocialNetworks.ControllerModels;
using VoiceSocialNetworks.DataLayer.Abstractions;

namespace VoiceSocialNetworks.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class AliceController : ControllerBase
    {
        private readonly IUserCreator _userCreator;
        private readonly IUnitOfWork _unitOfWork;
        public AliceController(IUserCreator userCreator, IUnitOfWork unitOfWork)
        {
            _userCreator = userCreator;
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(RequestWrapper request)
        {
            Console.WriteLine($"Current user authenticated : {HttpContext.User.Identity.IsAuthenticated}");
            var result = new ResponseWrapper
            {
                Session = request.Session,
                Version = request.Version
            };
            result.Response = new Response
            {
                Text = $"Привет!"
            };

            return await Task.FromResult(Ok(result));

            //var user = HttpContext.User;
            //if (!user.Identity.IsAuthenticated)
            //{
            //    result.StartAccountLinking = new object();

            //    return await Task.FromResult(Ok(result));
            //}
            //var userId = user.FindFirst("id").Value;
            //var yandexUser = await _unitOfWork.UserRepository.GetEntity(userId);
            //if (yandexUser == null)
            //{
            //    await _userCreator.SyncYandexUser(user.Identity as ClaimsIdentity);
            //}

            //result.Response = new Response
            //{
            //    Text = $"Ты авторизован, {yandexUser.DisplayName} поздравляю!"
            //};

            //return await Task.FromResult(Ok(result));
        }
    }
}