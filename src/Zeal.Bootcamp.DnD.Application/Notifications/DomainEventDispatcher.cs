using MediatR;
using Zeal.Bootcamp.DnD.Domain.Core;

namespace Zeal.Bootcamp.DnD.Application;

public class DomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
{
    private static readonly Type NotificationType = typeof(Notification<>);

    public Task Dispatch(IDomainEvent e)
    {
        object? notification = CreateNotification(e);

        return notification != null
            ? mediator.Publish(notification)
            : Task.CompletedTask;
    }

    public Task Dispatch<T>(IDomainEvent<T> domainEvent)
        where T : IAggregateRoot
        => mediator.Publish(new Notification<IDomainEvent<T>>(domainEvent));

    private static object? CreateNotification(object content)
    {
        Type contentType = content.GetType();
        Type constructedType = NotificationType.MakeGenericType(contentType);

        object? notificationInstance = Activator.CreateInstance(constructedType, content);

        return notificationInstance;
    }
}