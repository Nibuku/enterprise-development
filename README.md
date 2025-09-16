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
* [Book](https://github.com/Nibuku/enterprise-development/blob/main/Library.Domain/Models/Book.cs) - класс описывает карточку книги в соответствии с вариантом
* [BookLoan](https://github.com/Nibuku/enterprise-development/blob/main/Library.Domain/Models/BookLoan.cs) - хранит информацию о выдачи конкретной книги читателю
* [Reader](https://github.com/Nibuku/enterprise-development/blob/main/Library.Domain/Models/Reader.cs) - характеризует читателя, а также хранит список книг, взятых им
* [PublicationType](https://github.com/Nibuku/enterprise-development/blob/main/Library.Domain/Models/PublicationType.cs) - содержит типы книг
* [Publisher](https://github.com/Nibuku/enterprise-development/blob/main/Library.Domain/Models/Publisher.cs) - содержит название издательства
  
### Тесты
[LibraryTests](https://github.com/Nibuku/enterprise-development/blob/main/Library.Tests/LibraryTests.cs) - заданные по варианту юнит-тесты
1. BooksOrderedByTitle() - Вывести информацию о выданных книгах, упорядоченных по названию.
2. TopReadersByNumberOfBooks() - Вывести информацию о топ 5 читателей, прочитавших больше всего книг за заданный период.
3. TopReadersByTotalLoanDays() - Вывести информацию о читателях, бравших книги на наибольший период времени, упорядочить по ФИО.
4. TopPopularPublishersLastYear() - Вывести топ 5 наиболее популярных издательств за последний год.
5. TopLeastPopularBooksLastYear() - Вывести топ 5 наименее популярных книг за последний год.
