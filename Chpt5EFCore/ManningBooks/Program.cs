using ManningBooks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

// using var keepAliveConnection = new SqliteConnection(CatalogContext.ConnectionString);
// keepAliveConnection.Open();

CatalogContext.SeedBooks();

var userRequests = new[]
{
    ".NET in Action",
"Grokking Simplicity",
"API Design Patterns",
"EF Core in Action",
};
// Asynchronous method
Console.WriteLine("ASYNCHRONOUS METHOD");
foreach (var userRequest in userRequests)
{
    await CatalogContext.WriteBookToConsoleAsync(userRequest);
}

// Synchronous method
Console.WriteLine("SYNCHRONOUS METHOD");
var tasks = new List<Task>();
foreach (var userRequest in userRequests)
{
    tasks.Add(CatalogContext.WriteBookToConsoleAsync(userRequest));
}
Task.WaitAll(tasks.ToArray());

using var dbContext = new CatalogContext();
dbContext.Database.EnsureCreated();

dbContext.Add(new Book("NET in Action"));
dbContext.Add(new Book("Fundamentals of Computer Programming with C#"));
dbContext.Add(new Book ( "API Design Patterns"));
dbContext.Add(new Book ( "The Programmer's Brain"));
dbContext.Add(new Book ( "Grokking Simplicity"));
dbContext.SaveChanges();

foreach (var book in dbContext.Books.OrderBy(b => b.Id))
{
    Console.WriteLine($"\"{book.Title}\" has id {book.Id}");
}

var efBook = new Book("EF Core in Action");
efBook.Ratings.Add(new Rating { Comment = "Great" });
efBook.Ratings.Add(new Rating { Stars = 4 });
dbContext.Add(efBook);
dbContext.SaveChanges();

var efRatings = (from b in dbContext.Books
                 where b.Title == "EF Core in Action"
                 select b.Ratings)
    .FirstOrDefault();
efRatings?.
    ForEach(r => Console.WriteLine(
        $"{r.Stars} stars: {r.Comment ?? "-blank-"}"));

foreach (var book in dbContext.Books.Include(b => b.Ratings))
{
    Console.WriteLine($"Book \"{book.Title}\" has id {book.Id}");
    book.Ratings.ForEach(r => Console.WriteLine(
        $"\t{r.Stars} stars: {r.Comment ?? "-blank-"}"));
}

