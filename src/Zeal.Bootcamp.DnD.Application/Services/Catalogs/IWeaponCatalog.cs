using Zeal.Bootcamp.DnD.Domain.Weapons;

namespace Zeal.Bootcamp.DnD.Application.Services.Catalogs;

public interface IWeaponCatalog
{
    Weapon Find(string name);
}
