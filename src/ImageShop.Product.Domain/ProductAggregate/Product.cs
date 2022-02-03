using ImageShop.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Domain.ProductAggregate
{
    public class Product: Entity, IAggregateRoot
    {
        private List<string> _tags;
        private List<string> _textMessages;

        public string Title { get; private set; }
        public string Description { get; private set; }

        public IReadOnlyCollection<string> Tags => _tags?.AsReadOnly();
        public IReadOnlyCollection<string> TextMessages => _textMessages?.AsReadOnly();

        public decimal Price { get; private set; }

        public User Owner { get; private set; }

        public Category Category { get; private set; }

        public ImageInfo Image { get; private set; }


        public Product(string id, string title, string description, decimal price, Category category, User owner): this(title, description, price, category, owner)
        {
            if (!string.IsNullOrWhiteSpace(id))
                Id = id;
        }

        public Product(string title, string description, decimal price, Category category, User owner)
        {
            if (string.IsNullOrWhiteSpace(Id))
                Id = Guid.NewGuid().ToString();

            if(string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title), "Title is mandatory");
            Title = title;
            Description = description;

            if(price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), price, "Price must be a positive or zero value");
            Price = price;

            if (category == null)
                Category = Category.NotSpecified;
            else
                Category = category;

            if(owner == null)
                throw new ArgumentNullException(nameof(owner), "Owner is mandatory");
            Owner = owner;
        }

        public void SetTags(List<string> tags)
        {
            _tags = tags.Where(t => !string.IsNullOrWhiteSpace(t)).Distinct().ToList();
        }

        public void SetTextMessages(List<string> textMessages)
        {
            _textMessages = textMessages.Where(t => !string.IsNullOrWhiteSpace(t)).Distinct().ToList();
        }
    }
}
