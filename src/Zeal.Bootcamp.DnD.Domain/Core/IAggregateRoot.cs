namespace Zeal.Bootcamp.DnD.Domain.Core;

public interface IAggregateRoot : IEntity;

public interface IAggregateRoot<TIdentifier>
    : IAggregateRoot, IEntity<TIdentifier>;