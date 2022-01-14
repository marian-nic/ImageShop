using ImageShop.Common.Abstractions;
using System;
using System.Collections.Generic;

namespace ImageShop.Product.Domain.ProductAggregate
{
    public class User: ValueObject
    {
        public string Name { get; private set; }
        public string Email { get; private set; }

        public User(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "User name is mandatory");

            if(string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email), "User email is mandatory");

            Name = name;
            Email = email;
        }

        protected override IEnumerable<object> GetIndividualComponents()
        {
            yield return Name;
            yield return Email;
        }
    }
}