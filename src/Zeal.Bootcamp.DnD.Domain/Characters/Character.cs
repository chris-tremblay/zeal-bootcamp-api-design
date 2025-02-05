using Zeal.Bootcamp.DnD.Domain.Weapons;

namespace Zeal.Bootcamp.DnD.Domain.Characters;

public class Character
{
    internal Character(Guid id, string name, Class @class, Weapon weapon)
    {
        Class = @class;
        Id = id;
        Name = name;
        Weapon = weapon;
    }

    public static Character Create(string name, Class @class)
        => new Character(Guid.NewGuid(), name, @class, Weapon.Hands);

    public Class Class { get; private set; }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public Weapon Weapon { get; private set; }

    public int CalculateAttackDamage()
        => Weapon.RollDamage();

    public void Equip(Weapon weapon)
    {
        if (!Class.AvailableWeapons.Contains(weapon))
            throw new Exception($"{Class.Name} cannot use {weapon.Name}");

        Weapon = weapon;
    }

    public void UnequipWeapon()
    {
        Weapon = Weapon.Hands;
    }
}