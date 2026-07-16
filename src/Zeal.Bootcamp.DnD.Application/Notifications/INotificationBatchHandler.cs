using MediatR;

namespace Zeal.Bootcamp.DnD.Application;

public interface INotificationBatchHandler<T>
{
    Task HandleBatch(IEnumerable<INotification> notifications);

    Task HandleBatch(IEnumerable<Notification<T>> notifications);
}