using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Zeal.Bootcamp.DnD.Application.Data.Queries.ListCharacters;

namespace Zeal.Bootcamp.DnD.Api.COnfiguration;

// Week 4
public static class ODataConfiguration
{
    public static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();

        EntitySetConfiguration<ListCharactersDataQueryResult> formDetailSet = builder.EntitySet<ListCharactersDataQueryResult>("forms");
        formDetailSet.EntityType.HasKey(r => r.Id);
       

        return builder.GetEdmModel();
    }
}