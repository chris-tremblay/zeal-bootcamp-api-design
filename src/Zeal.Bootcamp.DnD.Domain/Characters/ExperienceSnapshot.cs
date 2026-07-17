namespace Zeal.Bootcamp.DnD.Domain.Characters;

/// <summary>An immutable view of a character's progression.</summary>
public sealed record ExperienceSnapshot(Guid Id, int Points, int Level);
