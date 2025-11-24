using AutoMapper;
using Library.Application;
using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;
using Library.Application.Services;
using Library.Domain.Data;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Infrastructure.Mongo;
using Library.Infrastructure.Mongo.Repositories;
using Library.ServiceDefaults;
using MongoDB.Driver;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var mapperConfig = new MapperConfiguration(
    config => config.AddProfile(new MappingProfile()),
    LoggerFactory.Create(builder => builder.AddConsole()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IRepositoryAsync<Book, int>, BookMongoRepository>();
builder.Services.AddScoped<IRepositoryAsync<BookReader, int>, BookReaderMongoRepository>();
builder.Services.AddScoped<IRepositoryAsync<BookCheckout, int>, BookCheckoutMongoRepository>();
builder.Services.AddScoped<IRepositoryAsync<Publisher, int>, PublisherMongoRepository>();
builder.Services.AddScoped<IRepositoryAsync<PublicationType, int>, TypeMongoRepository>();

builder.Services.AddScoped<IApplicationService<BookGetDto, BookCreateDto, int>, BookService>();
builder.Services.AddScoped<IApplicationService<BookReaderGetDto, BookReaderCreateDto, int>, BookReaderService>();
builder.Services.AddScoped<IApplicationService<PublisherGetDto, PublisherCreateDto, int>, PublisherService>();
builder.Services.AddScoped<IApplicationService<PublicationTypeGetDto, PublicationTypeCreateDto, int>, PublicationTypeService>(); 
builder.Services.AddScoped<ILibraryAnalyticsService, LibraryAnalyticsService>();
builder.Services.AddScoped<IBookCheckoutService, BookCheckoutService>();

builder.Services.AddScoped<DbSeed>();
builder.Services.AddHostedService<DbService>();

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
