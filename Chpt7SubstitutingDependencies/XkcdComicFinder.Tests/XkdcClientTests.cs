using System;
using System.Net;
using FakeItEasy;

namespace XkcdComicFinder.Tests;

public class XkcdClientTests
{
    private readonly XkcdClient xkcdClient;
    private readonly HttpMessageHandler _fakeMsgHandler;

private const string LatestJson = """
{
    "month": "4",
    "num": 2630,
    "link": "",
    "year": "2024", 
    "news": "",
    "safe_title": "Paperwork",
    "transcript": "",
    "alt": "The paperwork is just a waiver confirming you understand that the paperwork is just a waiver.",
    "img": "https://imgs.xkcd.com/comics/paperwork.png",
    "title": "Paperwork",
    "day": "12"
}
""";
    public XkcdClientTests()
    {
        _fakeMsgHandler = A.Fake<HttpMessageHandler>();
        var httpClient = SetupHttpClient(_fakeMsgHandler);
        xkcdClient = new(httpClient);
    }

    internal static HttpClient SetupHttpClient(HttpMessageHandler msgHandler)
    {
        var httpClient = new HttpClient(msgHandler);
        httpClient.BaseAddress = new Uri("https://xkcd.com");
        return httpClient;
    }

    private void SetResponse(HttpStatusCode statusCode, string content = "")
    {
        A.CallTo(_fakeMsgHandler)
           .WithReturnType<Task<HttpResponseMessage>>()
           .Where(c => c.Method.Name == "SendAsync")
           .Returns(new HttpResponseMessage()
           {
               StatusCode = statusCode,
               Content = new StringContent(content)
           });
    }

    [Fact]
    public async Task GetLatest()
    {
        SetResponse(HttpStatusCode.OK, LatestJson);
        var comic = await xkcdClient.GetLatestAsync();
        Assert.Equal(2630, comic.Number);
    }
    [Fact]
    public async Task NoComicFound()
    {
        SetResponse(HttpStatusCode.NotFound);
        var comic = await xkcdClient.GetByNumberAsync(1);
        Assert.Null(comic);
    }
    [Fact]
    public async Task GetByNumber()
    {
        SetResponse(HttpStatusCode.OK, LatestJson);
        var comic = await xkcdClient.GetByNumberAsync(2630);
        Assert.NotNull(comic);
        Assert.Equal(2630, comic.Number);
    }
}       
