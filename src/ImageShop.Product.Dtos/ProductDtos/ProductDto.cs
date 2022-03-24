using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Dtos.ProductDtos
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<string> Tags { get; set; }
        public List<string> TextMessages { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public ImageInfoDto Image { get; set; }

        public int NumberOfReviews { get; set; }
        public decimal ReviewScore { get; set; }
    }
}
