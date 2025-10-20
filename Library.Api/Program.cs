using AutoMapper;
using Library.Application;
using Library.Application.Services;
using Library.Application.Contracts.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Infrastructure.InMemory.Repositories;
using Library.Application.Contracts.Dtos;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(
    config => config.AddProfile(new MappingProfile()),
    LoggerFactory.Create(builder => builder.AddConsole()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IRepository<Book, int>, BookRepository>();
builder.Services.AddSingleton<IRepository<BookReader, int>, BookReaderRepository>();
builder.Services.AddSingleton<IRepository<BookCheckout, int>, BookCheckoutRepository>();
builder.Services.AddSingleton<IRepository<Publisher, int>, PublisherRepository>();
builder.Services.AddSingleton<IRepository<PublicationType, int>, PublicationTypeRepository>();

builder.Services.AddScoped<IApplicationService<BookGetDto, BookCreateDto, int>, BookService>();
builder.Services.AddScoped<IApplicationService<BookReaderGetDto, BookReaderCreateDto, int>, BookReaderService>();
builder.Services.AddScoped<IApplicationService<PublisherGetDto, PublisherCreateDto, int>, PublisherService>();
builder.Services.AddScoped<IApplicationService<CheckoutGetDto, CheckoutCreateDto, int>, BookCheckoutService>();
builder.Services.AddScoped<IApplicationService<PublicationTypeGetDto, PublicationTypeCreateDto, int>, PublicationTypeService>(); 
builder.Services.AddScoped<ILibraryAnalyticsService, LibraryAnalyticsService>();

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
