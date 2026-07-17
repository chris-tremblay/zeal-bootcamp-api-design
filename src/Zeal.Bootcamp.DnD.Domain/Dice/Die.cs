using Zeal.Bootcamp.DnD.Domain.Core;

namespace Zeal.Bootcamp.DnD.Domain.Dice;

/// <summary>A die is a value object: two dice with the same number of sides are equivalent.</summary>
public sealed class Die(DieType type) : ValueObject<Die>
{
    public static readonly Die D4 = new(DieType.D4);
    public static readonly Die D6 = new(DieType.D6);
    public static readonly Die D8 = new(DieType.D8);
    public static readonly Die D10 = new(DieType.D10);
    public static readonly Die D12 = new(DieType.D12);
    public static readonly Die D20 = new(DieType.D20);
    public static readonly Die D100 = new(DieType.D100);

    public DieType Type { get; } = type;
    public int NumberOfSides => Type.Value;
    public int Roll() => Random.Shared.Next(1, NumberOfSides + 1);

    public IReadOnlyCollection<int> Roll(int numberOfDice)
    {
        if (numberOfDice < 1)
            throw new DomainException("At least one die must be rolled.");

        return Enumerable.Range(0, numberOfDice).Select(_ => Roll()).ToArray();
    }

    protected override bool EqualsCore(Die other) => Type == other.Type;
    protected override int GetHashCodeCore() => Type.GetHashCode();
}
