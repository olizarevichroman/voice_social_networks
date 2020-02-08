using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using VoiceSocialNetworks.DataLayer.Abstractions;

namespace VoiceSocialNetworks.AuthenticationHandlers
{
    public class YandexAuthenticationHandler : OAuthHandler<OAuthOptions>
    {
        //private readonly IUserCreator _userCreator;
        public YandexAuthenticationHandler(
            IOptionsMonitor<OAuthOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            //IUserCreator userCreator)
            : base(options, logger, encoder, clock)
        {
            //_userCreator = userCreator;
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(
            ClaimsIdentity identity,
            AuthenticationProperties properties,
            OAuthTokenResponse tokens)
        {
            //Options.Events.OnCreatingTicket += _userCreator.SyncYandexUser;
            using var request = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", tokens.AccessToken);

            var response = await Backchannel.SendAsync(request, Context.RequestAborted).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"An error occurred when retrieving Google user information ({response.StatusCode}). Please check if the authentication information is correct.");
            }
            
            using var payload = JsonDocument.Parse(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            var context = new OAuthCreatingTicketContext(new ClaimsPrincipal(identity), properties, Context, Scheme, Options, Backchannel, tokens, payload.RootElement);
            context.RunClaimActions();
            await Events.CreatingTicket(context).ConfigureAwait(false);

            return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
        }
    }
}