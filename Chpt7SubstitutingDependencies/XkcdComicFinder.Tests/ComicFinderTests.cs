using System;
using System.Net;
using System.Text.Json;
using FakeItEasy;
using Microsoft.Data.Sqlite;

namespace XkcdComicFinder.Tests;

public class ComicFinderTests : IDisposable
{
    private const string NumberLink = "https://xkcd.com/{0}/info.0.json";
    private const string LatestLink = "https://xkcd.com/info.0.json";
    private readonly ComicDbContext _comicDbContext;
    private readonly SqliteConnection _keepAliveConn;
    private readonly HttpMessageHandler _fakeMsgHandler;
    private readonly ComicFinder _comicFinder;

    public ComicFinderTests()
    {
        (_comicDbContext, _keepAliveConn) =
          ComicRepositoryTests.SetupSqlite("comics_int");
        var comicRepo = new ComicRepository(_comicDbContext);

        _fakeMsgHandler = A.Fake<HttpMessageHandler>();
        var httpClient =
          XkcdClientTests.SetupHttpClient(_fakeMsgHandler);
        var xkcdClient = new XkcdClient(httpClient);

        _comicFinder = new ComicFinder(xkcdClient, comicRepo);
    }

    public void Dispose()
    {
        _keepAliveConn.Close();
        _comicDbContext.Dispose();
    }

    private static Uri GetUri(Comic c) =>
      new(string.Format(NumberLink, c.Number));

    internal static void SetResponseComics(
        HttpMessageHandler fakeMsgHandler,
        params Comic[] comics)
    {
        var responses = comics.ToDictionary(GetUri,
            c => JsonSerializer.Serialize(c));
        responses.Add(new Uri(LatestLink),
            JsonSerializer.Serialize(comics[0]));

        A.CallTo(fakeMsgHandler)
            .WithReturnType<Task<HttpResponseMessage>>()
            .Where(c => c.Method.Name == "SendAsync")
            .Returns(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
            });
        foreach (var responsePair in responses)
        {
            A.CallTo(fakeMsgHandler)
                .WithReturnType<Task<HttpResponseMessage>>()
                .Where(c => c.Method.Name == "SendAsync")
                .WhenArgumentsMatch(args =>
                    args.First() is HttpRequestMessage req
                    && req.RequestUri == responsePair.Key)
                .Returns(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responsePair.Value),
                });
        }
    }

    [Fact]
    public async Task StartWithEmptyRepo()
    {
        // Given
        SetResponseComics(_fakeMsgHandler,
            new Comic() { Number = 12, Title = "b" },
            new Comic() { Number = 1, Title = "a" },
            new Comic() { Number = 4, Title = "c" });

        // When
        var foundComics = (await _comicFinder.FindAsync("b"))
            .ToBlockingEnumerable();

        // Then
        Assert.Single(foundComics);
        Assert.Single(foundComics, c=> c.Number == 12);
    }
}
