using Zeal.Bootcamp.DnD.Domain.Dice;

namespace Zeal.Bootcamp.DnD.Domain.Weapons;

public class Weapon
{
    public static Hands Hands = new Hands();
    public static Longsword Longsword = new Longsword();
    public static Staff Staff = new Staff();

    internal Weapon(string name, Die baseDamage, int modifier = 0)
    {
        Name = name;
        BaseDamage = baseDamage;
        Modifier = modifier;
    }

    public Die BaseDamage { get; set; }

    public int Modifier { get; private set; }

    public string Name { get; private set; }

    public int RollDamage()
        => Math.Max(0, BaseDamage.Roll() + Modifier);
}