using ImageShop.Data.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ImageShop.Data.Abstraction
{
    public interface ICosmosRepository<T> where T : CosmosResource
    {
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> filterExpression, string partitionKey = null);
        Task InitializeDatabaseAndContainer();
        Task<T> CreateAsync(T item, bool returnCreatedItem = false);
    }
}