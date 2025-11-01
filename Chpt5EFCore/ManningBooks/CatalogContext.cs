using System;
using Microsoft.EntityFrameworkCore;

namespace ManningBooks;

public class CatalogContext : DbContext
{
    public const string ConnectionString = "DataSource=manningbooks.db;cache=shared";
    public DbSet<Book> Books { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite(ConnectionString);
    public static void SeedBooks()
    {
        using var dbContext = new CatalogContext();
        if (!dbContext.Database.EnsureCreated())
        {
            return;
        }

        dbContext.Add(new Book("Grokking Simplicity"));
        dbContext.Add(new Book("API Design Patterns"));
        var efBook = new Book("EF Core in Action");
        efBook.Ratings.Add(new Rating { Comment = "Great!" });
        efBook.Ratings.Add(new Rating { Stars = 4 });
        dbContext.Add(efBook);
        dbContext.SaveChanges();
    }
    
    public static async Task WriteBookToConsoleAsync (string title)
    {
        using var dbContext = new CatalogContext();
        var book = await dbContext.Books.Include(b => b.Ratings)
          .FirstOrDefaultAsync(b => b.Title == title);
        if (book == null)
        {
            Console.WriteLine(@$"""{title}"" not found.");
        }
        else
        {
            Console.WriteLine(@$"Book ""{book.Title}"" has id {book.Id}");
            book.Ratings.ForEach(r => Console.WriteLine($"\t{r.Stars} stars: {r.Comment ?? "-blank-"}"));
        }
    }
}

