namespace Zeal.Bootcamp.DnD.Domain.Core;

public interface IEntity;

public interface IEntity<TIdentifier> : IEntity
{
    TIdentifier Id { get; }

    void SetId(TIdentifier id);
}