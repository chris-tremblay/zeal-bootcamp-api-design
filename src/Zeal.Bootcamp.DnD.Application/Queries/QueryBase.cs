using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace Zeal.Bootcamp.DnD.Application.Queries;

/// <summary>
///   An abstract base query class that provides implementations of various required interfaces.
/// </summary>
public abstract record QueryBase;

/// <summary>
///   An abstract base query class that provides implementations of various required interfaces.
/// </summary>
/// <typeparam name="TResponse">The response type returned by the query.</typeparam>
[SuppressMessage("Maintainability Rules", "SA1402", Justification = "Related class.")]
public abstract record QueryBase<TResponse> : QueryBase, IRequest<TResponse>;