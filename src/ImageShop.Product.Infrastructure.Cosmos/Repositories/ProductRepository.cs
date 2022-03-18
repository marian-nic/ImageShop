using AutoMapper;
using ImageShop.Data.Abstraction;
using ImageShop.Product.Domain.ProductAggregate;
using ImageShop.Product.Infrastructure.Cosmos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Infrastructure.Cosmos.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly ICosmosRepository<ProductModel> _repository;
        //private List<Domain.ProductAggregate.Product> _mockData = new List<Domain.ProductAggregate.Product>();

        public ProductRepository(IMapper mapper, ICosmosRepository<ProductModel> repository)
        {
            _mapper = mapper;
            _repository = repository;

            //var product1 = new Domain.ProductAggregate.Product("Red Car", "A speeding red car", 2, Category.Wallpaper, new User("User 1", "user1@test.com"));
            //product1.SetTags(new List<string>() { "car", "drive" });
            //_mockData.Add(product1);

            //var product2 = new Domain.ProductAggregate.Product("Sunset", "A beautiful sunset over the mountains", 5, Category.NotSpecified, new User("User 2", "user2@test.com"));
            //product2.SetTags(new List<string>() { "sun", "sunset", "mountain" });
            //product2.SetTextMessages(new List<string>() { "Over the edge", "The sun is going to sleep" });
            //product2.SetImage(new ImageInfo("dwedw dwd w", "sunset.png", "image/png"));
            //_mockData.Add(product2);

        }

        public async Task<List<Domain.ProductAggregate.Product>> GetList(string title, string tag)
        {
            var products = await _repository.GetListAsync(p => p.Title.Contains(title) || p.Tags.Contains(tag));

            return products.Select( p =>
            {
                var product = new Domain.ProductAggregate.Product(p.Title, p.Description, p.Price, Category.FromDisplayName<Category>(p.Category), p.Owner);
                product.SetTags(p.Tags);
                product.SetTextMessages(p.TextMessages);
                product.SetImage(p.Image);
                return product;
            }).ToList();

        }

        public async Task<string> Create(Domain.ProductAggregate.Product product)
        {
            await _repository.InitializeDatabaseAndContainer();

            var producModel = _mapper.Map<ProductModel>(product);
            var result = await _repository.CreateAsync(producModel, true);

            return result.Id;
        }
    }
}
