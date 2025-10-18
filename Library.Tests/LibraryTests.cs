namespace Library.Tests;

/// <summary>
/// Коллекция юнит-тестов для библиотеки
/// </summary>
public class LibraryTests(LibraryFixture fixture) : IClassFixture<LibraryFixture>
{
    /// <summary>
    /// Проверяется. что выданные книги исвлечены правильно и отсортированы.
    /// </summary>
    [Fact]
    public void BooksOrderedByTitle()
    {
        var testDate = new DateOnly(2025, 10, 18);
        var expectedOrder = new List<string>
        {
            "Inferno",
            "Murder on the Orient Express",
            "The Da Vinci Code",
        };

        var actualOrder = fixture.CheckoutRepository.ReadAll()
            .Where(c => c.LoanDate <= testDate && c.LoanDate.AddDays(c.LoanDays) >= testDate)
            .Select(c => c.Book)
            .Distinct()
            .OrderBy(b => b.Title)
            .Select(b => b.Title)
            .ToList();

        Assert.Equal(expectedOrder, actualOrder);
    }

    /// <summary>
    /// Тест выводит информацию о топ-5 читателях, прочитавших наибольшее количество книг за период.
    /// </summary>
    [Fact]
    public void TopReadersByNumberOfBooks()
    {
        var start = new DateOnly(2025, 1, 1);
        var end = new DateOnly(2025, 12, 31);
        var expectedTop5Readers = new List<string>
        {
            "John Lennon",
            "Beyonce Knowles",
            "Elon Musk",
            "Leonardo DiCaprio",
            "Angela Merkel"
        };

        var top5Readers = fixture.CheckoutRepository.ReadAll()
            .Where(c => c.LoanDate >= start && c.LoanDate.AddDays(c.LoanDays) <= end)
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
    /// Тест выводит информацию о читателях, бравших книги на наибольший период времени, отсортированных по ФИО.
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

        var topReaders = fixture.CheckoutRepository.ReadAll()
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
    /// Тест выводит топ-5 наиболее популярных издательств за последний год.
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

        var topPublishers = fixture.CheckoutRepository.ReadAll()
            .Where(c => c.LoanDate >= oneYearAgo)
            .GroupBy(c => c.Book.Publisher)
            .Select(g => new 
            { 
                Publisher = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .Select(x => x.Publisher.Name)
            .ToList();

        Assert.Equal(expected, topPublishers);
    }

    /// <summary>
    /// Тест выводит топ-5 наименее популярных книг за последний год.
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

        var recentLoans = fixture.CheckoutRepository.ReadAll()
            .Where(с=> с.LoanDate >= oneYearAgo)
            .ToList();

        var actualBooks = recentLoans
            .GroupBy(с => с.Book)
            .Select(g => new 
            { 
                Book = g.Key,
                Count = g.Count() 
            })
            .OrderBy(x => x.Count)
            .ThenBy(x => x.Book.Title) 
            .Take(5)
            .Select(x => x.Book.Title)
            .ToList();

        Assert.Equal(expectedBooks, actualBooks);
    }
}

