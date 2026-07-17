using Zeal.Bootcamp.DnD.Domain.Core;

namespace Zeal.Bootcamp.DnD.Domain.Weapons;

/// <summary>The simplified weapon proficiency groups used by the character builder.</summary>
public sealed class WeaponProficiency : Enumeration<WeaponProficiency, string>
{
    public static readonly WeaponProficiency Simple = new("Simple");
    public static readonly WeaponProficiency Martial = new("Martial");

    private WeaponProficiency(string value) : base(value) { }
}
