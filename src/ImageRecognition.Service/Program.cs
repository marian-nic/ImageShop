using ImageRecognition.Abstractions;
using ImageRecognition.Infrastructure.AzureVision;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageRecognition.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    AddComputerVisionConfiguration(hostContext, services);

                    services.AddHostedService<Worker>();
                });

        private static void AddComputerVisionConfiguration(HostBuilderContext hostContext, IServiceCollection services)
        {
            ExternalConnectionConfiguration imageAnalysisServiceConnectionConfiguration = new ExternalConnectionConfiguration();
            var imageAnalysisServiceConnectionConfigurationSection = hostContext.Configuration.GetSection("ImageAnalysisServiceConnectionConfiguration");
            imageAnalysisServiceConnectionConfigurationSection.Bind(imageAnalysisServiceConnectionConfiguration);

            services.Configure<ExternalConnectionConfiguration>(imageAnalysisServiceConnectionConfigurationSection);

            services.AddSingleton<IImageProcessorService, ImageProcessorService>();
        }
    }
}
