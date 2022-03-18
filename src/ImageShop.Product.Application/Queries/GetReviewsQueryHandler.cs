using AutoMapper;
using ImageShop.Product.Domain.ReviewAggregate;
using ImageShop.Product.Dtos.EventDtos;
using ImageShop.Product.Dtos.ReviewDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageShop.Product.Application.Queries
{
    public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, QueryResponseDto<List<ReviewDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _repository;

        public GetReviewsQueryHandler(IMapper mapper, IReviewRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<QueryResponseDto<List<ReviewDto>>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
        {
            var response = new QueryResponseDto<List<ReviewDto>>();

            try
            {
                var reviews = await _repository.GetList(request.ProductId);

                var dtos = _mapper.Map<List<ReviewDto>>(reviews);

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
