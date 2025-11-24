using AutoMapper;
using Library.Application;
using Library.Application.Contracts.Interfaces;
using Library.Application.Services;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Infrastructure.Kafka;
using Library.Infrastructure.Kafka.Deserializers;
using Library.Infrastructure.Mongo;
using Library.Infrastructure.Mongo.Repositories;
using Library.ServiceDefaults;
using MongoDB.Driver;

var builder = Host.CreateApplicationBuilder(args);

var mapperConfig = new MapperConfiguration(
    config => config.AddProfile(new MappingProfile()),
    LoggerFactory.Create(builder => builder.AddConsole()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.AddServiceDefaults();
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("library");
    return new MongoClient(connectionString);
});

builder.Services.AddSingleton(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var database = client.GetDatabase("library");
    return new MongoDbContext(database);
});
builder.Services.AddHostedService<KafkaConsumer>();

builder.Services.AddSingleton<KeyDeserializer>();
builder.Services.AddSingleton<ValueDeserializer>();
builder.Services.AddHostedService<KafkaConsumer>();
builder.Services.AddScoped<IRepositoryAsync<Book, int>, BookMongoRepository>();
builder.Services.AddScoped<IRepositoryAsync<BookReader, int>, BookReaderMongoRepository>();
builder.Services.AddScoped<IRepositoryAsync<BookCheckout, int>, BookCheckoutMongoRepository>();

builder.Services.AddScoped<IBookCheckoutService, BookCheckoutService>();


var host = builder.Build();
host.Run();