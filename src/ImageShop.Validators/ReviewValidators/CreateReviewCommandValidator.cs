using ImageShop.Product.Application.Commands;
using ImageShop.Validators.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ImageShop.Validators.ReviewValidators
{
    public class CreateReviewCommandValidator : BaseAbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(command => command.NewReview).NotEmpty().WithMessage("The review details are required and cannot be missing!");
            RuleFor(command => command.NewReview.ProductId).NotEmpty().WithMessage("Product id is madatory!");
            RuleFor(command => command.NewReview.Score).InclusiveBetween(1, 5);
            RuleFor(command => command.NewReview.Description).MaximumLength(500);

        }
    }
}
