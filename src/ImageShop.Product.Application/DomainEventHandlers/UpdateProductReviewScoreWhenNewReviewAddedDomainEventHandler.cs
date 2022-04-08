using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ImageShop.Product.Domain.DomainEvents;
using ImageShop.Product.Domain.ProductAggregate;
using MediatR;

namespace ImageShop.Product.Application.DomainEventHandlers
{
    public class UpdateProductReviewScoreWhenNewReviewAddedDomainEventHandler : INotificationHandler<NewReviewAddedDomainEvent>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public UpdateProductReviewScoreWhenNewReviewAddedDomainEventHandler(IMapper mapper, IProductRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task Handle(NewReviewAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            var product = await _repository.GetById(notification.ProductId);

            if (product == null)
                return;

            product.AddAReviewScore(notification.Score);

           var result =  await _repository.Update(product, true);
        }
    }
}
