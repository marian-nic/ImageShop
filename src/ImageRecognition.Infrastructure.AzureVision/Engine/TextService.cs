using ImageRecognition.Abstractions;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImageRecognition.Infrastructure.AzureVision.Engine
{
    public class TextService : IImageAnalysisService<AnalyzeResults>
    {
        private readonly ComputerVisionClient _computerVisionClient;

        public TextService(ExternalConnectionConfiguration connectionConfiguration)
        {
            _computerVisionClient = new ComputerVisionClient(new ApiKeyServiceClientCredentials(connectionConfiguration.Key),
                new System.Net.Http.DelegatingHandler[] { })
            {
                Endpoint = connectionConfiguration.Endpoint
            };  
        }

        public async Task<AnalyzeResults> AnalyzeImage(string imageUrl)
        {
            var headerResult = await _computerVisionClient.ReadAsync(imageUrl);
            return await GetTextAsync(headerResult.OperationLocation);
        }

        public async Task<AnalyzeResults> AnalyzeImage(Stream imageStream)
        {
            var headerResult = await _computerVisionClient.ReadInStreamAsync(imageStream);
            return await GetTextAsync(headerResult.OperationLocation);
        }

        private async Task<AnalyzeResults> GetTextAsync(string operationLocation)
        {
            int numerOfCharsInOperationId = 36;

            string operationId = operationLocation.Substring(operationLocation.Length - numerOfCharsInOperationId);
            var guid = new Guid(operationId);

            ReadOperationResult result =  await _computerVisionClient.GetReadResultAsync(guid);

            //wait for operation to complete
            int i = 0;
            int maxRetries = 10;
            while ((result.Status == OperationStatusCodes.Running || 
                result.Status == OperationStatusCodes.NotStarted) && i++ < maxRetries)
            {
                await Task.Delay(1000);
                result = await _computerVisionClient.GetReadResultAsync(guid);
            }

            return result.AnalyzeResult;
        }
    }
}
