using AutoMapper;
using ImageShop.Product.Application.MediatorExtension;
using ImageShop.Product.Domain.ReviewAggregate;
using ImageShop.Product.Dtos.EventDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageShop.Product.Application.Commands
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, CommandResponseDto<string>>
    {
        private readonly IReviewRepository _repository;
        private readonly IMediator _mediator;

        public CreateReviewCommandHandler( IReviewRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<CommandResponseDto<string>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponseDto<string>();

            try
            {
                var review = new Review(request.NewReview.ProductId, request.NewReview.Description, request.NewReview.Score);
                review.RegisterNewReviewAddedDomainEvent();

                var id = await _repository.Create(review);

                response.ProcessedSuccessfully = true;
                response.Payload = id;

                await _mediator.DispatchDomainEventsAsync(review);

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
