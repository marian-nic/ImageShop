using ImageShop.Product.Dtos.EventDtos;
using ImageShop.Product.Dtos.ReviewDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Application.Queries
{
    public class GetReviewsQuery : IRequest<QueryResponseDto<List<ReviewDto>>>
    {
        public string ProductId { get; set; }
    }
}
