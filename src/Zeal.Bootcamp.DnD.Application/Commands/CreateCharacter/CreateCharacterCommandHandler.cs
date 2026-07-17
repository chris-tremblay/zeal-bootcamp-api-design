using MediatR;
using Zeal.Bootcamp.DnD.Application.Data.Repositories;
using Zeal.Bootcamp.DnD.Domain.Characters;
using Zeal.Bootcamp.DnD.Domain.Extensions;
using Zeal.Bootcamp.DnD.Domain.Weapons;

namespace Zeal.Bootcamp.DnD.Application.Commands.CreateCharacter;

internal sealed class CreateCharacterCommandHandler(ICharacterRepository characterRepository)
    : IRequestHandler<CreateCharacterCommand, Guid>
{
    public async Task<Guid> Handle(
        CreateCharacterCommand request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Class characterClass = typeof(Class).GetStaticField<Class>(request.ClassName);
        Weapon weapon = typeof(Weapon).GetStaticField<Weapon>(request.Weapon);
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
