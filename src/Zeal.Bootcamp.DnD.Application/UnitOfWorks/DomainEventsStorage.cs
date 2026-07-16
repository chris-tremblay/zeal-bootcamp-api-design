using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Zeal.Bootcamp.DnD.Domain.Core;

namespace Zeal.Bootcamp.DnD.Application;

public class DomainEventsStorage : IDomainEventsStorage
{
    private readonly ConcurrentQueue<IDomainEvent> _domainEvents = new();

    public bool HasItems => _domainEvents.Any();

    public void Enqueue(IEnumerable<IDomainEvent> domainEvents)
    {
        foreach (IDomainEvent domainEvent in domainEvents)
            _domainEvents.Enqueue(domainEvent);
    }

    public bool TryDequeue([NotNullWhen(true)] out IDomainEvent? domainEvent) =>
        _domainEvents.TryDequeue(out domainEvent);
}