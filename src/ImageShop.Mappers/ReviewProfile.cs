using ImageShop.Mappers.Abstractions;
using ImageShop.Product.Domain.ReviewAggregate;
using ImageShop.Product.Dtos.ReviewDtos;
using ImageShop.Product.Infrastructure.Cosmos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Mappers
{
    public class ReviewProfile : BaseMapperProfile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Review, ReviewModel>()
            .ForMember(x => x.PartitionKey, opt => opt.MapFrom(y => y.ProductId)).ReverseMap();

        }
    }
}
