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
    public class ImageAnalysisService : IImageAnalysisService<ImageAnalysis>
    {
        private readonly ComputerVisionClient _computerVisionClient;

        private readonly List<VisualFeatureTypes?> _features = new List<VisualFeatureTypes?>() {
            VisualFeatureTypes.Categories, VisualFeatureTypes.Adult, VisualFeatureTypes.ImageType, VisualFeatureTypes.Description, VisualFeatureTypes.Tags, VisualFeatureTypes.Faces,
            VisualFeatureTypes.Brands, VisualFeatureTypes.Objects
        };

        public ImageAnalysisService(ExternalConnectionConfiguration connectionConfiguration)
        {
            _computerVisionClient = new ComputerVisionClient(new ApiKeyServiceClientCredentials(connectionConfiguration.Key),
                new System.Net.Http.DelegatingHandler[] { })
            {
                Endpoint = connectionConfiguration.Endpoint
            };  
        }

        public async Task<ImageAnalysis> AnalyzeImage(string imageUrl)
        {
            var analysisResult = await _computerVisionClient.AnalyzeImageAsync(imageUrl, _features);
            return analysisResult;
        }

        public async Task<ImageAnalysis> AnalyzeImage(Stream imageStream)
        {
            var analysisResult = await _computerVisionClient.AnalyzeImageInStreamAsync(imageStream, _features);
            return analysisResult;
        }
    }
}
