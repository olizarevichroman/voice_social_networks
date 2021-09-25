using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using VoiceSocialNetworks.DataLayer.Abstractions;

namespace VoiceSocialNetworks.AuthenticationHandlers
{
    public class OAuthAuthenticationHandler : OAuthHandler<OAuthOptions>
    {
        private readonly IUserCreator _userCreator;

        public OAuthAuthenticationHandler(
            IOptionsMonitor<OAuthOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserCreator userCreator)
            : base(options, logger, encoder, clock)
        {
            _userCreator = userCreator;
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity,
            AuthenticationProperties properties,
            OAuthTokenResponse tokens)
        {
            var authenticationResult = await Context.AuthenticateAsync();
            var user = authenticationResult.Principal;
            await _userCreator.SyncVkUser(user, tokens);

            //no need to create ticket and do this stuff (, dust need to add/update info with tokens to DB
            var context = new OAuthCreatingTicketContext(new ClaimsPrincipal(identity), properties, Context, Scheme, Options, Backchannel, tokens, tokens.Response.RootElement);
            context.RunClaimActions();
            await Events.CreatingTicket(context).ConfigureAwait(false);
            
            return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
        }
    }
}