using Zeal.Bootcamp.DnD.Domain.Core;
using Zeal.Bootcamp.DnD.Domain.Dice;
using Zeal.Bootcamp.DnD.Domain.Weapons;

namespace Zeal.Bootcamp.DnD.Domain.Characters;

/// <summary>A small, Player's Handbook-inspired character class catalog.</summary>
public sealed class Class : Enumeration<Class, string>
{
    public static readonly Class Barbarian = new("Barbarian", Die.D12,
        [WeaponCategory.SimpleMelee, WeaponCategory.SimpleRanged, WeaponCategory.MartialMelee, WeaponCategory.MartialRanged]);
    public static readonly Class Fighter = new("Fighter", Die.D10,
        [WeaponCategory.SimpleMelee, WeaponCategory.SimpleRanged, WeaponCategory.MartialMelee, WeaponCategory.MartialRanged]);
    public static readonly Class Mage = new("Mage", Die.D6, [WeaponCategory.SimpleMelee]);
    public static readonly Class Thief = new("Thief", Die.D8,
        [WeaponCategory.SimpleMelee, WeaponCategory.SimpleRanged]);
    public static readonly Class Wizard = new("Wizard", Die.D6, [WeaponCategory.SimpleMelee]);

    private readonly IReadOnlySet<WeaponCategory> _weaponProficiencies;

    private Class(string name, Die hitDie, IEnumerable<WeaponCategory> weaponProficiencies)
        : base(name)
    {
        HitDie = hitDie;
        _weaponProficiencies = weaponProficiencies.ToHashSet();
    }

    public string Name => Value;
    public Die HitDie { get; }
    public IReadOnlySet<WeaponCategory> WeaponProficiencies => _weaponProficiencies;
    public bool CanUse(Weapon weapon) => weapon == Weapon.Hands || _weaponProficiencies.Contains(weapon.Category);
}
