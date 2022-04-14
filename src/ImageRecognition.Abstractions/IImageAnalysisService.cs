using System;
using System.IO;
using System.Threading.Tasks;

namespace ImageRecognition.Abstractions
{
    public interface IImageAnalysisService<T> where T : class
    {
        Task<T> AnalyzeImage(string imageUrl);
        Task<T> AnalyzeImage(Stream imageStream);
    }
}
