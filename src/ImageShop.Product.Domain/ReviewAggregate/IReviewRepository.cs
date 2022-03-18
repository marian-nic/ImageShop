using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Domain.ReviewAggregate
{
    public interface IReviewRepository
    {
        public Task<List<Review>> GetList(string productId);
        public Task<string> Create(Review review);
    }
}
