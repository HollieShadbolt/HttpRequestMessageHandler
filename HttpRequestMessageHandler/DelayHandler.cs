namespace HttpRequestMessageHandler;

public class DelayHandler : Interfaces.IDelayHandler
{
    /// <inheritdoc/>
    /// <exception cref="TaskCanceledException">The cancellation token was cancelled.</exception>
    public async Task Delay(int millisecondsDelay, CancellationToken cancellationToken) =>
        await Task.Delay(millisecondsDelay, cancellationToken);
}