using System;
using Microsoft.EntityFrameworkCore;

namespace ManningBooks;

public class CatalogContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseInMemoryDatabase("ManningBooks");
}
