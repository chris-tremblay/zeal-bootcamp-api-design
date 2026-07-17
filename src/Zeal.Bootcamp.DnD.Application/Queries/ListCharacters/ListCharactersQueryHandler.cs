using MediatR;
using Zeal.Bootcamp.DnD.Application.Data.Queries.ListCharacters;

namespace Zeal.Bootcamp.DnD.Application.Queries.ListCharacters;

internal sealed class ListCharactersQueryHandler(IListCharactersDataQuery query)
    : IRequestHandler<ListCharactersQuery, IQueryable<ListCharactersDataQueryResult>>
{
    public Task<IQueryable<ListCharactersDataQueryResult>> Handle(
        ListCharactersQuery request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(query.Execute());
    }
}
