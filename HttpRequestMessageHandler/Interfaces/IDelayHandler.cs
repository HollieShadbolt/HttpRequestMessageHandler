namespace HttpRequestMessageHandler.Interfaces;

/// <summary>
/// A <see cref="IDelayHandler.Delay"/> handler.
/// </summary>
public interface IDelayHandler
{
    /// <summary>
    /// Creates a cancellable task that completes after a specified number of milliseconds.
    /// </summary>
    /// <param name="millisecondsDelay">The number of milliseconds to wait before completing the returned task.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    Task Delay(int millisecondsDelay, CancellationToken cancellationToken);
}