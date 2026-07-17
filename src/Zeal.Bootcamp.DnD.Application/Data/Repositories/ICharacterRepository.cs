using Zeal.Bootcamp.DnD.Domain.Characters;

namespace Zeal.Bootcamp.DnD.Application.Data.Repositories;

public interface ICharacterRepository
{
    Task<Character?> Get(Guid id);

    Task Save(Character character);
}
