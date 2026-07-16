using System.Reflection;
using Authorizations.Application.Notifications.Attributes;
using MediatR;
using Zeal.Bootcamp.DnD.Application;

namespace Authorizations.Application.Notifications;

public abstract class NotificationHandlerBase<TEvent> : INotificationHandler<Notification<TEvent>>
{
    public Task Handle(Notification<TEvent> notification, CancellationToken cancellationToken)
    {
        Type handlerType = GetType();
        bool deferNotification = handlerType.GetCustomAttribute<DeferExecutionUntilUnitOfWorkIsCompleteAttribute>() is not null;
        bool batchNotification = handlerType.GetCustomAttribute<BatchNotificationsAttribute>() is not null;

        if (deferNotification)
        {
            if (batchNotification && this is INotificationBatchHandler<TEvent> handler)
            {
                NotificationBatchManager.EnqueuePostTransactionNotification(notification, handler);
                return Task.CompletedTask;
            }
            else
            {
                DeferredNotificationsManager.EnqueueDeferredNotification(() => HandleEvent(notification.Detail, cancellationToken));
                return Task.CompletedTask;
            }
        }
        else
        {
            if (batchNotification && this is INotificationBatchHandler<TEvent> handler)
            {
                NotificationBatchManager.EnqueuePreTransactionNotification(notification, handler);
                return Task.CompletedTask;
            }

            return HandleEvent(notification.Detail, cancellationToken);
        }
    }

    protected internal virtual Task HandleEvent(TEvent notification, CancellationToken cancellationToken)
        => Task.CompletedTask;
}