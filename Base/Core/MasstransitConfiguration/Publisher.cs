using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MasstransitConfiguration
{
    public class Publisher  
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public Publisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public Task Publish<T>(T message) where T : class
        {
            _publishEndpoint.Publish(message, context =>
            {
                context.CorrelationId = NewId.NextGuid();
            });
            return Task.CompletedTask;
        }
    }
}
