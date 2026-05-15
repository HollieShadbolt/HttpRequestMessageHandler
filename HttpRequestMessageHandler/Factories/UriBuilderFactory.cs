namespace HttpRequestMessageHandler.Factories;

/// <summary>
/// A <see cref="UriBuilder"/> factory.
/// </summary>
/// <param name="uri">The URI string.</param>
public sealed class UriBuilderFactory(string uri)
{
    /// <summary>
    /// Get the URI string.
    /// </summary>
    /// <returns>The URI string.</returns>
    public string Uri => uri;

    /// <summary>
    /// Get a <see cref="UriBuilder"/>.
    /// </summary>
    /// <param name="paths">The paths to the resource referenced by the URI.</param>
    /// <returns>The <see cref="UriBuilder"/>.</returns>
    public UriBuilder GetUriBuilder(params string[] paths)
    {
        var uriBuilder = new UriBuilder(uri);

        foreach (var path in paths)
        {
            uriBuilder.Path += $"/{path}";
        }

        return uriBuilder;
    }
}