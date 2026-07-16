using System.Diagnostics.CodeAnalysis;
using Zeal.Bootcamp.DnD.Domain.Core;

namespace Zeal.Bootcamp.DnD.Application;

public interface IDomainEventsStorage
{
    bool HasItems { get; }

    void Enqueue(IEnumerable<IDomainEvent> domainEvents);

    bool TryDequeue([NotNullWhen(true)] out IDomainEvent? domainEvent);
}