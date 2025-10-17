namespace Library.Application.Dtos.AnaliticsDtos;
public class PublisherCountDto
{
    /// <summary>
    /// Publisher's name.
    /// </summary>
    public required string Name { get; set; }

    public required int Count { get; set; } = 0;
}
