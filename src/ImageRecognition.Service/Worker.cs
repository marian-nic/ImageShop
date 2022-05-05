using ImageRecognition.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ImageRecognition.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IImageProcessorService _imageProcessorService;

        public Worker(ILogger<Worker> logger, IImageProcessorService imageProcessorService)
        {
            _logger = logger;
            _imageProcessorService = imageProcessorService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var uri = "<placeholder blob storage Generated SAS URI>";

                var result = await _imageProcessorService.AnalyzeImage(uri);
                if(result != null)
                {
                    _logger.LogInformation(result.Description);
                    result.Tags.ForEach(tag => _logger.LogInformation(tag));
                    result.TextMessages.ForEach(text => _logger.LogInformation(text));
                }

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
