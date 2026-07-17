namespace Zeal.Bootcamp.DnD.Domain.Characters;

/// <summary>An immutable view of a character's inventory.</summary>
public sealed record InventorySnapshot(Guid Id, IReadOnlyCollection<InventoryItemSnapshot> Items);
