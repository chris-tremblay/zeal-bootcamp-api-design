namespace Authorizations.Application.Notifications.Attributes;

/// <summary>
///   Notifies the <see cref="NotificationHandlerBase{TNotification}"/> that notifications routed to this handler should
///   be handled as a single batch.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class BatchNotificationsAttribute : Attribute;