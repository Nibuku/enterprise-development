namespace Library.Application.Dtos.AnaliticsDtos;
public class PublisherCountDto
{
    /// <summary>
    /// The unique id for publisher.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Publisher's name.
    /// </summary>
    public required string Name { get; set; }

    public required int Count { get; set; } = 0;
}
