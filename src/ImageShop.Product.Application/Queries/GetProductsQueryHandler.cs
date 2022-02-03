using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ImageShop.Product.Domain.ProductAggregate;
using ImageShop.Product.Dtos.EventDtos;
using ImageShop.Product.Dtos.ProductDtos;
using MediatR;
using AutoMapper;

namespace ImageShop.Product.Application.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, QueryResponseDto<List<ProductDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public GetProductsQueryHandler(IMapper mapper, IProductRepository repository)
        {
            this._mapper = mapper;
            this._repository = repository;
        }
        public async Task<QueryResponseDto<List<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
           var response = new QueryResponseDto<List<ProductDto>>();

            try
            {
                var products = await _repository.GetList(request.SearchedTitle, request.SearchedTag);

                var dtos = _mapper.Map<List<ProductDto>>(products);

                response.ProcessedSuccessfully = true;
                response.Payload = dtos;
            }
            catch (Exception ex)
            {
                response.ProcessedSuccessfully = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}
