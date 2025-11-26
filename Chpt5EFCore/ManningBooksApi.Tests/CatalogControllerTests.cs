using System;
using ManningBooksApi.Controllers;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace ManningBooksApi.Tests;

public class CatalogControllerTests : IDisposable
{
    private const string ConnectionString = "DataSource=test;mode=memory;cache=shared";
    private readonly CatalogContext _catalogContext;
    private readonly SqliteConnection _keepAliveConn;
    private readonly CatalogController _controller;

    public CatalogControllerTests(ITestOutputHelper testOutput)
    {
        _keepAliveConn = new(ConnectionString);
        _keepAliveConn.Open();

        var optionBuilder = new DbContextOptionsBuilder();
        optionBuilder
           .AddInterceptors(new LogSqlInterceptor(testOutput))
           .UseSqlite(ConnectionString);
        _catalogContext = new(optionBuilder.Options);
        Assert.True(_catalogContext.Database.EnsureCreated());
        _controller = new(_catalogContext);
    }

    [Fact]
    public async Task GetBook()
    {
        SeedBooks();
        var book = await _controller.GetBook(1);
        Assert.NotNull(book);
    }

    public void Dispose()
    {
        _keepAliveConn.Close();
        _catalogContext.Dispose();
    }

    private void SeedBooks()
    {
        _catalogContext.Add(new Book("Grokking Simplicity"));
        _catalogContext.Add(new Book("API Design Patterns"));
        var efBook = new Book("EF Core in Action");
        efBook.Ratings.Add(new Rating { Comment = "Great!" });
        efBook.Ratings.Add(new Rating { Stars = 4 });
        _catalogContext.Add(efBook);
        _catalogContext.SaveChanges();
    }
}
