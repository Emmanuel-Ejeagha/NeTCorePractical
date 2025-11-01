using System;

namespace ManningBooks;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    // public Rating rating { get; } // One-to-one Relationship
    public List<Rating> Ratings { get; } = new(); // one to many

    public List<Tag> Tags{ get; } // Many-to-many

    public Book(string title)
    {
        Title = title;
    }
}

// public class Rating
// {
//     public int Id { get; set; }
//     public int BookId { get; set; }
//     public Book Book { get; set; }
// }

public class Tag
{
    public int Id { get; set; }
    public List<Book> Books { get;}
}