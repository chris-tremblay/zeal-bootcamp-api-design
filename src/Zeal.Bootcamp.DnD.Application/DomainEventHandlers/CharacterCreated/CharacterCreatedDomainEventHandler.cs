using Authorizations.Application.Notifications;
using Microsoft.Extensions.Logging;
using Zeal.Bootcamp.DnD.Domain.Characters.Events;

namespace Zeal.Bootcamp.DnD.Application.DomainEventHandlers.CharacterCreated;

public sealed class CharacterCreatedDomainEventHandler(
    ILogger<CharacterCreatedDomainEventHandler> logger)
    : NotificationHandlerBase<CharacterCreatedDomainEvent>
{
    protected internal override Task HandleEvent(
        CharacterCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        using (logger.BeginScope(new Dictionary<string, object>
        {
            ["CharacterID"] = notification.Source.Id,
        }))
        {
            logger.LogInformation("Character created.");
        }

        return Task.CompletedTask;
    }
}
