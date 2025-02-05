using Zeal.Bootcamp.DnD.Domain.Dice;

namespace Zeal.Bootcamp.DnD.Domain.Weapons;

public class Hands : Weapon
{
    internal Hands()
        : base(nameof(Staff), Die.D4, -2)
    {
    }
}