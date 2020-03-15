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
    public class InnerAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
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
                Console.WriteLine($"In {nameof(InnerAuthenticationHandler)}.{nameof(HandleAuthenticateAsync)} autorization header value = {authorizationHeader}");
                var oauthToken = authorizationHeader.Replace(BEARER_PREFIX, "").TrimStart();
                
                if (oauthToken == null)
                {
                    return AuthenticateResult.NoResult();
                }

                var userClaims = await _yandexClient.GetUserClaims(oauthToken);
                if (userClaims == null)
                {
                    return AuthenticateResult.NoResult();
                }

                var claimsPrincipal = new ClaimsPrincipal();
                var identity = new ClaimsIdentity(userClaims, "YandexToken");
                claimsPrincipal.AddIdentity(identity);
                var authenticationTicket = new AuthenticationTicket(claimsPrincipal, "YandexToken");

                return AuthenticateResult.Success(authenticationTicket);
            }

            return AuthenticateResult.NoResult();
        }
    }
}
