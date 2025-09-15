using Library.Domain.Enums;
using Library.Domain.Models;

namespace Library.Tests.Domain;

/// <summary>
/// A collection of unit tests for the Library domain.
/// </summary>
public class LibraryTests : IClassFixture<DataSeed>
{
    private readonly List<Book> _books;
    private readonly List<Reader> _readers;
    private readonly List<BookLoan> _loans;

    public LibraryTests(DataSeed seed)
    {
        _books = seed.Books;
        _readers = seed.Readers;
        _loans = seed.Loans;
    }

    /// <summary>
    /// Tests that loaned books are correctly retrieved and ordered by their title.
    /// </summary>
    [Fact]
    public void Books_OrderedByTitle()
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

    /// <summary>
    /// Test that outputs information about the top 5 readers who have read the most books in a given period.
    /// </summary>
    [Fact]
    public void Top5Readers_ByNumberOfBooks()
    {

        var top5Readers = _readers
            .OrderByDescending(r => r.BookLoans.Count)
            .ThenBy(r => r.FullName)
            .Take(5)
            .ToList();


        var expectedTop5Readers = new List<Reader>
            {
                _readers.First(r => r.FullName == "Джон Леннон"),       
                _readers.First(r => r.FullName == "Бейонсе Ноулз"),     
                _readers.First(r => r.FullName == "Илон Маск"),         
                _readers.First(r => r.FullName == "Леонардо ДиКаприо"), 
                _readers.First(r => r.FullName == "Ангела Меркель")      
            };

        Assert.Equal(expectedTop5Readers, top5Readers);
    }

    /// <summary>
    /// Test that outputs information about readers who have taken books for the longest period of time, sorted by full name.
    /// </summary>
    [Fact]
    public void Top5Readers_ByTotalLoanDays()
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

    /// <summary>
    /// Test that displays the top 5 most popular publishers over the past year.
    /// </summary>
    [Fact]
    public void Top5PopularPublishers_LastYear()
    {
        var oneYearAgo = new DateOnly(2024, 9, 30);

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

    /// <summary>
    /// Test that outputs the top 5 least popular books over the past year.
    /// </summary>
    [Fact]
    public void Top5LeastPopularBooks_LastYear()
    {
        var oneYearAgo = new DateOnly(2024, 9, 30);

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

