using ImageShop.Data.Abstraction;
using ImageShop.Product.Domain.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Infrastructure.Cosmos
{
    public class ProductModel: CosmosResource
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public List<string> Tags { get; set; }
        public List<string> TextMessages { get; set; }

        public decimal Price { get; set; }

        public User Owner { get; set; }

        public string Category { get; set; }

        public ImageInfo Image { get;  set; }
    }
}
