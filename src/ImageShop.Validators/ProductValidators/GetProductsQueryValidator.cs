using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ImageShop.Product.Application.Queries;
using ImageShop.Validators.Abstractions;

namespace ImageShop.Validators.ProductValidators
{
    public class GetProductsQueryValidator :BaseAbstractValidator<GetProductsQuery>
    {
        public GetProductsQueryValidator()
        {
            RuleFor(query => query.SearchedTitle).NotEmpty().WithMessage("The search title term is missing!");
        }
    }
}
