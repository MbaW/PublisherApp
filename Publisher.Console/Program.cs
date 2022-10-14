using Publisher.Data;
using Publisher.Domain;

PubContext _context = new PubContext();


//RetrieveAndUpdateAuthor();
//RetrieveAndUpdateMultipleAuthor();
//VariousOperations();
//CoordinateRetrieveAndUpdateAuthor();
DeleteAnAuthor(3);
//InsertMultipleAuthors();
//BulkAddUpdate();
void BulkAddUpdate()
{
    var newAuthors = new Author[]
    {
        new Author { FirstName = "Ruth", LastName = "Ozeki" },
        new Author { FirstName = "Sofia", LastName = "Segovia" },
        new Author { FirstName = "Ursula K.", LastName = "Leguin" },
        new Author { FirstName = "Hugh", LastName = "Howey" },
        new Author { FirstName = "Isabelle", LastName = "Allende" }
    };
    _context.Authors.AddRange(newAuthors);
    var book = _context.Books.Find(2);
    book.Title = "Programming EF Core 2nd Edition";
    _context.SaveChanges();
}

void InsertMultipleAuthors()
{
    var authors = new Author[]
    {
        new Author { FirstName = "Ruth", LastName = "Ozeki" },
        new Author { FirstName = "Sofia", LastName = "Segovia" },
        new Author { FirstName = "Ursula K.", LastName = "Leguin" },
        new Author { FirstName = "Hugh", LastName = "Howey" },
        new Author { FirstName = "Isabelle", LastName = "Allende" }
    };
    InsertMultipleAuthorsPassedIn(authors);
}

void InsertMultipleAuthorsPassedIn(IEnumerable<Author> authors)
{
    _context.Authors.AddRange(authors);
    _context.SaveChanges();
}
void DeleteAnAuthor(int id)
{
    var extraWM = _context.Authors.Find(id);
    if (extraWM != null)
    {
        var books = _context.Books.Where(b => b.AuthorId == id).ToList();
        if (books.Count > 0)
        {
            _context.Books.RemoveRange(books);
        }
        _context.Authors.Remove(extraWM);

        _context.SaveChanges();
    }
}

void CoordinateRetrieveAndUpdateAuthor()
{
    var author = FindThatAuthor(1);
    if (author.FirstName == "Julie")
    {
        author.FirstName = "Julia";
        SaveThatAuthor(author);
    }
}

Author FindThatAuthor(int v)
{
    using var shortLiveContext = new PubContext();
    return shortLiveContext.Authors.Find(v);
}

void SaveThatAuthor(Author author)
{
    using var anotherShortLiveContext = new PubContext();
    anotherShortLiveContext.Authors.Update(author);
    anotherShortLiveContext.SaveChanges();
}


void VariousOperations()
{
    var author = _context.Authors.Find(2);
    author.LastName = "NewFoundLand";
    var newAuthor = new Author { LastName = "Appleman", FirstName = "Dan" };
    _context.Authors.Add(newAuthor);
    _context.SaveChanges();
}

void RetrieveAndUpdateAuthor()
{
    var lermanAuthors = _context.Authors.Where(a => a.LastName == "Lerman").ToList();
    foreach (var la in lermanAuthors)
    {
        la.LastName = "Lehrman";
    }
    _context.SaveChanges();
}
void RetrieveAndUpdateMultipleAuthor()
{
    var lermanAuthors = _context.Authors.Where(a => a.LastName == "Lehrman").ToList();
    foreach (var la in lermanAuthors)
    {
        la.LastName = "Lerman";
    }

    Console.WriteLine($"Before {_context.ChangeTracker.DebugView.ShortView}");
    _context.ChangeTracker.DetectChanges();
    Console.WriteLine($"After: {_context.ChangeTracker.DebugView.ShortView}");
    _context.SaveChanges();
}