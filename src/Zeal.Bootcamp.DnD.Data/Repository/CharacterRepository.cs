using Zeal.Bootcamp.DnD.Domain.Characters;

namespace Zeal.Bootcamp.DnD.Data.Repository;

internal class CharacterRepository : ICharacterRepository
{
    public Task<Character> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Save(Character character)
    {
        throw new NotImplementedException();
    }
}