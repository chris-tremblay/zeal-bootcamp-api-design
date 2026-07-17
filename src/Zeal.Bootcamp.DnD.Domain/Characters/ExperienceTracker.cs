using Zeal.Bootcamp.DnD.Domain.Core;

namespace Zeal.Bootcamp.DnD.Domain.Characters;

/// <summary>
///   A child entity whose identity and changing experience matter over time.
/// </summary>
public sealed class ExperienceTracker : Entity<Guid>
{
    // Simplified thresholds keep the example readable while retaining D&D's 20-level shape.
    private static readonly int[] Thresholds =
        [0, 300, 900, 2_700, 6_500, 14_000, 23_000, 34_000, 48_000, 64_000,
         85_000, 100_000, 120_000, 140_000, 165_000, 195_000, 225_000, 265_000, 305_000, 355_000];

    internal ExperienceTracker(Guid id) : base(id) { }

    public int Level => Array.FindLastIndex(Thresholds, threshold => Points >= threshold) + 1;

    public int Points { get; private set; }

    internal void Add(int points)
    {
        if (points <= 0)
            throw new DomainException("Experience awarded must be greater than zero.");
        Points = checked(Points + points);
    }

    internal ExperienceSnapshot ToSnapshot() => new(Id, Points, Level);
}
