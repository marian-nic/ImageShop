using FluentValidation;
using ImageShop.Product.Application.Queries;
using ImageShop.Validators.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Validators.ReviewValidators
{
    public class GetReviewsQueryValidator : BaseAbstractValidator<GetReviewsQuery>
    {
        public GetReviewsQueryValidator()
        {
            RuleFor(query => query.ProductId).NotEmpty().WithMessage("Product id is madatory!");
        }
    }
}
