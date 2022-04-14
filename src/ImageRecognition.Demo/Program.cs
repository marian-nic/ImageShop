using ImageRecognition.Abstractions;
using System;
using System.Threading.Tasks;
using ImageRecognition.Infrastructure.AzureVision.Engine;

namespace ImageRecognition.Demo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var connection = new ExternalConnectionConfiguration()
            {
                Key = "<placeholder key>",
                Endpoint = "<placeholder endpoint uri>"
            };

            //var service = new ImageAnalysisService(connection);
            //var service = new OcrService(connection);
            var service = new TextService(connection);

            //string uri = "https://i.pinimg.com/originals/d9/18/03/d918030f0572ea28257de4b11d04b13f.jpg";
            string uri = "https://www.quotemaster.org/images/83/839dde1ff86905bd139f6478e14d3b90.jpg";
            //string uri = "https://www.rd.com/wp-content/uploads/2021/10/jessica-ennis-hill-inspirational-sports-quote.jpg?resize=700,700";

            var result = await service.AnalyzeImage(uri);
            Console.WriteLine(result.ToParagraph());
            Console.ReadLine();
        }
    }
}
