using Microsoft.EntityFrameworkCore;
using Zeal.Bootcamp.DnD.Application.Data.Queries.ListCharacters;
using Zeal.Bootcamp.DnD.Data.Entities;

namespace Zeal.Bootcamp.DnD.Data.Queries;

// Week 4
internal class ListCharactersDataQuery(DnDContext db) : IListCharactersDataQuery
{
    public IQueryable<ListCharactersDataQueryResult> Execute()
        => db.Set<CharacterEntity>().AsNoTracking().Select(i => new ListCharactersDataQueryResult
        {
            ClassName = i.Class,
            Name = i.Name,
            WeaponName = i.EquippedWeaponItem == null ? "Hands" : i.EquippedWeaponItem.Name,
        });
}
