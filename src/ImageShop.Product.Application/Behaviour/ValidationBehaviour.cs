using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;
using ImageShop.Product.Application.Exceptions;

namespace ImageShop.Product.Application.Behaviour
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationBehaviour(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validationResponse = _validator.Validate(request);

            if(validationResponse != null && !validationResponse.IsValid)
            {
                throw new CustomValidationException(String.Join("\n", validationResponse.Errors));
            }

            return await next();
        }
    }
}
