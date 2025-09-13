using Library.Domain.Enums;
using Library.Domain.Models;

namespace Library.Tests.Library.Tests;

public class LibraryTests
{
    private readonly List<Author> _authors;
    private readonly List<Book> _books;
    private readonly List<Reader> _readers;
    private readonly List<BookLoan> _loans;

    public LibraryTests()
    {
        _authors = DataSeed.GetAuthors();
        _books = DataSeed.GetBooks();
        _readers = DataSeed.GetReaders();
        _loans = DataSeed.GetBookLoans(_books, _readers);

        DataSeed.LinkAuthorsAndBooks(_authors, _books);
        DataSeed.LinkReadersWithLoans(_readers, _loans);
    }

    [Fact]
    public void Books_ShouldBeOrderedByTitle()
    {
        var booksQuery =
            from loan in _loans
            select loan.Book;

        var books = booksQuery.Distinct().ToList();

        var expectedOrder = new List<Book>
        {
            _books.First(b => b.Title == "Ангелы и демоны"),
            _books.First(b => b.Title == "Большой энциклопедический словарь"),
            _books.First(b => b.Title == "Война и мир"),
            _books.First(b => b.Title == "Десять негритят"),
            _books.First(b => b.Title == "Инферно"),
            _books.First(b => b.Title == "Код да Винчи"),
            _books.First(b => b.Title == "Мастер и Маргарита"),
            _books.First(b => b.Title == "Норвежский лес"),
            _books.First(b => b.Title == "Пикник на обочине"),
            _books.First(b => b.Title == "Программирование на C#"),
            _books.First(b => b.Title == "Убийство в Восточном экспрессе"),
            _books.First(b => b.Title == "Физика для вузов"),
            
        };

        var actualOrder = books.OrderBy(b => b.Title).ToList();

        Assert.Equal(expectedOrder, actualOrder);
    }

    [Fact]
    public void Top5Readers_ByNumberOfLoans()
    {

        var top5Readers = _readers
            .OrderByDescending(r => r.BookLoans.Count)
            .ThenBy(r => r.FullName)
            .Take(5)
            .ToList();


        var expectedTop5Readers = new List<Reader>
            {
                _readers.First(r => r.FullName == "Джон Леннон"),        // 3 книги
                _readers.First(r => r.FullName == "Бейонсе Ноулз"),      // 2 книги
                _readers.First(r => r.FullName == "Илон Маск"),          // 2 книги
                _readers.First(r => r.FullName == "Леонардо ДиКаприо"), // 2 книги
                _readers.First(r => r.FullName == "Ангела Меркель")      // 1 книга
            };

        Assert.Equal(expectedTop5Readers, top5Readers);
    }

    [Fact]
    public void Readers_Top5ByTotalLoanDays_ShouldBeOrderedByFullName()
    {
        var loanDaysPerReader = _readers
            .Select(reader => new
            {
                Reader = reader,
                TotalLoanDays = reader.BookLoans.Sum(loan => loan.LoanDays) 
            })
            .ToList();

        var top5Readers = loanDaysPerReader
            .OrderByDescending(r => r.TotalLoanDays)
            .ThenBy(r => r.Reader.FullName)
            .Take(5)
            .Select(r => new
            {
                r.Reader.FullName,
                r.TotalLoanDays
            })
            .ToList();

        var expected = new List<(string Name, int Days)>
        {
            ("Сергей Брин", 60),
            ("Джон Леннон", 47),
            ("Леонардо ДиКаприо", 44),
            ("Бейонсе Ноулз", 42),
            ("Ангела Меркель", 30)
        };

        Assert.Equal(expected.Select(e => e.Name), top5Readers.Select(r => r.FullName));
        Assert.Equal(expected.Select(e => e.Days), top5Readers.Select(r => r.TotalLoanDays));
    }


    [Fact]
    public void Top5PopularPublishers_LastYear()
    {
        var oneYearAgo = new DateTime(2024, 9, 30);

        var topPublishers = _loans
            .Where(l => l.LoanDate >= oneYearAgo)
            .GroupBy(l => l.Book.Publisher)
            .Select(g => new { Publisher = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToList();

        var expected = new List<Publisher>
        {
            Publisher.Eksmo,
            Publisher.AST,
            Publisher.Prosveshchenie,
            Publisher.Piter,
            Publisher.DMKPress
        };

        Assert.Equal(expected, topPublishers.Select(x => x.Publisher));
    }


    [Fact]
    public void Top5LeastPopularBooks_LastYear_ShouldMatchExpected()
    {
        var oneYearAgo = new DateTime(2024, 9, 30);

        var recentLoans = _loans
            .Where(l => l.LoanDate >= oneYearAgo)
            .ToList();

        var loansByBook = recentLoans
            .GroupBy(l => l.Book)
            .Select(g => new { Book = g.Key, Count = g.Count() })
            .OrderBy(x => x.Count) 
            .Take(5)               
            .Select(x => x.Book)  
            .ToList();

        var expectedBooks = new List<Book>
        {
            _books.First(b => b.Title == "Десять негритят"),
            _books.First(b => b.Title == "Ангелы и демоны"),
            _books.First(b => b.Title == "Пикник на обочине"),
            _books.First(b => b.Title == "Война и мир"),
            _books.First(b => b.Title == "Мастер и Маргарита")
        };

        Assert.Equal(expectedBooks.Select(b => b.Title), loansByBook.Select(b => b.Title));
    }

}

