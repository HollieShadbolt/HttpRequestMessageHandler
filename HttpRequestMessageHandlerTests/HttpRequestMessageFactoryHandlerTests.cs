namespace HttpRequestMessageHandlerTests;

using Moq;

[TestFixture]
public static class HttpRequestMessageFactoryHandlerTests
{
    [Test]
    public static async Task SendAsync_TestAsync()
    {
        // Arrange
        var mockHttpClientHandler = new Mock<HttpRequestMessageHandler.Interfaces.IHttpRequestMessageHandler>();

        var httpRequestMessage = new HttpRequestMessage();

        var cancellationTokenSource = new CancellationTokenSource();

        var httpResponseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

        mockHttpClientHandler
            .Setup(httpRequestMessageHandler =>
                httpRequestMessageHandler.SendAsync(httpRequestMessage, cancellationTokenSource.Token))
            .ReturnsAsync(httpResponseMessage);

        var mockDelayHandler = new Mock<HttpRequestMessageHandler.Interfaces.IDelayHandler>();

        var httpContentFactory =
            new HttpRequestMessageHandler.HttpRequestMessageFactoryHandler(
                mockHttpClientHandler.Object,
                mockDelayHandler.Object);

        // Act
        await httpContentFactory.SendAsync(HttpRequestMessageFactory, cancellationTokenSource.Token);

        // Assert
        mockHttpClientHandler.Verify(
            httpRequestMessageHandler =>
                httpRequestMessageHandler.SendAsync(httpRequestMessage, cancellationTokenSource.Token),
            Times.Exactly(1));

        mockHttpClientHandler.VerifyNoOtherCalls();

        mockDelayHandler.Verify(delayHandler => delayHandler.Delay(1_000, cancellationTokenSource.Token),
            Times.Exactly(1));

        mockDelayHandler.VerifyNoOtherCalls();

        return;

        HttpRequestMessage HttpRequestMessageFactory()
        {
            return httpRequestMessage;
        }
    }

    [Test]
    public static async Task SendAsync_ExceptionHandler_TestAsync()
    {
        // Arrange
        var mockHttpClientHandler = new Mock<HttpRequestMessageHandler.Interfaces.IHttpRequestMessageHandler>();

        var cancellationTokenSource = new CancellationTokenSource();

        var httpRequestMessage = new HttpRequestMessage();

        var inputValue = Guid.NewGuid().ToString();

        var jsonContent = System.Net.Http.Json.JsonContent.Create(inputValue);

        var httpResponseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = jsonContent
        };

        var count = 0;

        const int limit = 3;

        var exception = new Exception();

        mockHttpClientHandler
            .Setup(httpClientHandler => httpClientHandler.SendAsync(httpRequestMessage, cancellationTokenSource.Token))
            .ReturnsAsync(() => count++ < limit ? throw exception : httpResponseMessage);

        var mockDelayHandler = new Mock<HttpRequestMessageHandler.Interfaces.IDelayHandler>();

        var httpContentFactory =
            new HttpRequestMessageHandler.HttpRequestMessageFactoryHandler(
                mockHttpClientHandler.Object,
                mockDelayHandler.Object);

        // Act
        await httpContentFactory.SendAsync(HttpRequestMessageFactory, cancellationTokenSource.Token);

        // Assert
        mockHttpClientHandler.Verify(httpRequestMessageHandler => httpRequestMessageHandler.ExceptionHandler(exception),
            Times.Exactly(limit));

        mockHttpClientHandler.Verify(
            httpRequestMessageHandler =>
                httpRequestMessageHandler.SendAsync(httpRequestMessage, cancellationTokenSource.Token),
            Times.Exactly(limit + 1));

        mockHttpClientHandler.VerifyNoOtherCalls();

        mockDelayHandler.Verify(delayHandler => delayHandler.Delay(1_000, cancellationTokenSource.Token),
            Times.Exactly(4));

        mockDelayHandler.VerifyNoOtherCalls();

        return;

        HttpRequestMessage HttpRequestMessageFactory()
        {
            return httpRequestMessage;
        }
    }
}