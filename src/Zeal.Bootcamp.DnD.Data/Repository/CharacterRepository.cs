using Zeal.Bootcamp.DnD.Data.Entities;
using Zeal.Bootcamp.DnD.Domain.Characters;
using Zeal.Bootcamp.DnD.Domain.Extensions;
using Zeal.Bootcamp.DnD.Domain.Weapons;

namespace Zeal.Bootcamp.DnD.Data.Repository;

internal class CharacterRepository(DnDContext db) : ICharacterRepository
{
    public async Task<Character?> Get(Guid id)
    {
        var entity = db.Set<CharacterEntity>()
            .FirstOrDefault(i => i.CharacterId == id);

        if (entity is null)
            return null;

        return await Task.FromResult(new Character(
            entity.CharacterId,
            entity.Name,
            typeof(Class).GetStaticField<Class>(entity.Class),
            typeof(Weapon).GetStaticField<Weapon>(entity.Weapon)));
    }

    public async Task Save(Character character)
    {
        var entity = db.Set<CharacterEntity>()
            .FirstOrDefault(i => i.CharacterId == character.Id);

        if(entity is null)
        {
            entity = new CharacterEntity
            {
                CharacterId = character.Id,
            };
            db.Add(entity);
        }

        entity.Class = character.Class.Name;
        entity.Name = character.Name;
        entity.Weapon = character.Weapon.Name;

        await db.SaveChangesAsync();
    }
}