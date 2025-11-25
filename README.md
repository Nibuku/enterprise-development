# Разработка корпоративных приложений
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

[LibraryFixture](https://github.com/Nibuku/enterprise-development/Library.Tests/LibraryFixture.cs) - фикстура, использующая для заполнения репозитории.

### Library.Infrastructure.InMemory - Слой для доступа к данным, которые храняться в памяти
- **Repositories** - Реализации репозиториев:
  - [BookRepository.cs](./Library.Infrastructure/Repositories/BookRepository.cs)
  - [BookReaderRepository.cs](./Library.Infrastructure/Repositories/BookReaderRepository.cs) 
  - [BookCheckoutRepository.cs](./Library.Infrastructure/Repositories/BookCheckoutRepository.cs)
  - [PublisherRepository.cs](./Library.Infrastructure/Repositories/PublisherRepository.cs)
  - [PublicationTypeRepository.cs](./Library.Infrastructure/Repositories/PublicationTypeRepository.cs)

### Library.Infrastructure.Mongo - Слой для доступа к данным, используется база данных MongoDB
- **Repositories** - Реализации репозиториев:
  - [BookMongoRepository.cs](./Library.Infrastructure.Mongo/Repositories/BookMongoRepository.cs)
  - [BookReaderMongoRepository.cs](./Library.Infrastructure.Mongo/Repositories/BookReaderMongoRepository.cs) 
  - [BookCheckoutMongoRepository.cs](./Library.Infrastructure.Mongo/Repositories/BookCheckoutMongoRepository.cs)
  - [PublisherMongoRepository.cs](./Library.Infrastructure.Mongo/Repositories/PublisherMongoRepository.cs)
  - [TypeMongoRepository.cs](./Library.Infrastructure.Mongo/Repositories/TypeMongoRepository.cs)
	
- [MongoDbContext.cs](./Library.Infrastructure.Mongo/MongoDbContext.cs) - контекст базы данных MongoDB


### Library.Application - Сервисный слой
#### Dtos
- AnaliticsDtos - DTO для аналитических запросов:
  - [BookReaderWithCountDto.cs](./Library.Application/Dtos/BookReaderWithCountDto.cs) - Читатель с количеством книг, которые он прочитал.
  - [BookReaderWithDaysDto.cs](./Library.Application/Dtos/BookReaderWithDaysDto.cs) - Читатель с количеством дней, которые у него были книги на руках.
  - [BookWithCountDto.cs](./Library.Application/Dtos/BookWithCountDto.cs) - Книга с счетчиком выдач.  
  - [PublisherCountDto.cs](./Library.Application/Dtos/PublisherCountDto.cs) - Издательство с счетчиком книг.

- DTO:
  - [BookCreateDto.cs](./Library.Application/Dtos/BookCreateDto.cs) / [BookGetDto.cs](./Library.Application/Dtos/BookGetDto.cs) - Для создания и получения книг.
  - [BookReaderCreateDto.cs](./Library.Application/Dtos/BookReaderCreateDto.cs) / [BookReaderGetDto.cs](./Library.Application/Dtos/BookReaderGetDto.cs) - Для читателей.
  - [CheckoutCreateDto.cs](./Library.Application/Dtos/CheckoutCreateDto.cs) / [CheckoutGetDto.cs](./Library.Application/Dtos/CheckoutGetDto.cs) - Для выдачи книг.
  - [PublicationTypeCreateDto.cs](./Library.Application/Dtos/PublicationTypeCreateDto.cs) / [PublicationTypeGetDto.cs](./Library.Application/Dtos/PublicationTypeGetDto.cs) - Для типов публикаций.
  - [PublisherCreateDto.cs](./Library.Application/Dtos/PublisherCreateDto.cs) / [PublisherGetDto.cs](./Library.Application/Dtos/PublisherGetDto.cs) - Для издательств.


#### Interfaces - Контракты сервисов
- [IApplicationService.cs](./Library.Application/Interfaces/IApplicationService.cs) - Интерфейс для CRUD операций.
- [ILibraryAnalyticsService.cs](./Library.Application/Interfaces/ILibraryAnalyticsService.cs) - Интерфейс для аналитической службы.

#### Services - Реализации сервисов с CRUD операциями
- [BookService.cs](./Library.Application/Services/BookService.cs) - Для книг.
- [BookReaderService.cs](./Library.Application/Services/BookReaderService.cs) - Для читателей.
- [BookCheckoutService.cs](./Library.Application/Services/BookCheckoutService.cs) - Для выдачи книг.
- [PublicationTypeService.cs](./Library.Application/Services/PublicationTypeService.cs) - Для типов публикаций.
- [PublisherService.cs](./Library.Application/Services/PublisherService.cs) - Для издательств.
- [LibraryAnalyticsService.cs](./Library.Application/Services/LibraryAnalyticsService.cs) - Сервис аналитических запросов.
- [DbService.cs](./Library.Application/Services/DbService.cs) - сервис для заполнения базы данных начальными значениями
- [MappingProfile.cs](./Library.Application/MappingProfile.cs) - Настройки AutoMapper для преобразования между DTO и доменной областью.


### Library.Api - Веб-API
#### Controllers - API контроллеры
- [AnalyticsController.cs](./Library.Api/Controllers/AnalyticsController.cs) - Контроллер для аналитических запросов (содержит те же запросы, которые проверяются в юнит-тестах).
- [BookController.cs](./Library.Api/Controllers/BookController.cs) - Управление книгами.
- [BookReaderController.cs](./Library.Api/Controllers/BookReaderController.cs) - Управление читателями.
- [BookCheckoutController.cs](./Library.Api/Controllers/BookCheckoutController.cs) - Управление выдачей книг.
- [PublicationTypeController.cs](./Library.Api/Controllers/PublicationTypeController.cs) - Управление типами публикаций.
- [PublisherController.cs](./Library.Api/Controllers/PublisherController.cs) - Управление издательствами.
- [CrudControllerBase.cs](./Library.Api/Controllers/CrudControllerBase.cs) - Базовый класс для CRUD операций.

### Library.Generator.Kafka - Генератор тестовых данных для Kafka. Создает и отправляет в Kafka-топик сообщения с данными о книжных выдачах (checkouts).

-[KafkaProducerService](.\Library.Generator.Kafka\KafkaProducerService.cs) -  продюсер Kafkи
-[BookCheckoutGenerator](.\Library.Generator.Kafka\BookChechoutGenerator.cs) - генератор данных о книжных выдачах

#### Services
-[GeneratorService](.\Library.Generator.Kafka\Services\GeneratorService.cs) - сервис генерации тестовых данных
-[IProducerService](.\Library.Generator.Kafka\Services\IProducerService.cs) - интерфейс для отправки сообщений в Kafka

#### Serializers:
-[KeySerializer.cs](.\Library.Generator.Kafka\Serializers\KeySerializer.cs) - сериализатор ключей сообщений (Guid)
-[ValueSerializer.cs](.\Library.Generator.Kafka\Serializers\ValueSerializer.cs) - сериализатор значений (списки CheckoutCreateDto)

- [Program.cs](.\Library.Generator.Kafka\Program.cs) - точка входа, регистрация сервисов

### Library.Infrastructure.Kafka - Потребитель для Kafka-сообщений

- [KafkaConsumer](.\Library.Infrastructure.Kafka\KafkaConsumer.cs)** - фоновый сервис для потребления сообщений из Kafka.
- 
#### Deserializers:
- [KeyDeserializer.cs](.\Library.Infrastructure.Kafka\Deserializers\KeyDeserializer.cs) - десериализатор ключей сообщений (Guid)
- [ValueDeserializer.cs](.\Library.Infrastructure.Kafka\Deserializers\ValueDeserializer.cs) - десериализатор значений (списки CheckoutCreateDto)

- [Program.cs](.\Library.Infrastructure.Kafka\Program.cs) - точка входа приложения. Регистрирует Kafka consumer как hosted service и настраивает зависимости.