using Microsoft.EntityFrameworkCore;
using Zeal.Bootcamp.DnD.Application;
using Zeal.Bootcamp.DnD.Data.Entities;
using Zeal.Bootcamp.DnD.Domain.Characters;
using Zeal.Bootcamp.DnD.Domain.Extensions;
using Zeal.Bootcamp.DnD.Domain.Weapons;

namespace Zeal.Bootcamp.DnD.Data.Repository;

internal class CharacterRepository(
    DnDContext db,
    IDomainEventsStorage domainEventsStorage) 
    : ICharacterRepository
{
    public async Task<Character?> Get(Guid id)
    {
        var entity = db.Set<CharacterEntity>()
            .Include(i => i.EquippedWeaponItem)
            .Include(i => i.Inventory).ThenInclude(i => i.Items)
            .Include(i => i.ExperienceTracker)
            .FirstOrDefault(i => i.CharacterId == id);

        if (entity is null)
            return null;

        return await Task.FromResult(new Character(
            entity.CharacterId,
            entity.Name,
            typeof(Class).GetStaticField<Class>(entity.Class),
            entity.EquippedWeaponItem is null
                ? Weapon.Hands
                : typeof(Weapon).GetStaticField<Weapon>(entity.EquippedWeaponItem.Name)));
    }

    public async Task Save(Character character)
    {
        var entity = db.Set<CharacterEntity>()
            .FirstOrDefault(i => i.CharacterId == character.Id);

        if (entity is null)
        {
            entity = new CharacterEntity
            {
                CharacterId = character.Id,
                Inventory = new InventoryEntity
                {
                    InventoryId = character.Inventory.Id,
                    CharacterId = character.Id,
                },
                ExperienceTracker = new ExperienceTrackerEntity
                {
                    ExperienceTrackerId = character.Experience.Id,
                    CharacterId = character.Id,
                },
            };
            db.Add(entity);
        }

        entity.Class = character.Class.Name;
        entity.Name = character.Name;
        entity.BackgroundStory = character.Background.Text;
        entity.ExperienceTracker.Points = character.Experience.Points;

        foreach (var item in character.Inventory.Items)
        {
            var itemEntity = entity.Inventory.Items.SingleOrDefault(i => i.InventoryItemId == item.Id);
            if (itemEntity is null)
            {
                itemEntity = new InventoryItemEntity
                {
                    InventoryItemId = item.Id,
                    InventoryId = entity.Inventory.InventoryId,
                };
                entity.Inventory.Items.Add(itemEntity);
            }

            itemEntity.Name = item.Name;
            itemEntity.ItemType = item.ItemType;
        }

        entity.EquippedWeaponItemId = character.EquippedWeaponItemId;

        domainEventsStorage.Enqueue(character.PullEvents());
    }
}