using Zeal.Bootcamp.DnD.Domain.Core;

namespace Zeal.Bootcamp.DnD.Domain.Dice;

/// <summary>The standard polyhedral dice used by D&amp;D.</summary>
public sealed class DieType : Enumeration<DieType, int>
{
    public static readonly DieType D4 = new(4);
    public static readonly DieType D6 = new(6);
    public static readonly DieType D8 = new(8);
    public static readonly DieType D10 = new(10);
    public static readonly DieType D12 = new(12);
    public static readonly DieType D20 = new(20);
    public static readonly DieType D100 = new(100);

    private DieType(int sides) : base(sides) { }
}
