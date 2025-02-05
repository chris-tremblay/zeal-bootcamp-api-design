namespace Zeal.Bootcamp.DnD.Domain.Dice;

public class Die
{
    public static Die D10 = new Die(10);
    public static Die D100 = new Die(100);
    public static Die D12 = new Die(12);
    public static Die D20 = new Die(20);
    public static Die D4 = new Die(4);
    public static Die D6 = new Die(6);
    public static Die D8 = new Die(8);
    private static Random random = new Random();

    internal Die(int numberOfSides)
    {
        NumberOfSides = numberOfSides;
    }

    public int NumberOfSides { get; private set; }

    public int Roll()
        => random.Next(1, NumberOfSides + 1);
}