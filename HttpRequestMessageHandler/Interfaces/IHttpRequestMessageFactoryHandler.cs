namespace HttpRequestMessageHandler.Interfaces;

/// <summary>
/// A <see cref="Func&lt;HttpRequestMessage&gt;"/> handler.
/// </summary>
public interface IHttpRequestMessageFactoryHandler
{
    /// <summary>
    /// Send an HTTP request as an asynchronous operation until a successful <see cref="HttpResponseMessage"/> is returned.
    /// </summary>
    /// <param name="httpRequestMessageFactory">A factory for the HTTP request message to send.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    Task SendAsync(Func<HttpRequestMessage> httpRequestMessageFactory, CancellationToken cancellationToken);

    /// <summary>
    /// Send an HTTP request as an asynchronous operation until a successful <see cref="HttpResponseMessage"/> is returned and deserialized.
    /// </summary>
    /// <param name="httpRequestMessageFactory">A factory for the HTTP request message to send.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <typeparam name="T">The target type to deserialize to.</typeparam>
    /// <returns>The task object representing the asynchronous operation.</returns>
    Task<T> SendAsync<T>(Func<HttpRequestMessage> httpRequestMessageFactory, CancellationToken cancellationToken);

    /// <summary>
    /// Send an HTTP request as an asynchronous operation.
    /// </summary>
    /// <param name="httpRequestMessageFactory">A factory for the HTTP request message to send.</param>
    /// <param name="httpResponseMessageHandler">The <see cref="HttpResponseMessage"/> handler. For examples, see <see cref="HttpResponseMessageHandlers"/>.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <typeparam name="T">The target type to deserialize to.</typeparam>
    /// <returns>The task object representing the asynchronous operation.</returns>
    Task<T> SendAsync<T>(
        Func<HttpRequestMessage> httpRequestMessageFactory,
        Func<HttpResponseMessage, CancellationToken, Task<T>> httpResponseMessageHandler,
        CancellationToken cancellationToken);
}