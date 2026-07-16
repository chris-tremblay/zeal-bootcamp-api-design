using Zeal.Bootcamp.DnD.Domain.Core;

namespace Zeal.Bootcamp.DnD.Domain.Extensions;

public static class ValidationExceptionExtensions
{
    public static DomainException IsInvalidBecause<T>(this T resource, string reason)
        => new DomainException(reason);

    public static DomainException IsInvalidBecause(this Type resource, string reason)
        => new DomainException(reason);
}