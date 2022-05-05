using System;
using System.Collections.Generic;

namespace ImageRecognition.Dtos
{
    public class AnalysisResultDto
    {
        public string Description { get; set; }

        public List<string> Tags { get; set; }
        public List<string> TextMessages { get; set; }
        public bool ContainsAdultContent { get; set; }
    }
}
