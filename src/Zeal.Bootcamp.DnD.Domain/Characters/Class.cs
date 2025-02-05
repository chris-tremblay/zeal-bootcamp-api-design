using Zeal.Bootcamp.DnD.Domain.Weapons;

namespace Zeal.Bootcamp.DnD.Domain.Characters;

public abstract class Class
{
    internal Class(string name, IEnumerable<Weapon> availableWeapons)
    {
        AvailableWeapons = availableWeapons;
        Name = name;
    }

    public string Name { get; private set; }

    internal IEnumerable<Weapon> AvailableWeapons { get; set; }
}