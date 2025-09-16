# Разработка корпоративных приложений
[Таблица с успеваемостью](https://docs.google.com/spreadsheets/d/1JD6aiOG6r7GrA79oJncjgUHWtfeW4g_YZ9ayNgxb_w0/edit?usp=sharing)

## Задание «Библиотека»
В базе данных библиотеки хранятся сведения о каталоге книг, читателях и выданных на руки книгах.

На каждую книгу в библиотеке заведена карточка, содержащая инвентарный номер, шифр в алфавитном каталоге, инициалы и фамилии авторов, название, вид издания, издательство, год издания. 
Вид издания является справочником.
Издательство является справочником.

Читатель характеризуется ФИО, адресом, телефоном, датой регистрации. 

При выдаче книги читателю отмечается дата выдачи и количество дней, на которое выдается книга.

### Классы
* [Author](https://github.com/Nibuku/enterprise-development/blob/main/Library.Domain/Models/Author.cs)- класс хранит информацию об авторе и спиок его книг
* [Book](https://github.com/Nibuku/enterprise-development/blob/main/Library.Domain/Models/Book.cs) - класс описывает карточку книги в соответствии с вариантом
* [BookLoan](https://github.com/Nibuku/enterprise-development/blob/main/Library.Domain/Models/BookLoan.cs) - хранит информацию о выдачи конкретной книги читателю
* [Reader](https://github.com/Nibuku/enterprise-development/blob/main/Library.Domain/Models/Reader.cs) - характеризует читателя, а также хранит список книг, взятых им

### Enum
* [PublicationType](https://github.com/Nibuku/enterprise-development/blob/main/Library.Domain/Enums/PublicationType.cs) - содержит типы книг (справочник, журнал, новела и др.)
* [Publisher](https://github.com/Nibuku/enterprise-development/blob/main/Library.Domain/Enums/Publisher.cs) - содержит издательства
  
### Тесты
[LibraryTests](https://github.com/Nibuku/enterprise-development/blob/main/Library.Tests/LibraryTests.cs) - заданные по варианту юнит-тесты
1. Books_OrderedByTitle() - Вывести информацию о выданных книгах, упорядоченных по названию.
2. Top5Readers_ByNumberOfBooks() - Вывести информацию о топ 5 читателей, прочитавших больше всего книг за заданный период.
3. Top5Readers_ByTotalLoanDays() - Вывести информацию о читателях, бравших книги на наибольший период времени, упорядочить по ФИО.
4. Top5PopularPublishers_LastYear() - Вывести топ 5 наиболее популярных издательств за последний год.
5. Top5LeastPopularBooks_LastYear() - Вывести топ 5 наименее популярных книг за последний год.
