using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ImageShop.Product.Domain.DomainEvents
{
    public class NewReviewAddedDomainEvent: INotification
    {
        public int Score { get; private set; }
        public string ProductId { get; private set; }

        public NewReviewAddedDomainEvent(int score, string productId)
        {
            Score = score;
            ProductId = productId;
        }
    }
}
