using ImageShop.Product.Domain.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Infrastructure.Cosmos.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private List<Domain.ProductAggregate.Product> _mockData = new List<Domain.ProductAggregate.Product>();

        public ProductRepository()
        {
            var product1 = new Domain.ProductAggregate.Product("Red Car", "A speeding red car", 2, Category.Wallpaper, new User("User 1", "user1@test.com"));
            product1.SetTags(new List<string>() { "car", "drive" });
            _mockData.Add(product1);

            var product2 = new Domain.ProductAggregate.Product("Sunset", "A beautiful sunset over the mountains", 5, Category.NotSpecified, new User("User 2", "user2@test.com"));
            product2.SetTags(new List<string>() { "sun", "sunset", "mountain" });
            _mockData.Add(product2);
        }

        public async Task<List<Domain.ProductAggregate.Product>> GetList(string title, string tag)
        {
            return _mockData;
        }
    }
}
