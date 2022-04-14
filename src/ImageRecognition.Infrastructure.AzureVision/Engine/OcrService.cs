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
    public class OcrService : IImageAnalysisService<OcrResult>
    {
        private readonly ComputerVisionClient _computerVisionClient;

        public OcrService(ExternalConnectionConfiguration connectionConfiguration)
        {
            _computerVisionClient = new ComputerVisionClient(new ApiKeyServiceClientCredentials(connectionConfiguration.Key),
                new System.Net.Http.DelegatingHandler[] { })
            {
                Endpoint = connectionConfiguration.Endpoint
            };  
        }

        public async Task<OcrResult> AnalyzeImage(string imageUrl)
        {
            var ocrResult = await _computerVisionClient.RecognizePrintedTextAsync(true, imageUrl);
            return ocrResult;
        }

        public async Task<OcrResult> AnalyzeImage(Stream imageStream)
        {
            var ocrResult = await _computerVisionClient.RecognizePrintedTextInStreamAsync(true, imageStream);
            return ocrResult;
        }
    }
}
