using Zeal.Bootcamp.DnD.Domain.Characters;

namespace Zeal.Bootcamp.DnD.Application.Services.Catalogs;

public interface IClassCatalog
{
    Class Find(string name);
}
