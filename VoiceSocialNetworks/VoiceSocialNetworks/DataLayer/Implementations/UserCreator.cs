using Microsoft.AspNetCore.Authentication.OAuth;
using Newtonsoft.Json;
using System.Collections.Generic;
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
        public async Task SyncYandexUser(ClaimsIdentity identity)
        {
            var userRepository = _unitOfWork.UserRepository;
            var claimsKeyToValue = identity.Claims.Select(c => new KeyValuePair<string, string>(c.Type, c.Value));
            var claimsDictionary = new Dictionary<string, string>(claimsKeyToValue);
            
            var claimsJson = JsonConvert.SerializeObject(claimsDictionary);
            var yandexUser = JsonConvert.DeserializeObject<User>(claimsJson);
            var user = await userRepository.GetEntity(yandexUser.Id).ConfigureAwait(false);

            if (user == null)
            {
                userRepository.Add(yandexUser);
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
