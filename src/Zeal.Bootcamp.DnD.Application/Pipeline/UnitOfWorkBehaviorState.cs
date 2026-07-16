namespace Zeal.Bootcamp.DnD.Application.Pipeline;

internal class UnitOfWorkBehaviorState
{
    /// <summary>
    /// Gets incremented for each Command that is being processed and decremented back after the command has been processed.
    /// </summary>
    public int CallLevel { get; set; }
}