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

        public ProductRepository(IMapper mapper, ICosmosRepository<ProductModel> repository)
        {
            _mapper = mapper;
            _repository = repository;
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
                product.SetInitialScoreAndNumberOfReviews(p.ReviewScore, p.NumberOfReviews);
                return product;
            }).ToList();

        }

        public async Task<Domain.ProductAggregate.Product> GetById(string id)
        {
            var products = await _repository.GetListAsync(p => p.Id == id);

            return products.Select(p =>
            {
                var product = new Domain.ProductAggregate.Product(p.Title, p.Description, p.Price, Category.FromDisplayName<Category>(p.Category), p.Owner);
                product.SetTags(p.Tags);
                product.SetTextMessages(p.TextMessages);
                product.SetImage(p.Image);
                product.SetInitialScoreAndNumberOfReviews(p.ReviewScore, p.NumberOfReviews);
                return product;
            }).FirstOrDefault();
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
