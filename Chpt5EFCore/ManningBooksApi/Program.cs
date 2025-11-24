using ManningBooksApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddDbContext<CatalogContext>(options =>
    {
        var connectionString = config.GetConnectionString("Catalog");
        options.UseSqlite(connectionString);
    });

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer();

builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = DateTime.UtcNow.AddYears(1) - DateTime.UtcNow;
    options.ExcludedHosts.Add("test.manningcatalog.net");
});

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
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

// A keep-alice connection is required only for the Sqlite in-memory
// using var keepAliveConnection = new SqliteConnection(CatalogContext.ConnectionString);
// keepAliveConnection.Open();

// // app.MapGet("/", () => "Hello World!");

// CatalogContext.SeedBooks();

app.Run();
