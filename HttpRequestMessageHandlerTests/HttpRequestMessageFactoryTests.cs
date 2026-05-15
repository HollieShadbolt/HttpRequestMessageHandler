using System.Net.Http.Json;

namespace HttpRequestMessageHandlerTests;

[TestFixture]
public static class HttpRequestMessageFactoryTests
{
    [Test]
    public static void HttpRequestMessageFactory_Test()
    {
        // Arrange
        var expectedScheme = Guid.NewGuid().ToString();

        var expectedParameter = Guid.NewGuid().ToString();

        // Act
        var actualHttpRequestMessageFactory =
            new HttpRequestMessageHandler.Factories.HttpRequestMessageFactory(expectedScheme, expectedParameter);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actualHttpRequestMessageFactory.Scheme, Is.EqualTo(expectedScheme));
            Assert.That(actualHttpRequestMessageFactory.Parameter, Is.EqualTo(expectedParameter));
        });
    }

    [Test]
    public static void GetHttpRequestMessage_UriBuilder_HttpMethod_Test()
    {
        // Arrange
        var expectedScheme = Guid.NewGuid().ToString();

        var expectedParameter = Guid.NewGuid().ToString();

        var httpRequestMessageFactory =
            new HttpRequestMessageHandler.Factories.HttpRequestMessageFactory(expectedScheme, expectedParameter);

        var uri = Guid.NewGuid().ToString();

        var expectedUriBuilder = new UriBuilder(uri);

        var expectedMethod = Guid.NewGuid().ToString();

        var expectedHttpMethod = new HttpMethod(expectedMethod);

        // Act
        var actualHttpRequestMessage =
            httpRequestMessageFactory.GetHttpRequestMessage(expectedUriBuilder, expectedHttpMethod);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actualHttpRequestMessage.Headers.Authorization?.Scheme, Is.EqualTo(expectedScheme));
            Assert.That(actualHttpRequestMessage.Headers.Authorization?.Parameter, Is.EqualTo(expectedParameter));
            Assert.That(actualHttpRequestMessage.Method, Is.EqualTo(expectedHttpMethod));
            Assert.That(actualHttpRequestMessage.RequestUri, Is.EqualTo(expectedUriBuilder.Uri));
        });
    }

    [Test]
    public static void GetHttpRequestMessage_UriBuilder_Test()
    {
        // Arrange
        var expectedScheme = Guid.NewGuid().ToString();

        var expectedParameter = Guid.NewGuid().ToString();

        var httpRequestMessageFactory =
            new HttpRequestMessageHandler.Factories.HttpRequestMessageFactory(expectedScheme, expectedParameter);

        var uri = Guid.NewGuid().ToString();

        var expectedUriBuilder = new UriBuilder(uri);

        // Act
        var actualHttpRequestMessage = httpRequestMessageFactory.GetHttpRequestMessage(expectedUriBuilder);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actualHttpRequestMessage.Headers.Authorization?.Scheme, Is.EqualTo(expectedScheme));
            Assert.That(actualHttpRequestMessage.Headers.Authorization?.Parameter, Is.EqualTo(expectedParameter));
            Assert.That(actualHttpRequestMessage.Method, Is.EqualTo(HttpMethod.Get));
            Assert.That(actualHttpRequestMessage.RequestUri, Is.EqualTo(expectedUriBuilder.Uri));
        });
    }

    [Test]
    public static async Task GetHttpRequestMessage_UriBuilder_HttpMethod_Object_TestAsync()
    {
        // Arrange
        var expectedScheme = Guid.NewGuid().ToString();

        var expectedParameter = Guid.NewGuid().ToString();

        var httpRequestMessageFactory =
            new HttpRequestMessageHandler.Factories.HttpRequestMessageFactory(expectedScheme, expectedParameter);

        var uri = Guid.NewGuid().ToString();

        var uriBuilder = new UriBuilder(uri);

        var method = Guid.NewGuid().ToString();

        var expectedHttpMethod = new HttpMethod(method);

        var expectedKey = Guid.NewGuid().ToString();

        var expectedValue = Guid.NewGuid().ToString();

        var inputValue = new KeyValuePair<string, string>(expectedKey, expectedValue);

        // Act
        var actualHttpRequestMessage =
            httpRequestMessageFactory.GetHttpRequestMessage(uriBuilder, expectedHttpMethod, inputValue);

        // Assert
        var actualHttpContent = actualHttpRequestMessage.Content ?? throw new InvalidOperationException();

        var actualKeyValuePair = await actualHttpContent.ReadFromJsonAsync<KeyValuePair<string, string>>();

        Assert.Multiple(() =>
        {
            Assert.That(actualHttpRequestMessage.Headers.Authorization?.Scheme, Is.EqualTo(expectedScheme));
            Assert.That(actualHttpRequestMessage.Headers.Authorization?.Parameter, Is.EqualTo(expectedParameter));
            Assert.That(actualHttpRequestMessage.Method, Is.EqualTo(expectedHttpMethod));
            Assert.That(actualHttpRequestMessage.RequestUri, Is.EqualTo(uriBuilder.Uri));
            Assert.That(actualKeyValuePair.Key, Is.EqualTo(expectedKey));
            Assert.That(actualKeyValuePair.Value, Is.EqualTo(expectedValue));
        });
    }
}