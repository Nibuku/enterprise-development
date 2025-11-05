using Library.Domain.Models;
using MongoDB.Driver;

namespace Library.Domain.Data;

/// <summary>
/// Класс для инициализации базы данных. Заполняет коллекцию данными, если они еще не существуют.
/// </summary>
public class DbSeed(IMongoClient mongoClient)
{
    private readonly IMongoDatabase _database = mongoClient.GetDatabase("library");

    /// <summary>
    /// Создаёт коллекции и добавляет данные, если они ещё не существуют.
    /// </summary>
    /// <param name="token">Токен для отмены операции.</param>
    public async Task Seed(CancellationToken token)
    {
        var collectionsCursor = await _database.ListCollectionNamesAsync(cancellationToken: token);
        var collections = await collectionsCursor.ToListAsync(token);
        var existCollections = collections.ToHashSet();

        if (!existCollections.Contains("publishers"))
        {
            var publishersCollection = _database.GetCollection<Publisher>("publishers");
            await publishersCollection.InsertManyAsync(DataSeed.Publishers, cancellationToken: token);
        }

        if (!existCollections.Contains("types"))
        {
            var typesCollection = _database.GetCollection<PublicationType>("types");
            await typesCollection.InsertManyAsync(DataSeed.PublicationTypes, cancellationToken: token);
        }

        if (!existCollections.Contains("books"))
        {
            var booksCollection = _database.GetCollection<Book>("books");
            await booksCollection.InsertManyAsync(DataSeed.Books, cancellationToken: token);
        }

        if (!existCollections.Contains("readers"))
        {
            var readersCollection = _database.GetCollection<BookReader>("readers");
            await readersCollection.InsertManyAsync(DataSeed.Readers, cancellationToken: token);
        }

        if (!existCollections.Contains("checkouts"))
        {
            var checkoutsCollection = _database.GetCollection<BookCheckout>("checkouts");
            await checkoutsCollection.InsertManyAsync(DataSeed.Checkouts, cancellationToken: token);
        }
    }
}
