using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageShop.Common.Abstractions;
using MediatR;

namespace ImageShop.Product.Application.MediatorExtension
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, Entity entity)
        {
            if (entity == null || entity.DomainEvents == null || !entity.DomainEvents.Any())
                return;

            //Publish events
            foreach(var domainEvent in entity.DomainEvents)
                await mediator.Publish(domainEvent);

            //clear them
            entity.ClearDomainEvents();
        }
    }
}
