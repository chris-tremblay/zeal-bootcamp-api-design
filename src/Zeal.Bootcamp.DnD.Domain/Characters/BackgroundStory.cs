using Zeal.Bootcamp.DnD.Domain.Core;

namespace Zeal.Bootcamp.DnD.Domain.Characters;

public sealed class BackgroundStory 
    : ValueObject<BackgroundStory>
{
    public static readonly BackgroundStory Empty = new(string.Empty);

    public BackgroundStory(string text)
    {
        text = text?.Trim() ?? string.Empty;

        if (text.Length > 2_000)
            throw new DomainException("A background story cannot exceed 2,000 characters.");

        Text = text;
    }

    public string Text { get; }

    public override string ToString() => Text;

    protected override bool EqualsCore(BackgroundStory other) => Text == other.Text;

    protected override int GetHashCodeCore() => Text.GetHashCode(StringComparison.Ordinal);
}