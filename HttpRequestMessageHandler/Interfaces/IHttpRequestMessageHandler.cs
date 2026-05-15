namespace HttpRequestMessageHandler.Interfaces;

/// <summary>
/// A <see cref="HttpRequestMessage"/> handler.
/// </summary>
public interface IHttpRequestMessageHandler
{
    /// <summary>
    /// Send an HTTP request as an asynchronous operation.
    /// </summary>
    /// <param name="httpRequestMessage">The HTTP request message to send.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken);

    /// <summary>
    /// Handles an exception.
    /// </summary>
    /// <param name="exception">The <see cref="Exception"/>.</param>
    void ExceptionHandler(Exception exception);
}