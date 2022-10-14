using Microsoft.EntityFrameworkCore;
using Publisher.Data;
using Publisher.Domain;

using (PubContext context = new PubContext())
{
    context.Database.EnsureCreated();
}

//GetAuthors();
//AddAuthors();
//GetAuthors();
//AddAuthorWithBook();
GetAuthorWithBooks();

void GetAuthorWithBooks()
{
    using var context = new PubContext();
    var authors = context.Authors.Include(a => a.Books).ToList();
    foreach (var author in authors)
    {
        Console.WriteLine($"{author.FirstName} {author.LastName}");
        foreach (var book in author.Books)
        {
            Console.WriteLine($"\t* {book.Title}");
        }
    }
}

void AddAuthorWithBook()
{
    var author = new Author { FirstName = "William", LastName = "Mba" };

    author.Books.Add(new Book
    {
        Title = "Programming EF Core",
        PublishDate = new DateTime(2010, 2, 2)
    });
    author.Books.Add(new Book
    {
        Title = "Programming EF Core 2nd Ed.",
        PublishDate = new DateTime(2018, 10, 2)
    });
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}



void AddAuthors()
{
    var author = new Author { FirstName = "Josie", LastName = "Newf" };
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

void GetAuthors()
{
    using var context = new PubContext();
    var authors = context.Authors.ToList();
    foreach (var author in authors)
    {
        Console.WriteLine($"{author.FirstName} {author.LastName}");
    }
}