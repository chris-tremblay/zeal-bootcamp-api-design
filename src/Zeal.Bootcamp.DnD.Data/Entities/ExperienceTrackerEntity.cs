namespace Zeal.Bootcamp.DnD.Data.Entities;

internal sealed class ExperienceTrackerEntity
{
    public CharacterEntity Character { get; set; } = null!;

    public Guid CharacterId { get; set; }

    public Guid ExperienceTrackerId { get; set; }

    public int Points { get; set; }
}