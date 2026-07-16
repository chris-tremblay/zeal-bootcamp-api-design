using System.Collections.Concurrent;

namespace Zeal.Bootcamp.DnD.Application;

public class DeferredNotificationsManager
{
    // Store a queue of deferred task functions
    private static readonly AsyncLocal<ConcurrentQueue<Func<Task>>> Handlers = new();

    public static void EnqueueDeferredNotification(Func<Task> action)
    {
        // Initialize queue if it doesn't exist
        if (Handlers.Value == null)
            Handlers.Value = new ConcurrentQueue<Func<Task>>();

        Handlers.Value.Enqueue(action);
    }

    public static async Task HandleNotifications()
    {
        List<Task> tasks = new();

        // Execute all handlers
        while (Handlers.Value?.Count > 0)
        {
            if (Handlers.Value.TryDequeue(out Func<Task>? handler))
                tasks.Add(handler()); // Execute the handler function and collect its task
        }

        // Wait for all tasks to complete
        await Task.WhenAll(tasks);
    }

    public static void Init()
    {
        if (Handlers.Value == null)
            Handlers.Value = new();
    }
}