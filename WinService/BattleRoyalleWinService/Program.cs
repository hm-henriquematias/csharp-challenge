using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Domain.Services;
using Microsoft.Extensions.Configuration;

namespace BattleRoyalleWinService
{
    public class Program
    {
        public static IConfigurationRoot Configuration;

        public static void Main()
        {
            CreateHostBuilder().Build().Run();
        }

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IConfiguration>(new ConfigurationBuilder()
                                                            .SetBasePath(
                                                                Path.Combine(AppContext.BaseDirectory)
                                                            )
                                                            .AddJsonFile(
                                                                "appsettings.json", 
                                                                optional: true
                                                             )
                                                            .Build()
                    )
                    .AddSingleton<IServerInfoService, ServerInfoService>()
                    .AddHostedService<Worker>();
                });
    }
}
