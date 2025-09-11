using Library.Domain.Enums;
using Library.Domain.Models;

namespace Library.Tests;

public class DomainTests
{
    [Fact]
    public void Test1()
    {
        var books = new List<Book>
        {
            new() {Id=1, InventoryNumber="INV-001", CatalogCode="FIC-DOS-001", Title="Преступление и наказание", PublicationType=PublicationType.Novel, Publisher=Publisher.AST, PublicationYear=1866, Authors=new List<string>{"Достоевский Ф.М."}},
            new() {Id=2, InventoryNumber="INV-002", CatalogCode="FIC-TOL-001", Title="Война и мир", PublicationType=PublicationType.Novel, Publisher=Publisher.Eksmo, PublicationYear=1869, Authors=new List<string>{"Толстой Л.Н."}},
            new() {Id=3, InventoryNumber="INV-003", CatalogCode="FIC-BUL-001", Title="Мастер и Маргарита", PublicationType=PublicationType.Novel, Publisher=Publisher.AST, PublicationYear=1967, Authors=new List<string>{"Булгаков М.А."}},
            new() {Id=4, InventoryNumber="INV-004", CatalogCode="SCI-001", Title="Физика для вузов", PublicationType=PublicationType.Textbook, Publisher=Publisher.Piter, PublicationYear=2023, Authors=new List<string>{"Петров И.И.", "Сидоров А.В."}},
            new() {Id=5, InventoryNumber="INV-005", CatalogCode="SCI-002", Title="Химия. 10 класс", PublicationType=PublicationType.Textbook, Publisher=Publisher.Prosveshchenie, PublicationYear=2022, Authors=new List<string>{"Иванова М.С."}},
            new() {Id=6, InventoryNumber="INV-006", CatalogCode="REF-001", Title="Большой энциклопедический словарь", PublicationType=PublicationType.ReferenceBook, Publisher=Publisher.AST, PublicationYear=2020, Authors=new List<string>{"редакция"}},
            new() {Id=7, InventoryNumber="INV-007", CatalogCode="PER-001", Title="Журнал 'Наука и жизнь' №1", PublicationType=PublicationType.Periodical, Publisher=Publisher.Nauka, PublicationYear=2024, Authors=new List<string>{"коллектив авторов"}},
            new() {Id=8, InventoryNumber="INV-008", CatalogCode="COL-001", Title="Сборник научных трудов", PublicationType=PublicationType.Collection, Publisher=Publisher.MGU, PublicationYear=2023, Authors=new List<string>{"различные авторы"}},
            new() {Id=9, InventoryNumber="INV-009", CatalogCode="TUT-001", Title="Программирование на C#", PublicationType=PublicationType.Tutorial, Publisher=Publisher.DMKPress, PublicationYear=2024, Authors=new List<string>{"Смит Дж."}},
            new() {Id=10, InventoryNumber="INV-010", CatalogCode="DIS-001", Title="Исследование алгоритмов", PublicationType=PublicationType.Dissertation, Publisher=Publisher.MIPT, PublicationYear=2023, Authors=new List<string>{"Козлов А.П."}},
        };

        var readers = new List<Reader>
        {

            new() {Id=1, FullName="Иванов Иван Иванович", Address="ул. Пушкина, д. 10, кв. 5", Phone="+79161234567", RegistrationDate=new DateTime(2023,1,15)},
            new() {Id=2, FullName="Петрова Мария Сергеевна", Address="пр. Ленина, д. 25, кв. 12", Phone="+79162345678", RegistrationDate=new DateTime(2023,2,20)},
            new() {Id=3, FullName="Сидоров Алексей Владимирович", Address="ул. Гагарина, д. 8, кв. 3", Phone="+79163456789", RegistrationDate=new DateTime(2023,3,10)},
            new() {Id=4, FullName="Козлова Екатерина Дмитриевна", Address="ул. Советская, д. 15, кв. 7", Phone="+79164567890", RegistrationDate=new DateTime(2023,4,5)},
            new() {Id=5, FullName="Николаев Дмитрий Петрович", Address="пр. Мира, д. 30, кв. 9", Phone="+79165678901", RegistrationDate=new DateTime(2023,5,12)},
            new() {Id=6, FullName="Андреева Ольга Викторовна", Address="ул. Садовая, д. 12, кв. 4", Phone="+79166789012", RegistrationDate=new DateTime(2023,6,8)},
            new() {Id=7, FullName="Федоров Сергей Иванович", Address="ул. Лесная, д. 5, кв. 11", Phone="+79167890123", RegistrationDate=new DateTime(2023,7,25)},
            new() {Id=8, FullName="Павлова Анна Михайловна", Address="пр. Космонавтов, д. 17, кв. 6", Phone="+79168901234", RegistrationDate=new DateTime(2023,8,14)},
            new() {Id=9, FullName="Семенов Игорь Александрович", Address="ул. Центральная, д. 3, кв. 8", Phone="+79169012345", RegistrationDate=new DateTime(2023,9,30)},
            new() {Id=10, FullName="Морозова Татьяна Валерьевна", Address="ул. Молодежная, д. 7, кв. 2", Phone="+79160123456", RegistrationDate=new DateTime(2023,10,22)},
            new() {Id=11, FullName="Волков Павел Николаевич", Address="пр. Строителей, д. 9, кв. 10", Phone="+79161234560", RegistrationDate=new DateTime(2023,11,18)},
            new() {Id=12, FullName="Романova Юлия Олеговна", Address="ул. Школьная, д. 14, кв. 13", Phone="+79162345671", RegistrationDate=new DateTime(2023,12,5)}
        };

        var bookloans = new List<BookLoan>
        {
            new() {Id=1, BookId=1, ReaderId=1, LoanDays=14, LoanDate=new DateTime(2024,1,10)},
            new() {Id=2, BookId=2, ReaderId=2, LoanDays=21, LoanDate=new DateTime(2024,1,15)},
            new() {Id=3, BookId=3, ReaderId=3, LoanDays=7, LoanDate=new DateTime(2024,2,1)},
            new() {Id=4, BookId=4, ReaderId=4, LoanDays=30, LoanDate=new DateTime(2024,2,10)},
            new() {Id=5, BookId=5, ReaderId=5, LoanDays=14, LoanDate=new DateTime(2024,3,5)},
            new() {Id=6, BookId=6, ReaderId=6, LoanDays=60, LoanDate=new DateTime(2024,3,15)},
            new() {Id=7, BookId=7, ReaderId=7, LoanDays=10, LoanDate=new DateTime(2024,4,1)},
            new() {Id=8, BookId=8, ReaderId=8, LoanDays=45, LoanDate=new DateTime(2024,4,12)},
            new() {Id=9, BookId=9, ReaderId=9, LoanDays=21, LoanDate=new DateTime(2024,5,3)},
            new() {Id=10, BookId=10, ReaderId=10, LoanDays=90, LoanDate=new DateTime(2024,5,20)},
            new() {Id=11, BookId=11, ReaderId=11, LoanDays=14, LoanDate=new DateTime(2024,6,7)},
            new() {Id=12, BookId=12, ReaderId=12, LoanDays=30, LoanDate=new DateTime(2024,6,15)},
            new() {Id=13, BookId=13, ReaderId=1, LoanDays=21, LoanDate=new DateTime(2024,7,1)},
            new() {Id=14, BookId=14, ReaderId=2, LoanDays=7, LoanDate=new DateTime(2024,7,10)},
            new() {Id=15, BookId=15, ReaderId=3, LoanDays=14, LoanDate=new DateTime(2024,8,5)}
        };



    }
}