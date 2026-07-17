using Zeal.Bootcamp.DnD.Domain.Core;

namespace Zeal.Bootcamp.DnD.Domain.Weapons;

public sealed class WeaponCategory : Enumeration<WeaponCategory, string>
{
    public static readonly WeaponCategory Unarmed = new("Unarmed");
    public static readonly WeaponCategory SimpleMelee = new("Simple Melee");
    public static readonly WeaponCategory SimpleRanged = new("Simple Ranged");
    public static readonly WeaponCategory MartialMelee = new("Martial Melee");
    public static readonly WeaponCategory MartialRanged = new("Martial Ranged");

    private WeaponCategory(string value) : base(value) { }
}
