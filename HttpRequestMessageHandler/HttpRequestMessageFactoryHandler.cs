using HttpRequestMessageHandler.Interfaces;
using static HttpRequestMessageHandler.HttpResponseMessageHandlers;

namespace HttpRequestMessageHandler;

/// <inheritdoc/>
/// <param name="httpRequestMessageHandler">The <see cref="IHttpRequestMessageHandler"/>.</param>
/// <param name="delayHandler">The <see cref="IDelayHandler"/>.</param>
public sealed class HttpRequestMessageFactoryHandler(
    IHttpRequestMessageHandler httpRequestMessageHandler,
    IDelayHandler delayHandler) : IHttpRequestMessageFactoryHandler
{
    /// <inheritdoc/>
    /// <exception cref="TaskCanceledException">The cancellation token was cancelled.</exception>
    public async Task SendAsync(
        Func<HttpRequestMessage> httpRequestMessageFactory,
        CancellationToken cancellationToken) => await SendAsync(httpRequestMessageFactory, HandleAsync, cancellationToken);

    /// <inheritdoc/>
    /// <exception cref="TaskCanceledException">The cancellation token was cancelled.</exception>
    public async Task<T> SendAsync<T>(
        Func<HttpRequestMessage> httpRequestMessageFactory,
        CancellationToken cancellationToken) =>
        await SendAsync(httpRequestMessageFactory, HandleAsync<T>, cancellationToken);

    /// <inheritdoc/>
    /// <exception cref="TaskCanceledException">The cancellation token was cancelled.</exception>
    public async Task<T> SendAsync<T>(
        Func<HttpRequestMessage> httpRequestMessageFactory,
        Func<HttpResponseMessage, CancellationToken, Task<T>> httpResponseMessageHandler,
        CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            const int millisecondsDelay = 1_000;

            await delayHandler.Delay(millisecondsDelay, cancellationToken);

            var httpRequestMessage = httpRequestMessageFactory();

            try
            {
                var httpResponseMessage = await GetHttpResponseMessageAsync(httpRequestMessage, cancellationToken);

                return await httpResponseMessageHandler(httpResponseMessage, cancellationToken);
            }
            catch (Exception exception)
            {
                httpRequestMessageHandler.ExceptionHandler(exception);
            }
        }

        throw new TaskCanceledException();
    }

    private async Task<HttpResponseMessage> GetHttpResponseMessageAsync(
        HttpRequestMessage httpRequestMessage,
        CancellationToken cancellationToken) =>
        await httpRequestMessageHandler.SendAsync(httpRequestMessage, cancellationToken);
}