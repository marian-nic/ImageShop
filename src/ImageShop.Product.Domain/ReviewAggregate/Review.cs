using ImageShop.Common.Abstractions;
using ImageShop.Product.Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Domain.ReviewAggregate
{
    public class Review : Entity, IAggregateRoot
    {
        public string Description { get; private set; }
        public int Score { get; private set; }
        public string ProductId { get; private set; }


        public Review(string productId, string description, int score)
        {
            if (string.IsNullOrWhiteSpace(Id))
                Id = Guid.NewGuid().ToString();

            Description = description;

            if (score < 1 && score > 5)
                throw new ArgumentException($"Score {score} is invalid. It must be between 1 and 5");

            Score = score;
            if (string.IsNullOrWhiteSpace(productId))
                throw new ArgumentNullException(nameof(productId), "Product id is required");
            ProductId = productId;
        }

        public Review(string id, string productId, string description, int score) : this(productId, description, score)
        {

            if (!string.IsNullOrWhiteSpace(id))
                Id = id;
        }

        public void RegisterNewReviewAddedDomainEvent()
        {
            AddDomainEvent(new NewReviewAddedDomainEvent(Score, ProductId));
        }
    }
}
