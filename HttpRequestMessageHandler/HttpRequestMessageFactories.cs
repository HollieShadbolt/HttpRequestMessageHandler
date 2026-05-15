using HttpRequestMessageHandler.Factories;

namespace HttpRequestMessageHandler;

/// <summary>
/// A combination of <see cref="HttpRequestMessageFactory"/> and <see cref="UriBuilderFactory"/>.
/// </summary>
/// <param name="scheme">The scheme to use for authorization.</param>
/// <param name="parameter">The credentials containing the authentication information of the user agent for the resource being requested.</param>
/// <param name="uri">A URI string.</param>
public class HttpRequestMessageFactories(string scheme, string parameter, string uri)
{
    /// <summary>
    /// Gets the <see cref="HttpRequestMessageFactory"/>.
    /// </summary>
    /// <returns>The <see cref="HttpRequestMessageFactory"/>.</returns>
    public readonly HttpRequestMessageFactory HttpRequestMessageFactory = new(scheme, parameter);

    /// <summary>
    /// Gets the <see cref="UriBuilderFactory"/>.
    /// </summary>
    /// <returns>The <see cref="UriBuilderFactory"/>.</returns>
    public readonly UriBuilderFactory UriBuilderFactory = new(uri);
}