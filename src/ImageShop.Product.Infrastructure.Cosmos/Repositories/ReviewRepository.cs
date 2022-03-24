using AutoMapper;
using ImageShop.Data.Abstraction;
using ImageShop.Product.Domain.ReviewAggregate;
using ImageShop.Product.Infrastructure.Cosmos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Infrastructure.Cosmos.Repositories
{
    public class ReviewRepository: IReviewRepository
    {
        private readonly IMapper _mapper;
        private readonly ICosmosRepository<ReviewModel> _repository;

        public ReviewRepository(IMapper mapper, ICosmosRepository<ReviewModel> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<string> Create(Review review)
        {
            await _repository.InitializeDatabaseAndContainer();

            var reviewModel = _mapper.Map<ReviewModel>(review);
            await _repository.CreateAsync(reviewModel, false);

            return review.Id;
        }

        public async Task<List<Review>> GetList(string productId)
        {
            var reviews = await _repository.GetListAsync(r => r.ProductId == productId);

            return _mapper.Map<List<Review>>(reviews);
        }
    }
}
