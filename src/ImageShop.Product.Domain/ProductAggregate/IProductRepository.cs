using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Domain.ProductAggregate
{
    public interface IProductRepository
    {
        Task<List<Product>> GetList(string title, string tag);
        Task<string> Create(Domain.ProductAggregate.Product product);

        Task<Product> GetById(string id);

        Task<Domain.ProductAggregate.Product> Update(Domain.ProductAggregate.Product product, bool returnUpdatedItem = false);
    }
}
