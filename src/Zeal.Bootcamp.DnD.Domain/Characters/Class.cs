using Zeal.Bootcamp.DnD.Domain.Core;
using Zeal.Bootcamp.DnD.Domain.Dice;
using Zeal.Bootcamp.DnD.Domain.Weapons;

namespace Zeal.Bootcamp.DnD.Domain.Characters;

/// <summary>A small, Player's Handbook-inspired character class catalog.</summary>
public sealed class Class : Enumeration<Class, string>
{
    public static readonly Class Barbarian = new("Barbarian", Die.D12,
        [WeaponProficiency.Simple, WeaponProficiency.Martial]);
    public static readonly Class Fighter = new("Fighter", Die.D10,
        [WeaponProficiency.Simple, WeaponProficiency.Martial]);
    public static readonly Class Mage = new("Mage", Die.D6, [WeaponProficiency.Simple]);
    public static readonly Class Thief = new("Thief", Die.D8, [WeaponProficiency.Simple]);
    public static readonly Class Wizard = new("Wizard", Die.D6, [WeaponProficiency.Simple]);

    private readonly IReadOnlySet<WeaponProficiency> _weaponProficiencies;

    private Class(string name, Die hitDie, IEnumerable<WeaponProficiency> weaponProficiencies)
        : base(name)
    {
        HitDie = hitDie;
        _weaponProficiencies = weaponProficiencies.ToHashSet();
    }

    public string Name => Value;
    public Die HitDie { get; }
    public IReadOnlySet<WeaponProficiency> WeaponProficiencies => _weaponProficiencies;
    public bool CanUse(Weapon weapon) => weapon == Weapon.Hands || _weaponProficiencies.Contains(weapon.Proficiency);
}
