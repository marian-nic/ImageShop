using AutoMapper;
using ImageShop.Product.Domain.ProductAggregate;
using ImageShop.Product.Dtos.EventDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageShop.Product.Application.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CommandResponseDto<string>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public CreateProductCommandHandler(IMapper mapper, IProductRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommandResponseDto<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponseDto<string>();

            try
            {
                var owner = _mapper.Map<User>(request.Owner);
                var imageInfo = _mapper.Map<ImageInfo>(request.NewProduct.Image);

                var product = new Domain.ProductAggregate.Product(request.NewProduct.Title, request.NewProduct.Description, request.NewProduct.Price, Category.FromDisplayName<Category>(request.NewProduct.Category), owner);
                product.SetImage(imageInfo);
                product.SetTextMessages(request.NewProduct.TextMessages);
                product.SetTags(request.NewProduct.Tags);

                var id = await _repository.Create(product);

                response.ProcessedSuccessfully = true;
                response.Payload = id;

            }catch (Exception ex)
            {
                response.ProcessedSuccessfully = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}
