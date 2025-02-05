using Zeal.Bootcamp.DnD.Domain.Dice;

namespace Zeal.Bootcamp.DnD.Domain.Weapons;

public class Staff : Weapon
{
    internal Staff()
        : base(nameof(Staff), Die.D4)
    {
    }
}