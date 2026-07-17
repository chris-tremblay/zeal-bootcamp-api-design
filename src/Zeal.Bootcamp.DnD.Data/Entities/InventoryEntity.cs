namespace Zeal.Bootcamp.DnD.Data.Entities;

internal sealed class InventoryEntity
{
    public CharacterEntity Character { get; set; } = null!;

    public Guid CharacterId { get; set; }

    public Guid InventoryId { get; set; }

    public ICollection<InventoryItemEntity> Items { get; set; } = [];
}