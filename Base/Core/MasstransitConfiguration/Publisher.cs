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
        private IPublishEndpoint publishEndpoint;
        public Publisher(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }
        public Task Publish<T>(T message) where T : class
        {
            publishEndpoint.Publish<T>(message, c =>
            {
                c.CorrelationId = NewId.NextGuid();
            });
            return Task.CompletedTask;
        }
    }
}
