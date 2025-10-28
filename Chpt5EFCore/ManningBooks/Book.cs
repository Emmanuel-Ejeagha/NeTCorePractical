using System;

namespace ManningBooks;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public Rating rating { get; } // One-to-one Relationship
    public List<Rating> Ratings { get; } // one to many

    public Book(string title)
    {
        Title = title;
    }
}

public class Rating
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
}