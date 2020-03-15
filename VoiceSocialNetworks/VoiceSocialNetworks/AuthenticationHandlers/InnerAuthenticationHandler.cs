using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using VoiceSocialNetworks.SDK.Clients;

namespace VoiceSocialNetworks.AuthenticationHandlers
{
    public class InnerAuthenticationHandler : SignInAuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string BEARER_PREFIX = "Bearer";
        private readonly IYandexClient _yandexClient;
        public InnerAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IYandexClient yandexClient) : base(options, logger, encoder, clock)
        {
            _yandexClient = yandexClient;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorizationHeader = Context.Request.Headers[HeaderNames.Authorization].FirstOrDefault();
            if (authorizationHeader != null && authorizationHeader.StartsWith(BEARER_PREFIX))
            {
                var oauthToken = authorizationHeader.Replace(BEARER_PREFIX, "");
                if (oauthToken == null)
                {
                    return AuthenticateResult.NoResult();
                }

                var user = await _yandexClient.GetUser(oauthToken);
                if (user == null)
                {
                    return AuthenticateResult.NoResult();
                }

                var claimsPrincipal = new ClaimsPrincipal();
                var authenticationTicket = new AuthenticationTicket(Context.User, "YandexToken");

                return AuthenticateResult.Success(authenticationTicket);
            }

            return AuthenticateResult.NoResult();
        }

        protected override async Task HandleSignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        protected override Task HandleSignOutAsync(AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }
    }
}
