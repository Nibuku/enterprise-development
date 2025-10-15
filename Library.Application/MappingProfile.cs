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
    }

}
