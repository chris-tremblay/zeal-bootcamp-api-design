using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using Zeal.Bootcamp.DnD.Application.Commands;
using Zeal.Bootcamp.DnD.Application.UnitOfWorks;

namespace Zeal.Bootcamp.DnD.Application.Pipeline;

/// <summary>
///   A behavior used to automatically enlist commands in a unit of work.
/// </summary>
/// <typeparam name="TRequest">The type of request.</typeparam>
/// <typeparam name="TResponse">The type of response.</typeparam>
internal class UnitOfWorkBehavior<TRequest, TResponse>(
    IServiceProvider serviceProvider,
    IUnitOfWork unitOfWork,
    UnitOfWorkBehaviorState state,
    ILogger<UnitOfWorkBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <inheritdoc/>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogTrace("{RequestBody}", request.ToString());

        if (request is CommandBase)
            return await ProcessCommandHandler(next, cancellationToken);

        return await next();
    }

    private async Task Commit(CancellationToken cancellationToken)
    {
        logger.LogTrace("{Request}, Commit()", TypeNameHelper.GetTypeDisplayName(typeof(TRequest), false));

        await unitOfWork.Commit(cancellationToken);
    }

    private async Task<TResponse> ProcessCommandHandler(RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogTrace("{Request}, CallLevel={CallLevel}", TypeNameHelper.GetTypeDisplayName(typeof(TRequest), false), state.CallLevel);

        // Only when we enter the UnitOfWorkBehavior first time during HTTP request processing, we initialize the
        // notification managers.
        if (state.CallLevel == 0)
        {
            NotificationBatchManager.Init(serviceProvider);
            DeferredNotificationsManager.Init();
        }

        state.CallLevel += 1;

        TResponse response = await next();

        // Once we got back to the top level of the call stack, we commit the unit of work.
        if (state.CallLevel == 1)
            await Commit(cancellationToken);

        state.CallLevel -= 1;

        return response;
    }
}