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
            new() {Id=1, InventoryNumber="INV-001", CatalogCode="FIC-DOS-001", Title="������������ � ���������", PublicationType=PublicationType.Novel, Publisher=Publisher.AST, PublicationYear=1866, Authors=new List<string>{"����������� �.�."}},
            new() {Id=2, InventoryNumber="INV-002", CatalogCode="FIC-TOL-001", Title="����� � ���", PublicationType=PublicationType.Novel, Publisher=Publisher.Eksmo, PublicationYear=1869, Authors=new List<string>{"������� �.�."}},
            new() {Id=3, InventoryNumber="INV-003", CatalogCode="FIC-BUL-001", Title="������ � ���������", PublicationType=PublicationType.Novel, Publisher=Publisher.AST, PublicationYear=1967, Authors=new List<string>{"�������� �.�."}},
            new() {Id=4, InventoryNumber="INV-004", CatalogCode="SCI-001", Title="������ ��� �����", PublicationType=PublicationType.Textbook, Publisher=Publisher.Piter, PublicationYear=2023, Authors=new List<string>{"������ �.�.", "������� �.�."}},
            new() {Id=5, InventoryNumber="INV-005", CatalogCode="SCI-002", Title="�����. 10 �����", PublicationType=PublicationType.Textbook, Publisher=Publisher.Prosveshchenie, PublicationYear=2022, Authors=new List<string>{"������� �.�."}},
            new() {Id=6, InventoryNumber="INV-006", CatalogCode="REF-001", Title="������� ����������������� �������", PublicationType=PublicationType.ReferenceBook, Publisher=Publisher.AST, PublicationYear=2020, Authors=new List<string>{"��������"}},
            new() {Id=7, InventoryNumber="INV-007", CatalogCode="PER-001", Title="������ '����� � �����' �1", PublicationType=PublicationType.Periodical, Publisher=Publisher.Nauka, PublicationYear=2024, Authors=new List<string>{"��������� �������"}},
            new() {Id=8, InventoryNumber="INV-008", CatalogCode="COL-001", Title="������� ������� ������", PublicationType=PublicationType.Collection, Publisher=Publisher.MGU, PublicationYear=2023, Authors=new List<string>{"��������� ������"}},
            new() {Id=9, InventoryNumber="INV-009", CatalogCode="TUT-001", Title="���������������� �� C#", PublicationType=PublicationType.Tutorial, Publisher=Publisher.DMKPress, PublicationYear=2024, Authors=new List<string>{"���� ��."}},
            new() {Id=10, InventoryNumber="INV-010", CatalogCode="DIS-001", Title="������������ ����������", PublicationType=PublicationType.Dissertation, Publisher=Publisher.MIPT, PublicationYear=2023, Authors=new List<string>{"������ �.�."}},
        };

        var readers = new List<Reader>
        {

            new() {Id=1, FullName="������ ���� ��������", Address="��. �������, �. 10, ��. 5", Phone="+79161234567", RegistrationDate=new DateTime(2023,1,15)},
            new() {Id=2, FullName="������� ����� ���������", Address="��. ������, �. 25, ��. 12", Phone="+79162345678", RegistrationDate=new DateTime(2023,2,20)},
            new() {Id=3, FullName="������� ������� ������������", Address="��. ��������, �. 8, ��. 3", Phone="+79163456789", RegistrationDate=new DateTime(2023,3,10)},
            new() {Id=4, FullName="������� ��������� ����������", Address="��. ���������, �. 15, ��. 7", Phone="+79164567890", RegistrationDate=new DateTime(2023,4,5)},
            new() {Id=5, FullName="�������� ������� ��������", Address="��. ����, �. 30, ��. 9", Phone="+79165678901", RegistrationDate=new DateTime(2023,5,12)},
            new() {Id=6, FullName="�������� ����� ����������", Address="��. �������, �. 12, ��. 4", Phone="+79166789012", RegistrationDate=new DateTime(2023,6,8)},
            new() {Id=7, FullName="������� ������ ��������", Address="��. ������, �. 5, ��. 11", Phone="+79167890123", RegistrationDate=new DateTime(2023,7,25)},
            new() {Id=8, FullName="������� ���� ����������", Address="��. �����������, �. 17, ��. 6", Phone="+79168901234", RegistrationDate=new DateTime(2023,8,14)},
            new() {Id=9, FullName="������� ����� �������������", Address="��. �����������, �. 3, ��. 8", Phone="+79169012345", RegistrationDate=new DateTime(2023,9,30)},
            new() {Id=10, FullName="�������� ������� ����������", Address="��. ����������, �. 7, ��. 2", Phone="+79160123456", RegistrationDate=new DateTime(2023,10,22)},
            new() {Id=11, FullName="������ ����� ����������", Address="��. ����������, �. 9, ��. 10", Phone="+79161234560", RegistrationDate=new DateTime(2023,11,18)},
            new() {Id=12, FullName="�����ova ���� ��������", Address="��. ��������, �. 14, ��. 13", Phone="+79162345671", RegistrationDate=new DateTime(2023,12,5)}
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