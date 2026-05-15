using JsonContent = System.Net.Http.Json.JsonContent;

namespace HttpRequestMessageHandler.Factories;

/// <summary>
/// A <see cref="HttpRequestMessage"/> factory.
/// </summary>
/// <param name="scheme">The scheme to use for authorization.</param>
/// <param name="parameter">The credentials containing the authentication information of the user agent for the resource being requested.</param>
public sealed class HttpRequestMessageFactory(string scheme, string parameter)
{
    /// <summary>
    /// Get the scheme to use for authorization.
    /// </summary>
    /// <returns>The scheme to use for authorization.</returns>
    public string Scheme => scheme;

    /// <summary>
    /// Get the credentials containing the authentication information of the user agent for the resource being requested.
    /// </summary>
    /// <returns>The credentials containing the authentication information of the user agent for the resource being requested.</returns>
    public string Parameter => parameter;

    /// <summary>
    /// Get a <see cref="HttpRequestMessage"/>.
    /// </summary>
    /// <param name="uriBuilder">The <see cref="UriBuilder"/>.</param>
    /// <param name="httpMethod">The HTTP method.</param>
    /// <returns>The <see cref="HttpRequestMessage"/>.</returns>
    public HttpRequestMessage GetHttpRequestMessage(UriBuilder uriBuilder, HttpMethod httpMethod)
    {
        var authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(scheme, parameter);

        return new HttpRequestMessage(httpMethod, uriBuilder.Uri)
        {
            Headers =
            {
                Authorization = authorization
            }
        };
    }

    /// <summary>
    /// Get a <see cref="HttpRequestMessage"/>.
    /// </summary>
    /// <param name="uriBuilder">The <see cref="UriBuilder"/>.</param>
    /// <returns>The <see cref="HttpRequestMessage"/>.</returns>
    public HttpRequestMessage GetHttpRequestMessage(UriBuilder uriBuilder) =>
        GetHttpRequestMessage(uriBuilder, HttpMethod.Get);

    /// <summary>
    /// Get a <see cref="HttpRequestMessage"/>.
    /// </summary>
    /// <param name="uriBuilder">The <see cref="UriBuilder"/>.</param>
    /// <param name="httpMethod">The HTTP method.</param>
    /// <param name="inputValue">The value to serialize to <see cref="JsonContent"/>.</param>
    /// <returns>The <see cref="HttpRequestMessage"/>.</returns>
    public HttpRequestMessage GetHttpRequestMessage(UriBuilder uriBuilder, HttpMethod httpMethod, object inputValue)
    {
        var httpRequestMessage = GetHttpRequestMessage(uriBuilder, httpMethod);

        httpRequestMessage.Content = JsonContent.Create(inputValue);

        return httpRequestMessage;
    }
}