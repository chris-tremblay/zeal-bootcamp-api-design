namespace Zeal.Bootcamp.DnD.Application.UnitOfWorks;

/// <summary>
///   Represents a unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///   Commits a unit of work to the database and dispatches domain events.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>Returns a <see cref="Task"/> representing the asynchronous task.</returns>
    Task Commit(CancellationToken cancellationToken = default);
}