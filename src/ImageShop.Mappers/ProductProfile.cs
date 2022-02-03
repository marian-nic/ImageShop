using ImageShop.Mappers.Abstractions;
using ImageShop.Product.Domain.ProductAggregate;
using ImageShop.Product.Dtos.ProductDtos;
using System;

namespace ImageShop.Mappers
{
    public class ProductProfile : BaseMapperProfile
    {
        public ProductProfile()
        {
            CreateMap<ImageInfo, ImageInfoDto>();

            CreateMap<Product.Domain.ProductAggregate.Product, ProductDto>()
                .ForMember(x => x.Category, opt => opt.MapFrom(y => y.Category.ToString()))
                .ForMember(x => x.Tags, opt => opt.AllowNull())
                .ForMember(x => x.TextMessages, opt => opt.AllowNull());
        }
    }
}
