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

            return products.Select( p => ProductModelToProduct(p)).ToList();

        }

        public async Task<Domain.ProductAggregate.Product> GetById(string id)
        {
            var products = await _repository.GetListAsync(p => p.Id == id);

            return products.Select(p => ProductModelToProduct(p)).FirstOrDefault();
        }

        public async Task<string> Create(Domain.ProductAggregate.Product product)
        {
            await _repository.InitializeDatabaseAndContainer();

            var producModel = _mapper.Map<ProductModel>(product);
            var result = await _repository.CreateAsync(producModel, true);

            return result.Id;
        }

        public async Task<Domain.ProductAggregate.Product> Update(Domain.ProductAggregate.Product product, bool returnUpdatedItem = false)
        {
            await _repository.InitializeDatabaseAndContainer();

            var productModelToUpdate = _mapper.Map<ProductModel>(product);
            var productModel = await _repository.UpdateAsync(productModelToUpdate, returnUpdatedItem);

            if (productModel == null)
                return null;


            var productResult = ProductModelToProduct(productModel);

            return productResult;
        }

        private Domain.ProductAggregate.Product ProductModelToProduct(ProductModel productModel)
        {
            var product = new Domain.ProductAggregate.Product(productModel.Id, productModel.Title, productModel.Description, productModel.Price, Category.FromDisplayName<Category>(productModel.Category), productModel.Owner);
            product.SetTags(productModel.Tags);
            product.SetTextMessages(productModel.TextMessages);
            product.SetImage(productModel.Image);
            product.SetInitialScoreAndNumberOfReviews(productModel.ReviewScore, productModel.NumberOfReviews);
            return product;
        }
    }
}
