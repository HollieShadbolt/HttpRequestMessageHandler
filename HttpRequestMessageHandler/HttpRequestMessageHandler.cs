namespace HttpRequestMessageHandler;

/// <inheritdoc/>
public sealed class HttpRequestMessageHandler : Interfaces.IHttpRequestMessageHandler
{
    private readonly HttpClient _httpClient = new();

    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage,
        CancellationToken cancellationToken) => _httpClient.SendAsync(httpRequestMessage, cancellationToken);

    public void ExceptionHandler(Exception exception) => Console.WriteLine(exception);
}