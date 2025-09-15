namespace Library.Domain.Enums;

/// <summary>
/// Defines the type of a publication
/// </summary>
public enum PublicationType
{
    /// <summary>A scholarly book on a single subject. </summary>
    Monograph,

    /// <summary>A book used for education. </summary>
    Textbook,

    /// <summary>A book containing works from multiple authors. </summary>
    Collection,

    /// <summary>A publication released at regular intervals, like a magazine or newspaper.</summary>
    Periodical,

    /// <summary>A book containing factual information on various topics.</summary>
    ReferenceBook,

    /// <summary>A book that teaches a particular skill or subject. </summary>
    Tutorial,

    /// <summary>A long research paper, often for a university degree.</summary>
    Dissertation,

    /// <summary> A fictional story of book length. </summary>
    Novel
}
