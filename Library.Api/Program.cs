using AutoMapper;
using Library.Application.Dtos;
using Library.Application.Services;
using Library.Infrastructure.Repositories;
using Library.Application;
using Library.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(
    config => config.AddProfile(new MappingProfile()),
    LoggerFactory.Create(builder => builder.AddConsole()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddSingleton<BookRepository, BookRepository>();
builder.Services.AddSingleton<BookReaderRepository, BookReaderRepository>();
builder.Services.AddSingleton<BookCheckoutRepository, BookCheckoutRepository>();
builder.Services.AddSingleton<PublisherRepository, PublisherRepository>();
builder.Services.AddSingleton<PublicationTypeRepository, PublicationTypeRepository>();

builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<BookReaderService>();
builder.Services.AddScoped<PublisherService>();
builder.Services.AddScoped<BookCheckoutService>();
builder.Services.AddScoped<PublicationTypeService>(); 
builder.Services.AddScoped<ILibraryAnalyticsService, LibraryAnalyticsService>();

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
