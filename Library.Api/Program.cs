using AutoMapper;
using Library.Application.Dtos;
using Library.Application.Services;
using Library.Infrastructure.Repositories;
using Library.Application;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(
    config => config.AddProfile(new MappingProfile()),
    LoggerFactory.Create(builder => builder.AddConsole()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<BookRepository, BookRepository>();
builder.Services.AddSingleton<BookReaderRepository, BookReaderRepository>();
builder.Services.AddSingleton<BookCheckoutRepository, BookCheckoutRepository>();
builder.Services.AddSingleton<PublisherRepository, PublisherRepository>();
builder.Services.AddSingleton<PublicationTypeRepository, PublicationTypeRepository>();

builder.Services.AddScoped<IApplicationService<BookGetDto, BookCreateDto, int>, BookService>();
builder.Services.AddScoped<IApplicationService<BookReaderGetDto, BookReaderCreateDto, int>, BookReaderService>();
builder.Services.AddScoped<IApplicationService<PublisherGetDto, PublisherCreateDto, int>, PublisherService>();
builder.Services.AddScoped<IApplicationService<CheckoutGetDto, CheckoutCreateDto, int>, BookCheckoutService>();
builder.Services.AddScoped<IApplicationService<PublicationTypeGetDto, PublicationTypeCreateDto, int>, PublicationTypeService>(); 
builder.Services.AddScoped<ILibraryAnalyticsService, LibraryAnalyticsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
