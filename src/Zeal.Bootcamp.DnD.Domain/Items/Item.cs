using Zeal.Bootcamp.DnD.Domain.Core;

namespace Zeal.Bootcamp.DnD.Domain.Items;

/// <summary>
/// A catalog item is identified by its type and name. Individual copies receive identity
/// only after they are placed in an inventory.
/// </summary>
public abstract class Item(string name) : ValueObject<Item>
{
    public string Name { get; } = ValidateName(name);

    protected override bool EqualsCore(Item other) =>
        GetType() == other.GetType() && Name == other.Name;

    protected override int GetHashCodeCore() => HashCode.Combine(GetType(), Name);

    public override string ToString() => Name;

    private static string ValidateName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("An item must have a name.");

        return value.Trim();
    }
}
