using System.Diagnostics.CodeAnalysis;

namespace Zeal.Bootcamp.DnD.Domain.Core;

/// <inheritdoc/>
public class DomainEvent(IAggregateRoot source) : IDomainEvent
{
    /// <inheritdoc/>
    public virtual IAggregateRoot Source { get; protected set; } = source;
}

/// <inheritdoc/>
[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Generic Definition")]
public class DomainEvent<T>(T source)
    : DomainEvent(source), IDomainEvent<T>
    where T : IAggregateRoot
{
    /// <inheritdoc/>
    public new T Source
    {
        get => (T)base.Source;
        protected set => base.Source = value;
    }
}