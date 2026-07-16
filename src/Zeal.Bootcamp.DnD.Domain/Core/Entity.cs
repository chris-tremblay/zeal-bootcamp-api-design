using System.Diagnostics.CodeAnalysis;
using Zeal.Bootcamp.DnD.Domain.Extensions;

namespace Zeal.Bootcamp.DnD.Domain.Core;

/// <summary>
///   An object that has its own life-cycle and can be identified with a unique identifier.
/// </summary>
public class Entity
{
    protected Entity(object id)
    {
        Id = id ?? throw typeof(Entity).IsInvalidBecause($"id cannot be null.");
    }

    public object Id { get; protected set; }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }

    public static bool operator ==(Entity left, Entity right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Entity entity)
        {
            return Id.Equals(entity.Id);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

/// <summary>
///   An object that has a unique identity and lifecycle.
/// </summary>
/// <typeparam name="TIdentifier">The identifier used to uniquely identify the Entity.</typeparam>
/// <remarks>For more information, see: https://blog.jannikwempe.com/domain-driven-design-entities-value-objects.</remarks>
[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Generic Definition")]
public abstract class Entity<TIdentifier>(TIdentifier id)
    : Entity(id!), IEntity<TIdentifier>
{
    private object? _domainEventSink = null;

    /// <summary>
    ///   Gets the unique identifier for the entity.
    /// </summary>
    public new TIdentifier Id
    {
        get => (TIdentifier)base.Id;
        protected set => base.Id = value!;
    }

    public override int GetHashCode()
    {
        return Id!.GetHashCode();
    }

    void IEntity<TIdentifier>.SetId(TIdentifier id)
        => Id = id;

    internal void AttachDomainEventSink<TAggregate>(IDomainEventSink<TAggregate> sink)
        where TAggregate : class, IAggregateRoot
    {
        _domainEventSink = sink;
    }

    protected IDomainEventSink<TAggregate> GetDomainEventSink<TAggregate>()
        where TAggregate : class, IAggregateRoot
        => _domainEventSink as IDomainEventSink<TAggregate>
            ?? throw new InvalidOperationException(
                $"Domain event sink for '{typeof(TAggregate).Name}' has not been attached.");
}