using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Zeal.Bootcamp.DnD.Application.Data.Queries.ListCharacters;

namespace Zeal.Bootcamp.DnD.Api.Controllers;

// Week 4
[Route("odata")]
public class ODataController(IListCharactersDataQuery query) : Controller
{
    [HttpGet("characters")]
    [EnableQuery]
    public IQueryable<ListCharactersDataQueryResult> GetCharacters()
        => query.Execute();
}
