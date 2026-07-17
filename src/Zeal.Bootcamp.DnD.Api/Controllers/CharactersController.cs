using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zeal.Bootcamp.DnD.Application.Commands.CreateCharacter;
using Zeal.Bootcamp.DnD.Api.Contracts.Requests;
using Zeal.Bootcamp.DnD.Api.Contracts.Responses;

namespace Zeal.Bootcamp.DnD.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CharactersController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateCharacterResponse>> CreateCharacter(
        [FromBody] CreateCharacterRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCharacterCommand
        {
            Name = request.Name,
            ClassName = request.ClassName,
            Weapon = request.Weapon,
        };

        Guid characterId = await mediator.Send(command, cancellationToken);
        var response = new CreateCharacterResponse { Id = characterId };

        return Created($"api/characters/{characterId}", response);
    }
}
