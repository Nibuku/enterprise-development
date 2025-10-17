using AutoMapper;
using Library.Application.Dtos;
using Library.Application.Dtos.AnaliticsDtos;
using Library.Domain.Models;

namespace Library.Application;
public class MappingProfile : Profile
{
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


        CreateMap<Book, BookWithCountDto>()
            // Поле Count игнорируем, т.к. оно устанавливается вручную в сервисе.
            .ForMember(dest => dest.Count, opt => opt.Ignore());

        // Для GetTopReadersByPeriod
        // Маппинг BookReader -> BookReaderWithCountDto
        CreateMap<BookReader, BookReaderWithCountDto>()
            .ForMember(dest => dest.Count, opt => opt.Ignore());

        // Для GetLongestBorrowers
        // Маппинг BookReader -> BookReaderWithDaysDto (или BookReaderWithDayDto, если имя отличается)
        CreateMap<BookReader, BookReaderWithDaysDto>() // ИЛИ BookReaderWithDayDto, проверьте имя
            .ForMember(dest => dest.TotalDays, opt => opt.Ignore());
    }

}
