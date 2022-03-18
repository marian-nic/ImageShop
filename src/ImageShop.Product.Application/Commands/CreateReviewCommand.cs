using ImageShop.Product.Dtos.EventDtos;
using ImageShop.Product.Dtos.ReviewDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Application.Commands
{
    public class CreateReviewCommand:BaseCommand, IRequest<CommandResponseDto<string>>
    {
        public CreateReviewDto NewReview { get; set; }
    }
}
