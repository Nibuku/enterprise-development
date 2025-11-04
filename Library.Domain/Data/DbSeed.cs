using Library.Domain.Models;
using MongoDB.Driver;

namespace Library.Domain.Data;

public class DbSeed(IMongoClient mongoClient)
{
    private readonly IMongoDatabase _database = mongoClient.GetDatabase("library");

    public async Task Seed(CancellationToken token)
    {
        var collectionsCursor = await _database.ListCollectionNamesAsync(cancellationToken: token);
        var collections = await collectionsCursor.ToListAsync(token);
        var existCollections = collections.ToHashSet();

        if (!existCollections.Contains("Publishers"))
        {
            var publishersCollection = _database.GetCollection<Publisher>("Publishers");
            await publishersCollection.InsertManyAsync(DataSeed.Publishers, cancellationToken: token);
        }

        if (!existCollections.Contains("PublicationTypes"))
        {
            var typesCollection = _database.GetCollection<PublicationType>("Types");
            await typesCollection.InsertManyAsync(DataSeed.PublicationTypes, cancellationToken: token);
        }

        if (!existCollections.Contains("Books"))
        {
            var booksCollection = _database.GetCollection<Book>("Books");
            await booksCollection.InsertManyAsync(DataSeed.Books, cancellationToken: token);
        }

        if (!existCollections.Contains("Readers"))
        {
            var readersCollection = _database.GetCollection<BookReader>("Readers");
            await readersCollection.InsertManyAsync(DataSeed.Readers, cancellationToken: token);
        }

        if (!existCollections.Contains("Checkouts"))
        {
            var checkoutsCollection = _database.GetCollection<BookCheckout>("Checkouts");
            await checkoutsCollection.InsertManyAsync(DataSeed.Checkouts, cancellationToken: token);
        }
    }
}
