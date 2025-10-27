using ManningBooks;

using var dbContext = new CatalogContext();
dbContext.Add(new Book { Title = ".NET in Action" });
dbContext.Add(new Book { Title = "Fundamentals of Computer Programming with C#" });
dbContext.Add(new Book { Title = "API Design Patterns" });
dbContext.Add(new Book { Title = "The Programmer's Brain" });
dbContext.Add(new Book { Title = "Grokking Simplicity" });
dbContext.SaveChanges();

foreach (var book in dbContext.Books.OrderBy(b => b.Id))
{
    Console.WriteLine($"\"{book.Title}\" has id {book.Id}");
}