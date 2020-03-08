using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Security.Cryptography.X509Certificates;

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
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(options =>
                    {
#if DEBUG
                        options.ListenLocalhost(64039);
#else
                        options.ListenAnyIP(443, listenOptions =>
                        {
                            var certificatePath = Path.Combine(Directory.GetCurrentDirectory(), "cert.pfx");
                            var certificatePrivateKey = "9786961roma";
                            var certificate = new X509Certificate2(File.ReadAllBytes(certificatePath), certificatePrivateKey, X509KeyStorageFlags.DefaultKeySet);

                            listenOptions.UseHttps(certificate);
                        });
#endif
                    });
                });
    }
}
