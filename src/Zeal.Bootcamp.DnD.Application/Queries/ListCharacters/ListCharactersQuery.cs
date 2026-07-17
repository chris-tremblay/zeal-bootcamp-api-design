using Zeal.Bootcamp.DnD.Application.Data.Queries.ListCharacters;

namespace Zeal.Bootcamp.DnD.Application.Queries.ListCharacters;

public sealed record ListCharactersQuery : QueryBase<IQueryable<ListCharactersDataQueryResult>>;
