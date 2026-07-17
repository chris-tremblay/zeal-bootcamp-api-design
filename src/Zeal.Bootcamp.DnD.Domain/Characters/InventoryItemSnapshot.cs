namespace Zeal.Bootcamp.DnD.Domain.Characters;

/// <summary>An immutable view of an item held in a character's inventory.</summary>
public sealed record InventoryItemSnapshot(Guid Id, string Name, string ItemType);
