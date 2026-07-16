namespace Zeal.Bootcamp.DnD.Domain.Core;

/// <summary>
///   An event occuring in an <see cref="Entity"/>.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    ///   The source of the event.
    /// </summary>
    public IAggregateRoot Source { get; }
}

/// <summary>
///   An event occuring in an <see cref="Entity"/>.
/// </summary>
/// <typeparam name="T">The type of the <see cref="Entity"/> that is the source of the event.</typeparam>
public interface IDomainEvent<T> : IDomainEvent
    where T : IAggregateRoot
{
    /// <summary>
    ///   The source of the event.
    /// </summary>
    public new T Source { get; }
}