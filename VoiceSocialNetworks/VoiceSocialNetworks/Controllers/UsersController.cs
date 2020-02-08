//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using VoiceSocialNetworks.DataLayer.Implementations;
//using VoiceSocialNetworks.DataLayer.Models;

//namespace VoiceSocialNetworks.Controllers
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class UsersController : ControllerBase
//    {
//        private readonly UserRepository _repository;
//        public UsersController(UserRepository repository)
//        {
//            _repository = repository;
//        }
//        [HttpGet]
//        public IActionResult GetUser()
//        {
//            var user = HttpContext.User;
//            var identity = user.Identity;

//            var _ = new
//            {
//                identity.Name,
//                identity.IsAuthenticated
//            };

//            return new JsonResult(_);
//        }
//    }
//}