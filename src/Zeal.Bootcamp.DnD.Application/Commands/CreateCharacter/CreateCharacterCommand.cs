namespace Zeal.Bootcamp.DnD.Application.Commands.CreateCharacter;

public sealed class CreateCharacterCommand : CommandBase<Guid>
{
    public required string Name { get; init; }

    public required string ClassName { get; init; }

    public required string Weapon { get; init; }
}
