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
                .AddOAuth<OAuthOptions, YandexAuthenticationHandler>("Yandex", options =>
                {
                    options.SaveTokens = true;
                    options.ForwardSignIn = "MyScheme";
                    options.CallbackPath = "/signin-yandex";
                    options.ClientId = "79a2aa9a61394c5595073a33b8c34a8d";
                    options.ClientSecret = "a258e53ae8a848b88c057c2f6231baad";
                    options.AuthorizationEndpoint = "https://oauth.yandex.ru/authorize";
                    options.TokenEndpoint = "https://oauth.yandex.ru/token";
                    options.UserInformationEndpoint = "https://login.yandex.ru/info";
                    options.SignInScheme = "MyScheme";
                    options.ClaimActions.MapAll();
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
            else
            {
                app.UseHsts();
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
