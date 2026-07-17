using Zeal.Bootcamp.DnD.Domain.Core;

namespace Zeal.Bootcamp.DnD.Domain.Characters;

public sealed class CharacterName : ValueObject<CharacterName>
{
    public CharacterName(string value)
    {
        value = value?.Trim() ?? string.Empty;
        if (value.Length is < 2 or > 50)
            throw new DomainException("A character name must be between 2 and 50 characters.");
        Value = value;
    }

    public string Value { get; }
    public static implicit operator string(CharacterName name) => name.Value;
    public override string ToString() => Value;
    protected override bool EqualsCore(CharacterName other) => Value == other.Value;
    protected override int GetHashCodeCore() => Value.GetHashCode(StringComparison.Ordinal);
}
