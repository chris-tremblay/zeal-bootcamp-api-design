using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Zeal.Bootcamp.DnD.Application.Data.Queries.ListCharacters;
using Zeal.Bootcamp.DnD.Application.Queries.ListCharacters;

namespace Zeal.Bootcamp.DnD.Api.Controllers;

// Week 4
[Route("odata")]
public class ODataController(IMediator mediator)
    : Microsoft.AspNetCore.OData.Routing.Controllers.ODataController
{
    [HttpGet("characters")]
    [EnableQuery]
    public async Task<IQueryable<ListCharactersDataQueryResult>> GetCharacters(
        CancellationToken cancellationToken)
        => await mediator.Send(new ListCharactersQuery(), cancellationToken);
}
