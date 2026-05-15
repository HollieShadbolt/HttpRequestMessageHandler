namespace HttpRequestMessageHandlerTests;

[TestFixture]
public static class HttpResponseMessageHandlersTests
{
    [Test]
    public static async Task VerifyAsync_Success_TestAsync()
    {
        // Arrange
        var content = Guid.NewGuid().ToString();

        var expectedHttpContent = new StringContent(content);

        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = expectedHttpContent
        };

        // Act
        var actualHttpContent = await HttpRequestMessageHandler.HttpResponseMessageHandlers.HandleAsync(
            httpResponseMessage,
            CancellationToken.None);

        // Assert
        Assert.That(actualHttpContent, Is.EqualTo(expectedHttpContent));
    }

    [Test]
    public static void VerifyAsync_HttpRequestException_Test()
    {
        // Arrange
        var content = Guid.NewGuid().ToString();

        var httpContent = new StringContent(content);

        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.InternalServerError,
            Content = httpContent
        };

        // Act & Assert
        Assert.ThrowsAsync<HttpRequestException>(async () =>
            await HttpRequestMessageHandler.HttpResponseMessageHandlers.HandleAsync(httpResponseMessage,
                CancellationToken.None));
    }

    [Test]
    public static async Task VerifyAsync_Type_Success_TestAsync()
    {
        // Arrange
        var expectedKey = Guid.NewGuid().ToString();

        var expectedValue = Guid.NewGuid().ToString();

        var inputValue = new KeyValuePair<string, string>(expectedKey, expectedValue);

        var jsonContent = System.Net.Http.Json.JsonContent.Create(inputValue);

        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = jsonContent
        };

        // Act
        var actualKeyValuePair =
            await HttpRequestMessageHandler.HttpResponseMessageHandlers.HandleAsync<KeyValuePair<string, string>>(
                httpResponseMessage, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actualKeyValuePair.Key, Is.EqualTo(expectedKey));
            Assert.That(actualKeyValuePair.Value, Is.EqualTo(expectedValue));
        });
    }

    [Test]
    public static void VerifyAsync_Type_HttpRequestException_Test()
    {
        // Arrange
        var content = Guid.NewGuid().ToString();

        var httpContent = new StringContent(content);

        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.InternalServerError,
            Content = httpContent
        };

        // Act & Assert
        Assert.ThrowsAsync<HttpRequestException>(async () =>
            await HttpRequestMessageHandler.HttpResponseMessageHandlers.HandleAsync<object>(httpResponseMessage,
                CancellationToken.None));
    }

    [Test]
    public static void VerifyAsync_Type_JsonException_Test()
    {
        // Arrange
        var content = Guid.NewGuid().ToString();

        var httpContent = new StringContent(content);

        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = httpContent
        };

        // Act & Assert
        Assert.ThrowsAsync<System.Text.Json.JsonException>(async () =>
            await HttpRequestMessageHandler.HttpResponseMessageHandlers.HandleAsync<KeyValuePair<string, string>>(
                httpResponseMessage, CancellationToken.None));
    }
}