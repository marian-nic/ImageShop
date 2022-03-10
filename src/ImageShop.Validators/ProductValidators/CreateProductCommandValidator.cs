using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ImageShop.Product.Application.Commands;
using ImageShop.Validators.Abstractions;

namespace ImageShop.Validators.ProductValidators
{
    public class CreateProductCommandValidator : BaseAbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(command => command.NewProduct).NotEmpty().WithMessage("The image details are required and cannot be missing!");
            RuleFor(command => command.NewProduct.Title).NotEmpty().WithMessage("The image title is required and cannot be missing!");
            RuleFor(command => command.NewProduct.Category).NotEmpty().WithMessage("The image category is required and cannot be missing!");
            RuleFor(command => command.NewProduct.Description).MaximumLength(500).WithMessage("The image description must have a maximum of 500 characters!");

        }
    }
}
