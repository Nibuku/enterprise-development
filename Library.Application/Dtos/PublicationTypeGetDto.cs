namespace Library.Application.Dtos;
public class PublicationTypeGetDto
{
    /// <summary>
    /// The unique id for type of publication.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Type of publication
    /// </summary>
    public required string Type { get; set; }
}


