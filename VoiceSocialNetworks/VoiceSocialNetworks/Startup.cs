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
                .AddOAuth<OAuthOptions, OAuthAuthenticationHandler>("Vk", options =>
                {
                    options.SaveTokens = true;
                    options.ForwardSignIn = "MyScheme";
                    options.CallbackPath = "/signin-vk";
                    options.ClientId = "7286728";
                    options.ClientSecret = "xtfH1IKoohNWaUd2JJrK";
                    options.AuthorizationEndpoint = "https://oauth.vk.com/authorize";
                    options.TokenEndpoint = "https://oauth.vk.com/access_token";
                    options.SignInScheme = "MyScheme";
                    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "user_id");
                })
                .AddScheme<AuthenticationSchemeOptions, InnerAuthenticationHandler>("MyScheme",
                (_) => { Console.WriteLine("yes"); });
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
