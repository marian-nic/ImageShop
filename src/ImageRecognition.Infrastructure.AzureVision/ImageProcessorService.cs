using ImageRecognition.Abstractions;
using ImageRecognition.Dtos;
using ImageRecognition.Infrastructure.AzureVision.Engine;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRecognition.Infrastructure.AzureVision
{
    public class ImageProcessorService: IImageProcessorService
    {
        private ImageAnalysisService _imageAnalysisService;
        private TextService _textService;

        public ImageProcessorService(IOptions<ExternalConnectionConfiguration> connectionConfiguration)
        {
            _imageAnalysisService = new ImageAnalysisService(connectionConfiguration.Value);
            _textService = new TextService(connectionConfiguration.Value);
        }

        public async Task<AnalysisResultDto> AnalyzeImage(byte[] imageContent)
        {
            if(imageContent == null || !imageContent.Any())
            {
                throw new ArgumentNullException(nameof(imageContent));
            }

            ImageAnalysis imageAnalysisResult;
            AnalyzeResults textAnalyzeResult;

            using(var stream = new MemoryStream(imageContent))
            {
                imageAnalysisResult =  await _imageAnalysisService.AnalyzeImage(stream);
            }

            using (var stream = new MemoryStream(imageContent))
            {
                textAnalyzeResult = await _textService.AnalyzeImage(stream);
            }

            return CreateAnalysisResult(imageAnalysisResult, textAnalyzeResult);
        }

        public async Task<AnalysisResultDto> AnalyzeImage(string imageUri)
        {
            if (string.IsNullOrWhiteSpace(imageUri))
            {
                throw new ArgumentNullException(nameof(imageUri));
            }

            ImageAnalysis imageAnalysisResult;
            AnalyzeResults textAnalyzeResult;

            imageAnalysisResult = await _imageAnalysisService.AnalyzeImage(imageUri);

            textAnalyzeResult = await _textService.AnalyzeImage(imageUri);

            return CreateAnalysisResult(imageAnalysisResult, textAnalyzeResult);
        }

        private AnalysisResultDto CreateAnalysisResult(ImageAnalysis imageAnalysisResult, AnalyzeResults textAnalyzeResult)
        {
            var result = new AnalysisResultDto();

            if(imageAnalysisResult != null)
            {
                if(imageAnalysisResult.Adult != null)
                {
                    result.ContainsAdultContent = imageAnalysisResult.Adult.IsAdultContent || imageAnalysisResult.Adult.IsGoryContent;
                }

                result.Tags = imageAnalysisResult.Tags.Where(x => x.Confidence > 0.6).Select(x => x.Name).ToList();
                result.Description = imageAnalysisResult.Description.Captions.OrderByDescending(x => x.Confidence).First().Text;
            }

            if(textAnalyzeResult != null)
            {
                result.TextMessages = textAnalyzeResult.ToTextMessages();
            }

            return result;
        }
    }
}
