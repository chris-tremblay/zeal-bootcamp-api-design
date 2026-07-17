using Zeal.Bootcamp.DnD.Domain.Core;
using Zeal.Bootcamp.DnD.Domain.Characters.Events;
using Zeal.Bootcamp.DnD.Domain.Items;
using Zeal.Bootcamp.DnD.Domain.Weapons;

namespace Zeal.Bootcamp.DnD.Domain.Characters;

/// <summary>
///   The consistency boundary for a character, its inventory, and its progression. Changes to child entities are made
///   through this aggregate root.
/// </summary>
public sealed class Character : AggregateRoot<Guid>
{
    private readonly ExperienceTracker _experience;
    private readonly Inventory _inventory;

    internal Character(Guid id, string name, Class @class, Weapon weapon)
        : this(id, new CharacterName(name), @class, BackgroundStory.Empty)
    {
        if (weapon != Weapon.Hands)
        {
            InventoryItem item = _inventory.Add(weapon);
            EquippedWeaponItemId = item.Id;
        }
    }

    private Character(Guid id, CharacterName name, Class @class, BackgroundStory background)
        : base(id)
    {
        Name = name;
        Class = @class ?? throw new DomainException("A character class is required.");
        Background = background;
        _inventory = new Inventory(Guid.NewGuid());
        _experience = new ExperienceTracker(Guid.NewGuid());
    }

    public BackgroundStory Background { get; private set; }

    public Class Class { get; private set; }

    public Guid? EquippedWeaponItemId { get; private set; }

    /// <summary>
    ///   Returns immutable state, never the mutable child entity.
    /// </summary>
    public ExperienceSnapshot Experience => _experience.ToSnapshot();

    /// <summary>
    ///   Returns immutable state, never the mutable child entity or its children.
    /// </summary>
    public InventorySnapshot Inventory => _inventory.ToSnapshot();

    public int Level => _experience.Level;

    public CharacterName Name { get; private set; }

    public Weapon Weapon => EquippedWeaponItemId is Guid id
        ? (Weapon)_inventory.Get(id).Item
        : Weapon.Hands;

    public static Character Create(
        string name,
        Class @class,
        string backgroundStory = "",
        IEnumerable<Weapon>? startingWeapons = null)
    {
        var character = new Character(
            Guid.NewGuid(),
            new CharacterName(name),
            @class,
            new BackgroundStory(backgroundStory));

        foreach (Weapon weapon in startingWeapons ?? [])
            character.AddWeapon(weapon);

        character.AddDomainEvent(new CharacterCreatedDomainEvent(character));
        return character;
    }

    public InventoryItemSnapshot AddItem(Item item)
    {
        if (item is Weapon weapon && !Class.CanUse(weapon))
            throw new DomainException($"A {Class.Name} is not proficient with {weapon.Name}.");

        return _inventory.Add(item).ToSnapshot();
    }

    public InventoryItemSnapshot AddWeapon(Weapon weapon)
    {
        return AddItem(weapon);
    }

    public void AwardExperience(int points)
    {
        int oldLevel = Level;
        _experience.Add(points);
        AddDomainEvent(new ExperienceAwardedDomainEvent(this, points, oldLevel, Level));
    }

    public int CalculateAttackDamage() => Weapon.RollDamage();

    public void ChangeBackground(string story) => Background = new BackgroundStory(story);

    public void Equip(Guid inventoryItemId)
    {
        InventoryItem item = _inventory.Get(inventoryItemId);
        if (item.Item is not Weapon weapon)
            throw new DomainException($"{item.Item.Name} is not a weapon and cannot be equipped.");

        if (!Class.CanUse(weapon))
            throw new DomainException($"A {Class.Name} cannot equip {weapon.Name}.");

        EquippedWeaponItemId = item.Id;
    }

    /// <summary>
    ///   Compatibility overload for the original bootcamp example.
    /// </summary>
    public void Equip(Weapon weapon)
    {
        InventoryItem? item = _inventory.Items.FirstOrDefault(i => i.Item == weapon);
        if (item is null)
        {
            InventoryItemSnapshot addedItem = AddWeapon(weapon);
            item = _inventory.Get(addedItem.Id);
        }

        Equip(item.Id);
    }

    public void Rename(string name) => Name = new CharacterName(name);

    public void UnequipWeapon() => EquippedWeaponItemId = null;
}
