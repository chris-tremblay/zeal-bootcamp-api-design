using Zeal.Bootcamp.DnD.Domain.Core;
using Zeal.Bootcamp.DnD.Domain.Items;

namespace Zeal.Bootcamp.DnD.Domain.Characters;

/// <summary>
/// An entity representing one particular item owned by the character. It deliberately
/// depends on Item rather than Weapon so any inventory-compatible item can be stored.
/// </summary>
public sealed class InventoryItem : Entity<Guid>
{
    internal InventoryItem(Guid id, Item item) : base(id) => Item = item;

    internal Item Item { get; }

    internal InventoryItemSnapshot ToSnapshot() => new(Id, Item.Name, Item.GetType().Name);
}
