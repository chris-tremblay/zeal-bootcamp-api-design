namespace Zeal.Bootcamp.DnD.Domain.Core;

public interface IDomainEventSink<TAggregate>
    where TAggregate : IAggregateRoot
{
    void AddDomainEvent<TDomainEvent>(Func<TAggregate, TDomainEvent> factory)
        where TDomainEvent : IDomainEvent;
}