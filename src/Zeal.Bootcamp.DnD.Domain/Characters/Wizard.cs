using Zeal.Bootcamp.DnD.Domain.Weapons;

namespace Zeal.Bootcamp.DnD.Domain.Characters;

public class Wizard : Class
{
    internal Wizard()
        : base(nameof(Wizard), [Weapon.Hands, Weapon.Staff])
    {
    }
}