using System.Collections.Generic;
using Library.Domain.Enums;

namespace Library.Domain.Models;

public class Book
{
    public int Id { get; set; }
    public required string InventoryNumber { get; set; }
    public required string CatalogCode { get; set; }
    public required string Title { get; set; }
    public required PublicationType PublicationType { get; set; }
    public required Publisher Publisher { get; set; }
    public required int PublicationYear { get; set; }
    public List<string> Authors { get; set; } = new List<string>();
}