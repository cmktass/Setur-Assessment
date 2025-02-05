using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.Options;

namespace Core.MasstransitConfiguration
{
    public static class MessageBrokerExtension
    {
        public static IServiceCollection AddMessageBroker(
        this IServiceCollection services,
        Assembly consumersAssembly,
        IConfiguration configuration,
        Action<IRabbitMqBusFactoryConfigurator, IBusRegistrationContext> config = null
        )
        {
            services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMq"));
            
            services.AddMassTransit(x =>
            {
                // Consumer'ı DI konteynerine ekliyoruz
                x.AddConsumers(consumersAssembly);

                // RabbitMQ kullanılarak bus yapılandırması
                x.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitSettings = context.GetRequiredService<IOptions<RabbitMqSettings>>().Value;
                    cfg.Host(rabbitSettings.Host, h =>
                    {
                        h.Username(rabbitSettings.Username);
                        h.Password(rabbitSettings.Password);
                    });
                    config(cfg, context);
                    // Mesajların alınacağı receive endpoint tanımlaması
                });
            });
            return services;
        }
    }
}
