using System.Diagnostics.CodeAnalysis;
using MediatR;
using Zeal.Bootcamp.DnD.Domain.Core;

namespace Zeal.Bootcamp.DnD.Application;

public class Notification : INotification
{
    public static Notification<T> Create<T>(T @event)
        where T : IDomainEvent
        => new Notification<T>(@event);
}

[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Generic Implementation")]
public class Notification<T> : Notification
{
    public Notification(T detail)
    {
        Detail = detail;
    }

    public T Detail { get; }
}