using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRecognition.Infrastructure.AzureVision.Engine
{
    public static class Extensions
    {
        public static List<string> ToTextMessages(this OcrResult input)
        {
            var messages = new List<string>();
            foreach(var region in input.Regions)
            {
                foreach(var line in region.Lines)
                {
                    messages.Add(string.Join(" ", line.Words.Select(x => x.Text)));
                }
            }

            return messages;
        }

        public static string ToParagraph(this OcrResult input)
        {
            return string.Join("\n", input.ToTextMessages());
        }

        public static List<string> ToTextMessages(this AnalyzeResults input)
        {
            var messages = new List<string>();
            foreach (var results in input.ReadResults)
            {
                foreach (var line in results.Lines)
                {
                    messages.Add(line.Text);
                }
            }

            return messages;
        }

        public static string ToParagraph(this AnalyzeResults input)
        {
            return string.Join("\n", input.ToTextMessages());
        }
    }
}
