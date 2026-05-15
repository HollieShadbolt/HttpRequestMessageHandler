namespace HttpRequestMessageHandlerTests;

[TestFixture]
public static class HttpRequestMessageFactories
{
    [Test]
    public static void HttpRequestMessageFactories_Test()
    {
        // Arrange
        var expectedScheme = Guid.NewGuid().ToString();

        var expectedParameter = Guid.NewGuid().ToString();

        var expectedUri = Guid.NewGuid().ToString();

        // Act
        var httpRequestMessageExecutorFactories =
            new HttpRequestMessageHandler.HttpRequestMessageFactories(expectedScheme, expectedParameter, expectedUri);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(expectedScheme,
                Is.EqualTo(httpRequestMessageExecutorFactories.HttpRequestMessageFactory.Scheme));

            Assert.That(expectedParameter,
                Is.EqualTo(httpRequestMessageExecutorFactories.HttpRequestMessageFactory.Parameter));

            Assert.That(expectedUri, Is.EqualTo(httpRequestMessageExecutorFactories.UriBuilderFactory.Uri));
        });
    }
}