using ImageShop.Product.Dtos.EventDtos;
using ImageShop.Product.Dtos.ProductDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Application.Commands
{
    public class CreateProductCommand: BaseCommand, IRequest<CommandResponseDto<string>>
    {
        public CreateProductDto NewProduct { get; set; }
    }
}
