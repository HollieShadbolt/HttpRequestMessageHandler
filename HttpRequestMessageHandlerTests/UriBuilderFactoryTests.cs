namespace HttpRequestMessageHandlerTests;

[TestFixture]
public static class UriBuilderFactoryTests
{
    [Test]
    public static void UriBuilderFactory_Test()
    {
        // Arrange
        var expectedUri = Guid.NewGuid().ToString();

        // Act
        var uriBuilderFactory = new HttpRequestMessageHandler.Factories.UriBuilderFactory(expectedUri);

        // Assert
        Assert.That(uriBuilderFactory.Uri, Is.EqualTo(expectedUri));
    }

    [Test]
    public static void GetUriBuilder_Test()
    {
        // Arrange
        var uri = Guid.NewGuid().ToString();

        var uriBuilderFactory = new HttpRequestMessageHandler.Factories.UriBuilderFactory(uri);

        var pathOne = Guid.NewGuid().ToString();

        var pathTwo = Guid.NewGuid().ToString();

        var pathThree = Guid.NewGuid().ToString();

        string[] paths =
        [
            pathOne,
            pathTwo,
            pathThree
        ];

        // Act
        var actualUriBuilder = uriBuilderFactory.GetUriBuilder(paths);

        // Assert
        var expectedUriBuilder = new UriBuilder(uri)
        {
            Path = $"//{pathOne}/{pathTwo}/{pathThree}"
        };

        Assert.That(actualUriBuilder, Is.EqualTo(expectedUriBuilder));
    }
}