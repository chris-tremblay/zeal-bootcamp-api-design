using Zeal.Bootcamp.DnD.Domain.Core;

namespace Zeal.Bootcamp.DnD.Domain.Characters.Events;

public sealed class ExperienceAwarded(
    Character source,
    int points,
    int previousLevel,
    int currentLevel) : DomainEvent<Character>(source)
{
    public int Points { get; } = points;
    public int PreviousLevel { get; } = previousLevel;
    public int CurrentLevel { get; } = currentLevel;
    public bool LeveledUp => CurrentLevel > PreviousLevel;
}
