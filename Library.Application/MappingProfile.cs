using AutoMapper;
using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Dtos.AnaliticsDtos;
using Library.Domain.Models;

namespace Library.Application;

/// <summary>
/// AutoMapper для преобразования между DTO и доменом.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Создает новый экземпляр профиля маппинга.
    /// </summary>
    public MappingProfile()
    {
        CreateMap<BookCreateDto, Book>().ReverseMap();
        CreateMap<BookGetDto, Book>().ReverseMap();

        CreateMap<BookReaderCreateDto, BookReader>().ReverseMap();
        CreateMap<BookReaderGetDto, BookReader>().ReverseMap();

        CreateMap<CheckoutCreateDto, BookCheckout>().ReverseMap();
        CreateMap<CheckoutGetDto, BookCheckout>().ReverseMap();

        CreateMap<PublisherCreateDto, Publisher>().ReverseMap();
        CreateMap<PublisherGetDto, Publisher>().ReverseMap();

        CreateMap<PublicationTypeCreateDto, PublicationType>().ReverseMap();
        CreateMap<PublicationTypeGetDto, PublicationType>().ReverseMap();

        CreateMap<Book, BookWithCountDto>().ReverseMap();
        CreateMap<BookReader, BookReaderWithCountDto>().ReverseMap();
        CreateMap<BookReader, BookReaderWithDaysDto>().ReverseMap();
    }

}
