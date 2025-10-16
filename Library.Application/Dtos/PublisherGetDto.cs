namespace Library.Application.Dtos;
public class PublisherGetDto
{
    /// <summary>
    /// The unique id for publisher.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Publisher's name.
    /// </summary>
    public required string Name { get; set; }
}
