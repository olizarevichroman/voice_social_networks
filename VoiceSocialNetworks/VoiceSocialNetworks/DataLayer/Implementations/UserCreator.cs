using Microsoft.AspNetCore.Authentication.OAuth;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using VoiceSocialNetworks.DataLayer.Abstractions;
using VoiceSocialNetworks.DataLayer.Models;

namespace VoiceSocialNetworks.DataLayer.Implementations
{
    public class UserCreator : IUserCreator
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserCreator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task SyncYandexUser(OAuthCreatingTicketContext ticketContext)
        {
            var userRepository = _unitOfWork.UserRepository;
            var identity = ticketContext.Identity;
            var id  = identity.Claims.First(c => c.Type == "Id").Value;
            var user = await userRepository.GetEntity(id).ConfigureAwait(false);

            if (user == null)
            {
                var newUser = new User
                {
                    Id = id,
                    DisplayName = identity.Name
                };

                userRepository.Add(newUser);
                //here we need to create new user with associated claims
            }
            else
            {
                //here we need to make a request to update existing user (displayName and other data)
            }

            await _unitOfWork.CommitChanges().ConfigureAwait(false);
        }

        public async Task SyncVkUser(ClaimsPrincipal principal, OAuthTokenResponse tokenResponse)
        {
            var userRepository = _unitOfWork.UserRepository;
            var vkUserRepository = _unitOfWork.VkUserRepository;
            var yandexUserId = principal.Claims.First(c => c.Type == "Id").Value;
            var payload = tokenResponse.Response.RootElement;
            var vkId = payload.GetProperty("user_id").ToString();
            var vkUser = new VkUser
            {
                YandexUserId = yandexUserId,
                Id = vkId,
                AccessToken = tokenResponse.AccessToken
            };

            vkUserRepository.Add(vkUser);

            await _unitOfWork.CommitChanges().ConfigureAwait(false);
        }
    }
}
