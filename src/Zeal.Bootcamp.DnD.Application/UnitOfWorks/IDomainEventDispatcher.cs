using Zeal.Bootcamp.DnD.Domain.Core;

namespace Zeal.Bootcamp.DnD.Application;

/// <summary>
///   Dispatches domain events.
/// </summary>
public interface IDomainEventDispatcher
{
    /// <summary>
    ///   Dispatches a <see cref="IDomainEvent"/>.
    /// </summary>
    /// <param name="domainEvent">The <see cref="IDomainEvent"/> to dispatch.</param>
    public Task Dispatch(IDomainEvent domainEvent);

    /// <summary>
    ///   Dispatches a <see cref="IDomainEvent{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Entity"/> raising the <see cref="IDomainEvent{T}"/>.</typeparam>
    /// <param name="domainEvent">The <see cref="IDomainEvent{T}"/> to dispatch.</param>
    public Task Dispatch<T>(IDomainEvent<T> domainEvent)
        where T : IAggregateRoot;
}