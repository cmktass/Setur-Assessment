

using MediatR;

namespace Core.Core.Events
{
    public interface IDomainEvent : IEvent, INotification
    {
        public int CurrentUserId { get; set; }
        public string CorrelationId { get; set; }
    }

    public class DomainEvent : IDomainEvent
    {
        public int CurrentUserId { get; set; }
        public string CorrelationId { get; set; }
    }
}
