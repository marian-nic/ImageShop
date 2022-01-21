using ImageShop.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Domain.ProductAggregate
{
    public class ImageInfo : ValueObject
    {
        public string Url { get; private set; }
        public string OriginalFileName { get; private set; }
        public string ImageType { get; private set; }

        public ImageInfo(string url, string originalFileName, string imageType)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url), "Image URL is mandatory");

            if (string.IsNullOrWhiteSpace(originalFileName))
                throw new ArgumentNullException(nameof(originalFileName), "Original image name is mandatory");

            if (string.IsNullOrWhiteSpace(imageType))
                throw new ArgumentNullException(nameof(imageType), "Image content type is mandatory");

            Url = url;
            OriginalFileName = originalFileName;
            ImageType = imageType;
        }

        protected override IEnumerable<object> GetIndividualComponents()
        {
            yield return Url;
            yield return OriginalFileName;
            yield return ImageType;
        }
    }
}
