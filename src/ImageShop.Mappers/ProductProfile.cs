using ImageShop.Mappers.Abstractions;
using ImageShop.Product.Domain.ProductAggregate;
using ImageShop.Product.Dtos.ProductDtos;
using ImageShop.Product.Infrastructure.Cosmos;
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

            CreateMap<Product.Domain.ProductAggregate.Product, ProductModel>()
                .ForMember(x => x.PartitionKey, opt => opt.MapFrom(y => y.Category.ToString()))
                .ForMember(x => x.Category, opt => opt.MapFrom(y => y.Category.ToString()))
                .ForMember(x => x.Image, opt => opt.AllowNull());
        }
    }
}
