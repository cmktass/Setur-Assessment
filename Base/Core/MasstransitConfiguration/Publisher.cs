using MassTransit;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MasstransitConfiguration
{
    public class Publisher  
    {
        private IPublishEndpoint _publishEndpoint;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Publisher(IPublishEndpoint publishEndpoint, IHttpContextAccessor httpContextAccessor)
        {
            _publishEndpoint = publishEndpoint;
            _httpContextAccessor = httpContextAccessor;
        }
        public Task Publish<T>(T message) where T : class
        {
            _publishEndpoint.Publish<T>(message, c =>
            {
                c.CorrelationId = GetCorrelationId();
            });
            return Task.CompletedTask;
        }
        private Guid GetCorrelationId()
        {
            var context = _httpContextAccessor.HttpContext;

            if (context?.Items.ContainsKey("CorrelationId") == true)
            {
                return new Guid(context.Items["CorrelationId"]?.ToString());
            }

            return new Guid(); // Eğer değer yoksa null döner
        }
    }
}
