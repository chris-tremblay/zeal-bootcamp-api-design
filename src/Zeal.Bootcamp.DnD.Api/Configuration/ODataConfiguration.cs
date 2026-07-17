using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Zeal.Bootcamp.DnD.Application.Data.Queries.ListCharacters;

namespace Zeal.Bootcamp.DnD.Api.Configuration;

// Week 4
public static class ODataConfiguration
{
    public static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();

        EntitySetConfiguration<ListCharactersDataQueryResult> characters =
            builder.EntitySet<ListCharactersDataQueryResult>("characters");
        characters.EntityType.HasKey(character => character.Id);

        return builder.GetEdmModel();
    }
}
