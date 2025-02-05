using Zeal.Bootcamp.DnD.Domain.Characters;

namespace Zeal.Bootcamp.DnD.Data.Repository;

public interface ICharacterRepository
{
    public Task<Character> Get(Guid id);

    public Task Save(Character character);
}