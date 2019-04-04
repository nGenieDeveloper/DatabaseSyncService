using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog;
using System.Threading.Tasks;
using DatabaseSyncService.Models;
using DatabaseSyncService.Workers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace DatabaseSyncService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((services =>
                {
                    var Configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
                    services.Configure<WorkerModel>(Configuration.GetSection("Workers"));
                    services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<WorkerModel>>().Value);

                    services.AddHostedService<Worker1>();
                    services.AddHostedService<Worker2>();
                    services.AddHostedService<Worker3>();
                    services.AddHostedService<Worker4>();
                    services.AddHostedService<Worker5>();
                    
                    services.AddHostedService<Worker6>();
                    services.AddHostedService<Worker7>();
                    services.AddHostedService<Worker8>();
                    services.AddHostedService<Worker9>();
                    services.AddHostedService<Worker10>();

                    services.AddHostedService<Worker11>();
                    services.AddHostedService<Worker12>();
                    services.AddHostedService<Worker13>();
                    services.AddHostedService<Worker14>();
                    services.AddHostedService<Worker15>();

                    services.AddHostedService<Worker16>();
                    services.AddHostedService<Worker17>();
                    services.AddHostedService<Worker18>();
                    services.AddHostedService<Worker19>();
                    services.AddHostedService<Worker20>();

                    services.AddHostedService<Worker21>();
                    services.AddHostedService<Worker22>();
                    services.AddHostedService<Worker23>();
                    services.AddHostedService<Worker24>();
                    services.AddHostedService<Worker25>();

                    services.AddHostedService<Worker26>();
                    services.AddHostedService<Worker17>();
                    services.AddHostedService<Worker28>();
                    services.AddHostedService<Worker29>();
                    services.AddHostedService<Worker30>();

                    services.AddHostedService<Worker31>();
                    services.AddHostedService<Worker32>();
                    services.AddHostedService<Worker33>();
                    services.AddHostedService<Worker34>();
                    services.AddHostedService<Worker35>();

                    services.AddHostedService<Worker36>();
                    services.AddHostedService<Worker37>();
                    services.AddHostedService<Worker38>();
                    services.AddHostedService<Worker39>();
                    services.AddHostedService<Worker40>();

                    services.AddHostedService<Worker41>();
                    services.AddHostedService<Worker42>();
                    services.AddHostedService<Worker43>();
                    services.AddHostedService<Worker44>();
                    services.AddHostedService<Worker45>();

                    services.AddHostedService<Worker46>();
                    services.AddHostedService<Worker47>();
                    services.AddHostedService<Worker48>();
                    services.AddHostedService<Worker49>();
                    services.AddHostedService<Worker50>();

                }))
            .ConfigureLogging((hostingContext, builder) =>
            {
                builder.AddFile("./Logs/log-{Date}.txt", LogLevel.Warning);
            });

    }
}
