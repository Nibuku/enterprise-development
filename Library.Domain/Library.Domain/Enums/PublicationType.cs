namespace Library.Domain.Enums;

/// <summary>
/// Вид издания (справочник)
/// </summary>
public enum PublicationType
{
    /// <summary>Монография - научное издание по одной теме</summary>
    Monograph,

    /// <summary>Учебник - для обучения по образовательной программе</summary>
    Textbook,

    /// <summary>Сборник - произведения разных авторов</summary>
    Collection,

    /// <summary>Периодика - журналы, газеты</summary>
    Periodical,

    /// <summary>Справочник - справочная литература</summary>
    ReferenceBook,

    /// <summary>Пособие - учебное пособие</summary>
    Tutorial,

    /// <summary>Диссертация - научная работа</summary>
    Dissertation,

    /// <summary>Автореферат - краткое изложение диссертации</summary>
    Abstract,

    Novel
}
