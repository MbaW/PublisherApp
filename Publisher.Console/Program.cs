using Microsoft.EntityFrameworkCore;
using Publisher.Data;
using Publisher.Domain;

PubContext _context = new PubContext();

//SortAuthors();
//AddAuthors();
//SkipAndTakeAuthors();
QueryAggregate();

void QueryAggregate()
{
    var author = _context.Authors.OrderByDescending(a => a.FirstName)
        .FirstOrDefault(a => a.LastName == "Lerman");
}

void QueryFilters()
{
    var kword = "M%";
    var authors = _context.Authors
        .Where(a => EF.Functions.Like(a.LastName, kword));
    Console.WriteLine(authors);
}
void AddAuthors()
{
    _context.Authors.Add(new Author { FirstName = "Rhoda", LastName = "Lerman" });
    _context.Authors.Add(new Author { FirstName = "Don", LastName = "Jones" });
    _context.Authors.Add(new Author { FirstName = "Jim", LastName = "Christopher" });
    _context.Authors.Add(new Author { FirstName = "Stephen", LastName = "Haunts" });
    _context.SaveChanges();
}

void SkipAndTakeAuthors()
{
    var groupeSize = 2;
    for (int i = 0; i < 5; i++)
    {
        var authors = _context.Authors.Skip(groupeSize * i).Take(groupeSize).ToList();
        Console.WriteLine($"Group {i}:");
        foreach (var author in authors)
        {
            Console.WriteLine($"{author.FirstName} {author.LastName}");
        }
    }
}


void SortAuthors()
{
    var authorByLastName = _context.Authors
        .OrderBy(a => a.LastName)
        .ThenBy(a => a.FirstName).ToList();
    authorByLastName.ForEach(a => Console.WriteLine($"{a.LastName}, {a.FirstName}"));

    var authorDescending = _context.Authors
        .OrderByDescending(a => a.LastName)
        .ThenByDescending(a => a.FirstName).ToList();
    Console.WriteLine("**Descending Last and First**");
    authorDescending.ForEach(a => Console.WriteLine($"{a.LastName}, {a.FirstName}"));
}
