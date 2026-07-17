namespace Zeal.Bootcamp.DnD.Api.Contracts.Requests;

public sealed class CreateCharacterRequest
{
    public required string Name { get; init; }

    public required string ClassName { get; init; }

    public required string Weapon { get; init; }
}
