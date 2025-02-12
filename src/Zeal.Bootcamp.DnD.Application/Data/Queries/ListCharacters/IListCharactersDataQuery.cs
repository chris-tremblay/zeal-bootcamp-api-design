using Zeal.Bootcamp.DnD.Application.Data.Queries.ListCharacters;

namespace Zeal.Bootcamp.DnD.Application.Data.Queries.ListCharacters;

// Week 4
public interface IListCharactersDataQuery
{
    IQueryable<ListCharactersDataQueryResult> Execute();
}