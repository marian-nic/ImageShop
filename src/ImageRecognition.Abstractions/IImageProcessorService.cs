using ImageRecognition.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRecognition.Abstractions
{
    public interface IImageProcessorService
    {
        Task<AnalysisResultDto> AnalyzeImage(byte[] imageContent);
        Task<AnalysisResultDto> AnalyzeImage(string imageUri);
    }
}
