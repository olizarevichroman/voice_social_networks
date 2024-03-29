using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Security.Claims;
using VoiceSocialNetworks.AuthenticationHandlers;
using VoiceSocialNetworks.DataLayer.Abstractions;
using VoiceSocialNetworks.DataLayer.Implementations;
using Newtonsoft.Json;
using VoiceSocialNetworks.SDK.Clients;

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
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IVkClient, VkClient>();
            services.AddScoped<IYandexClient, YandexClient>();
            services.AddDbContext<ApplicationContext>();
            services.AddScoped<IUserCreator, UserCreator>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ApplicationContext>();
            services.AddScoped<UserRepository>();
            services.AddScoped<VkUserRepository>();
            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    });

            services.AddAuthentication(options =>
            {
                //options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = "PolicyBased";
                options.DefaultSignInScheme = "PolicyBased";
                options.DefaultChallengeScheme = "PolicyBased";
                options.DefaultSignOutScheme = "PolicyBased";
            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
                {
                    opt.LoginPath = "/";
                    opt.AccessDeniedPath = "/Error/AccessDenied";
                    opt.SlidingExpiration = true;
                    opt.ExpireTimeSpan = TimeSpan.FromDays(7);
                })
                .AddPolicyScheme("PolicyBased", "PolicyBased", options =>
                {
                    options.ForwardDefaultSelector = (context) =>
                    {
                        Console.WriteLine($"Request coming with path: {context.Request.Path}");
                        Console.WriteLine($"PolicyBased scheme challenged with Path = {context.Request.Path}");
                        var schemeName = context.Request.Path.Value == "/alice"
                                                            ? "YandexToken"
                                                            : CookieAuthenticationDefaults.AuthenticationScheme;

                        Console.WriteLine($"PolicyBased scheme returns scheme name = {schemeName}");

                        return schemeName;
                    };
                })
                .AddScheme<AuthenticationSchemeOptions, InnerAuthenticationHandler>("YandexToken", options =>
                {

                })
                .AddOAuth<OAuthOptions, OAuthAuthenticationHandler>("vkontakte", options =>
                {
                    options.SaveTokens = true;
                    options.ForwardSignIn = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.CallbackPath = "/signin-vk";
                    options.ClientId = "7301839";
                    options.ClientSecret = "WlXWXkieypiWMwgHRHcK";
                    options.AuthorizationEndpoint = "https://oauth.vk.com/authorize";
                    options.TokenEndpoint = "https://oauth.vk.com/access_token";
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "user_id");
                    options.Scope.Add("status");
                })
                .AddOAuth<OAuthOptions, YandexAuthenticationHandler>("Yandex", options =>
                {
                    options.SaveTokens = true;
                    options.ForwardSignIn = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.CallbackPath = "/signin-yandex";
                    options.ClientId = "79a2aa9a61394c5595073a33b8c34a8d";
                    options.ClientSecret = "a258e53ae8a848b88c057c2f6231baad";
                    options.AuthorizationEndpoint = "https://oauth.yandex.ru/authorize";
                    options.TokenEndpoint = "https://oauth.yandex.ru/token";
                    options.UserInformationEndpoint = "https://login.yandex.ru/info";
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.ClaimActions.MapAll();
                });
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/content",
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(),
                    "ClientApp/content"))
            });
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
