using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core.Events
{
    public interface IEvent
    {
        Guid EventId => Guid.NewGuid();

        public DateTime OccurredOn => DateTime.Now;

        public string EventType => GetType().AssemblyQualifiedName;
    }
}
