namespace Library.Application.Dtos;

/// <summary>
/// DTO для типов публикации
/// </summary>
public class PublicationTypeGetDto
{
    /// <summary>
    /// Id типа публикации
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Тип публикации
    /// </summary>
    public required string Type { get; set; }
}


