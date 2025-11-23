using ManningBooksApi;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CatalogContext>(options =>
    options.UseSqlite(
        "DataSource=manningbooks.db;cache=shared"));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    using (var dbContext = scope.ServiceProvider
        .GetRequiredService<CatalogContext>())
        dbContext.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

// A keep-alice connection is required only for the Sqlite in-memory
// using var keepAliveConnection = new SqliteConnection(CatalogContext.ConnectionString);
// keepAliveConnection.Open();

// // app.MapGet("/", () => "Hello World!");

// CatalogContext.SeedBooks();

app.Run();
