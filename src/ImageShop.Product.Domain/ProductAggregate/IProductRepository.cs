﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Domain.ProductAggregate
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetList(string title, string tag);
        public Task<string> Create(Domain.ProductAggregate.Product product);

        Task<Product> GetById(string id);
    }
}
