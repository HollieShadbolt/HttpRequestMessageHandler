using System.Net.Http.Json;

namespace HttpRequestMessageHandler;

/// <summary>
/// <see cref="HttpResponseMessage"/> handlers.
/// </summary>
public static class HttpResponseMessageHandlers
{
    /// <summary>
    /// Ensure <see cref="HttpResponseMessage"/> was successful.
    /// </summary>
    /// <param name="httpResponseMessage">The <see cref="HttpResponseMessage"/>.</param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <exception cref="HttpRequestException">The HTTP response is unsuccessful.</exception>
    public static Task<HttpContent> HandleAsync(
        HttpResponseMessage httpResponseMessage,
        CancellationToken cancellationToken)
    {
        var result = Verify(httpResponseMessage);

        return Task.FromResult(result);
    }

    /// <summary>
    /// Ensure <see cref="HttpResponseMessage"/> was successful and deserialize the <see cref="HttpContent"/>.
    /// </summary>
    /// <param name="httpResponseMessage">The <see cref="HttpResponseMessage"/>.</param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation.</param>
    /// <typeparam name="T">The target type to deserialize to.</typeparam>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <exception cref="HttpRequestException">The HTTP response is unsuccessful.</exception>
    /// <exception cref="System.Text.Json.JsonException">Deserialization was unsuccessful.</exception>
    /// <exception cref="InvalidOperationException">Deserialization was unsuccessful.</exception>
    public static async Task<T> HandleAsync<T>(
        HttpResponseMessage httpResponseMessage,
        CancellationToken cancellationToken)
    {
        var httpContent = Verify(httpResponseMessage);

        return await httpContent.ReadFromJsonAsync<T>(cancellationToken) ?? throw new InvalidOperationException();
    }

    private static HttpContent Verify(HttpResponseMessage httpResponseMessage)
    {
        httpResponseMessage.EnsureSuccessStatusCode();

        return httpResponseMessage.Content;
    }
}