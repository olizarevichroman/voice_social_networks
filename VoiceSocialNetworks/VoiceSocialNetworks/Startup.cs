using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Security.Claims;
using VoiceSocialNetworks.AuthenticationHandlers;

namespace VoiceSocialNetworks
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAuthentication()
                .AddOAuth<OAuthOptions, SlackAuthenticationHandler>("Slack", options =>
                {
                    options.SaveTokens = true;
                    options.ForwardSignIn = "MyScheme";
                    options.CallbackPath = "/signin-slack";
                    options.ClientId = "898943090578.913936806982";
                    options.ClientSecret = "9ca7d53f4943c4224c1d26fc7009140d";
                    options.AuthorizationEndpoint = "https://slack.com/oauth/authorize";
                    options.TokenEndpoint = "https://slack.com/api/oauth.access";
                    options.SignInScheme = "MyScheme";
                    options.Scope.Add("identity.basic");
                    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "user_id");
                    options.ClaimActions.MapJsonKey("Team", "team_id");
                })
                .AddScheme<AuthenticationSchemeOptions, InnerAuthenticationHandler>("MyScheme",
                (_) => { Console.WriteLine("yes"); });
            
            //var state = Base64UrlTextEncoder.Encode(dataProtection.Protect(new byte[1]));
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
