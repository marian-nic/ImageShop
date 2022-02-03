using ImageShop.Product.Dtos.EventDtos;
using ImageShop.Product.Dtos.ProductDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Application.Queries
{
    public class GetProductsQuery : IRequest<QueryResponseDto<List<ProductDto>>>
    {
        public string SearchedTitle { get; set; }
        public string SearchedTag { get; set; }
    }
}
