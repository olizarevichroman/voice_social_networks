using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

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
                    webBuilder.UseKestrel(opt =>
                    {
                        opt.ListenAnyIP(443, listenOptions =>
                        {
                            listenOptions.UseHttps("certificate.pfx", "9786961roma");
                        });
                    });
                });
    }
}
