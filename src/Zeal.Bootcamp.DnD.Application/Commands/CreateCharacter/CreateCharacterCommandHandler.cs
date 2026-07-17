using MediatR;
using Zeal.Bootcamp.DnD.Application.Data.Repositories;
using Zeal.Bootcamp.DnD.Application.Services.Catalogs;
using Zeal.Bootcamp.DnD.Domain.Characters;
using Zeal.Bootcamp.DnD.Domain.Weapons;

namespace Zeal.Bootcamp.DnD.Application.Commands.CreateCharacter;

internal sealed class CreateCharacterCommandHandler(
    ICharacterRepository characterRepository,
    IClassCatalog classCatalog,
    IWeaponCatalog weaponCatalog)
    : IRequestHandler<CreateCharacterCommand, Guid>
{
    public async Task<Guid> Handle(
        CreateCharacterCommand request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Class characterClass = classCatalog.Find(request.ClassName);
        Weapon weapon = weaponCatalog.Find(request.Weapon);
        Character character = Character.Create(
            request.Name,
            characterClass,
            startingWeapons: [weapon]);

        character.Equip(weapon);

        cancellationToken.ThrowIfCancellationRequested();
        await characterRepository.Save(character);

        return character.Id;
    }
}
