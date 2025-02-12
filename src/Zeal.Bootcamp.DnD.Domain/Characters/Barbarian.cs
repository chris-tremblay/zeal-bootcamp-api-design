using Zeal.Bootcamp.DnD.Domain.Weapons;

namespace Zeal.Bootcamp.DnD.Domain.Characters;

public class Barbarian : Class
{
    internal Barbarian()
        : base(nameof(Barbarian), [Weapon.Longsword])
    {
    }
}