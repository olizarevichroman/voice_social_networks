using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace VoiceSocialNetworks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging(loggingBuilder =>
                    {
                        loggingBuilder.AddConsole();
                    });
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration(configurationBuilder => configurationBuilder.AddEnvironmentVariables());
                });
    }
}
