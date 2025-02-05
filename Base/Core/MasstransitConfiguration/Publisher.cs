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
        
        public Publisher()
        {
            
        }
        public Task Publish<T>(T message) where T : class
        {
            return Task.CompletedTask;
        }
    }
}
