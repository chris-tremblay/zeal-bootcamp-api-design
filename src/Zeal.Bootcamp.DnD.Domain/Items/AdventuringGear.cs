namespace Zeal.Bootcamp.DnD.Domain.Items;

/// <summary>Non-weapon items that demonstrate that an inventory is not a weapon collection.</summary>
public sealed class AdventuringGear(string name) : Item(name)
{
    public static readonly AdventuringGear Torch = new("Torch");
    public static readonly AdventuringGear Rations = new("Rations (1 day)");
    public static readonly AdventuringGear Rope = new("Hempen rope (50 feet)");

}
