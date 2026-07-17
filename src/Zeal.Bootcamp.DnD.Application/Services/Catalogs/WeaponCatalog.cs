using Zeal.Bootcamp.DnD.Domain.Extensions;
using Zeal.Bootcamp.DnD.Domain.Weapons;

namespace Zeal.Bootcamp.DnD.Application.Services.Catalogs;

internal sealed class WeaponCatalog : IWeaponCatalog
{
    public Weapon Find(string name)
        => typeof(Weapon).GetStaticField<Weapon>(name);
}
