namespace Authorizations.Application.Notifications.Attributes;

/// <summary>
///   Notifies the <see cref="NotificationHandlerBase{TNotification}"/> that the execution of the handler should be
///   deferred until the transation is completed.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class DeferExecutionUntilUnitOfWorkIsCompleteAttribute : Attribute;