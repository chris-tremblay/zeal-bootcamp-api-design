using Zeal.Bootcamp.DnD.Domain.Core;
using Zeal.Bootcamp.DnD.Domain.Items;

namespace Zeal.Bootcamp.DnD.Domain.Characters;

public sealed class Inventory : Entity<Guid>
{
    private readonly List<InventoryItem> _items = [];

    internal Inventory(Guid id) : base(id) { }

    internal IEnumerable<InventoryItem> Items => _items;

    internal InventoryItem Add(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);
        var inventoryItem = new InventoryItem(Guid.NewGuid(), item);
        _items.Add(inventoryItem);
        return inventoryItem;
    }

    internal InventoryItem Get(Guid itemId) => _items.SingleOrDefault(i => i.Id == itemId)
        ?? throw new DomainException("The inventory item does not belong to this character.");

    internal InventorySnapshot ToSnapshot() =>
        new(Id, _items.Select(item => item.ToSnapshot()).ToArray());
}
