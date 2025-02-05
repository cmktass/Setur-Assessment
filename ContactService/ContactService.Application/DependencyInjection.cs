using ContactService.Application.Commands;
using Core.Validation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace ContactService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssemblyContaining<CreateContactCommandValidator>();
            return services;
        }
    }
}
