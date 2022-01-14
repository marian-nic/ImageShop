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

        public IReadOnlyCollection<string> Tags => _tags.AsReadOnly();
        public IReadOnlyCollection<string> TextMessages => _textMessages.AsReadOnly();

        public decimal Price { get; private set; }

        public User Owner { get; set; }

        public Category Category { get; set; }



    }
}
