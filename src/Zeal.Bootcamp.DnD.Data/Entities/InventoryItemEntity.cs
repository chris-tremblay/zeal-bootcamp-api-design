namespace Zeal.Bootcamp.DnD.Data.Entities;

internal sealed class InventoryItemEntity
{
    public int? DamageDieSides { get; set; }

    public int? DamageModifier { get; set; }

    public InventoryEntity Inventory { get; set; } = null!;

    public Guid InventoryId { get; set; }

    public Guid InventoryItemId { get; set; }

    public string ItemType { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? WeaponProficiency { get; set; }
}