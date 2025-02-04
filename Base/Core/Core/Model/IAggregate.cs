using Core.Core.Events;

namespace Core.Core.Model
{
    public interface IAggregate<T> : IAggregate, IEntity<T>
    {
    }
    public interface IAggregate : IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        IEvent[] ClearDomainEvents();
    }
}
