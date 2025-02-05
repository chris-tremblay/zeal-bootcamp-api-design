namespace Zeal.Bootcamp.DnD.Data.Entities;

internal class CharacterEntity
{
    public string Class { get; set; }

    public Guid CharacterId { get; set; }

    public string Name { get; set; }

    public string Weapon { get; set; }
}