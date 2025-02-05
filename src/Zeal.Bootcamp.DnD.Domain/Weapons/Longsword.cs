using Zeal.Bootcamp.DnD.Domain.Dice;

namespace Zeal.Bootcamp.DnD.Domain.Weapons;

public class Longsword : Weapon
{
    internal Longsword()
        : base(nameof(Longsword), Die.D6)
    {
    }
}