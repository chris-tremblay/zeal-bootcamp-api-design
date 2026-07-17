using Zeal.Bootcamp.DnD.Domain.Characters;
using Zeal.Bootcamp.DnD.Domain.Extensions;

namespace Zeal.Bootcamp.DnD.Application.Services.Catalogs;

internal sealed class ClassCatalog : IClassCatalog
{
    public Class Find(string name)
        => typeof(Class).GetStaticField<Class>(name);
}
