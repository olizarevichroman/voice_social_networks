using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VoiceSocialNetworks.DataLayer.Abstractions
{
    public interface IUserCreator
    {
        Task SyncYandexUser(ClaimsIdentity ticketContext);

        Task SyncVkUser(ClaimsPrincipal principal, OAuthTokenResponse tokenResponse);
    }
}
