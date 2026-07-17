using Zeal.Bootcamp.DnD.Domain.Dice;
using Zeal.Bootcamp.DnD.Domain.Items;

namespace Zeal.Bootcamp.DnD.Domain.Weapons;

/// <summary>A weapon is one specialized kind of item.</summary>
public sealed class Weapon(
    string name,
    WeaponCategory category,
    Die damageDie,
    int damageModifier = 0) : Item(name)
{
    public static readonly Weapon Hands = new("Hands", WeaponCategory.Unarmed, Die.D4, -2);
    public static readonly Weapon Club = new("Club", WeaponCategory.SimpleMelee, Die.D4);
    public static readonly Weapon Dagger = new("Dagger", WeaponCategory.SimpleMelee, Die.D4);
    public static readonly Weapon Staff = new("Quarterstaff", WeaponCategory.SimpleMelee, Die.D6);
    public static readonly Weapon Shortbow = new("Shortbow", WeaponCategory.SimpleRanged, Die.D6);
    public static readonly Weapon Shortsword = new("Shortsword", WeaponCategory.MartialMelee, Die.D6);
    public static readonly Weapon Longsword = new("Longsword", WeaponCategory.MartialMelee, Die.D8);
    public static readonly Weapon Longbow = new("Longbow", WeaponCategory.MartialRanged, Die.D8);

    public WeaponCategory Category { get; } = category;
    public Die DamageDie { get; } = damageDie;
    public int DamageModifier { get; } = damageModifier;
    public int RollDamage() => Math.Max(0, DamageDie.Roll() + DamageModifier);
}
