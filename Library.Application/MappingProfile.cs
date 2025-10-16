using AutoMapper;
using Library.Application.Dtos;
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
    }

}
