using Zeal.Bootcamp.DnD.Application.Data;
using Zeal.Bootcamp.DnD.Domain.Core;

namespace Zeal.Bootcamp.DnD.Application.UnitOfWorks;

internal class UnitOfWork(
    IDataStore dataStore,
    IDomainEventDispatcher dispatcher,
    IDomainEventsStorage domainEventsStorage)
    : IUnitOfWork
{
    public async Task Commit(CancellationToken cancellationToken = default)
    {
        while (domainEventsStorage.HasItems)
        {
            // Handle and dispatch collected domain events
            while (domainEventsStorage.TryDequeue(out IDomainEvent? domainEvent))
                await dispatcher.Dispatch(domainEvent);

            await NotificationBatchManager.DispatchPreTransactionNotifications();
        }

        await dataStore.SaveChanges();

        await DeferredNotificationsManager.HandleNotifications();
        await NotificationBatchManager.DispatchPostTransactionNotifications();
    }
}