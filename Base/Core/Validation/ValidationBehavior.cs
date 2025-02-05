using Core.Exception;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validation
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private IValidator<TRequest> _validators;
        public ValidationBehavior(IValidator<TRequest> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationResult = await _validators.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var error = validationResult.Errors.FirstOrDefault().ErrorMessage;
                throw new BadRequestException(error);
            }
            var response = await next();
            return response;
        }
    }
}
