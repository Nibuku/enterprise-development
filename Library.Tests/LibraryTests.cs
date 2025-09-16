namespace Library.Tests;

/// <summary>
/// A collection of unit tests for the Library domain.
/// </summary>
public class LibraryTests(DataSeed seed) : IClassFixture<DataSeed>
{
    /// <summary>
    /// Tests that loaned books are correctly retrieved and ordered by their title.
    /// </summary>
    [Fact]
    public void BooksOrderedByTitle()
    {
        var expectedOrder = new List<string>
        {
            "And Then There Were None",
            "Angels & Demons",
            "Inferno",
            "Murder on the Orient Express",
            "Norwegian Wood",
            "Physics for Universities",
            "Programming in C#",
            "Roadside Picnic",
            "The Da Vinci Code",
            "The Great Encyclopedia",
            "The Master and Margarita",
            "War and Peace"
        };

        var actualOrder = seed.Checkouts
            .Select(c => c.Book)
            .Distinct()
            .OrderBy(b => b.Title)
            .Select(b => b.Title)
            .ToList();

        Assert.Equal(expectedOrder, actualOrder);
    }

    /// <summary>
    /// Test that outputs information about the top 5 readers who have read the most books in a given period.
    /// </summary>
    [Fact]
    public void TopReadersByNumberOfBooks()
    {
        var expectedTop5Readers = new List<string>
        {
            "John Lennon",
            "Beyonce Knowles",
            "Elon Musk",
            "Leonardo DiCaprio",
            "Angela Merkel"
        };

        var top5Readers = seed.Checkouts
            .GroupBy(c=> c.Reader)
            .Select(g => new
            {
                Reader = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Reader.FullName)
            .Take(5)
            .Select(x => x.Reader.FullName)
            .ToList();

        Assert.Equal(expectedTop5Readers, top5Readers);
    }

    /// <summary>
    /// Test that outputs information about readers who have taken books for the longest period of time, sorted by full name.
    /// </summary>
    [Fact]
    public void TopReadersByTotalLoanDays()
    {
        var expected = new List<(string Name, int Days)>
        {
            ("Sergey Brin", 60),
            ("John Lennon", 47),
            ("Leonardo DiCaprio", 44),
            ("Beyonce Knowles", 42),
            ("Angela Merkel", 30)
        };

        var topReaders = seed.Checkouts
            .GroupBy(c => c.Reader)
            .Select(g => new
            {
                g.Key.FullName,
                TotalLoanDays = g.Sum(c => c.LoanDays)
            })
            .OrderByDescending(r => r.TotalLoanDays)
            .ThenBy(r => r.FullName)
            .Take(5)
            .ToList();

        Assert.Equal(expected.Select(e => e.Name), topReaders.Select(r => r.FullName));
        Assert.Equal(expected.Select(e => e.Days), topReaders.Select(r => r.TotalLoanDays));
    }

    /// <summary>
    /// Test that displays the top 5 most popular publishers over the past year.
    /// </summary>
    [Fact]
    public void TopPopularPublishersLastYear()
    {
        var oneYearAgo = new DateOnly(2024, 9, 30);
        var expected = new List<string>
        {
            "AST",
            "Eksmo",
            "Prosveshchenie",
            "Piter",
            "DMKPress"
        };

        var topPublishers = seed.Checkouts
            .Where(c => c.LoanDate >= oneYearAgo)
            .GroupBy(c => c.Book.Publisher)
            .Select(g => new { Publisher = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .Select(x => x.Publisher.Name)
            .ToList();

        Assert.Equal(expected, topPublishers);
    }

    /// <summary>
    /// Test that outputs the top 5 least popular books over the past year.
    /// </summary>
    [Fact]
    public void TopLeastPopularBooksLastYear()
    {
        var oneYearAgo = new DateOnly(2024, 9, 30);
        var expectedBooks = new List<string>
        {
            "And Then There Were None",
            "Angels & Demons",
            "Physics for Universities",
            "Programming in C#",
            "Roadside Picnic"
        };

        var recentLoans = seed.Checkouts
            .Where(ñ=> ñ.LoanDate >= oneYearAgo)
            .ToList();

        var actualBooks = recentLoans
            .GroupBy(ñ => ñ.Book)
            .Select(g => new { Book = g.Key, Count = g.Count() })
            .OrderBy(x => x.Count)
            .ThenBy(x => x.Book.Title) 
            .Take(5)
            .Select(x => x.Book.Title)
            .ToList();

        Assert.Equal(expectedBooks, actualBooks);
    }
}

