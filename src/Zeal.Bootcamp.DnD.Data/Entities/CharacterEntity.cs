namespace Zeal.Bootcamp.DnD.Data.Entities;

internal sealed class CharacterEntity
{
    public string BackgroundStory { get; set; } = string.Empty;

    public Guid CharacterId { get; set; }

    public string Class { get; set; } = string.Empty;

    public InventoryItemEntity? EquippedWeaponItem { get; set; }

    public Guid? EquippedWeaponItemId { get; set; }

    public ExperienceTrackerEntity ExperienceTracker { get; set; } = null!;

    public InventoryEntity Inventory { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
}