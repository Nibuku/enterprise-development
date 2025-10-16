using AutoMapper;
using Library.Application.Dtos;
using Library.Domain.Models;
using Library.Infrastructure.Repositories;


namespace Library.Application.Services;
public class PublicationTypeService(
    PublicationTypeRepository typeRepository,
    IMapper mapper) : IApplicationService<PublicationTypeGetDto, PublicationTypeCreateDto, int>
{
    public PublicationTypeGetDto Create(PublicationTypeCreateDto dto)
    {
        var type = mapper.Map<PublicationType>(dto);
        typeRepository.Create(type);

        return mapper.Map<PublicationTypeGetDto>(type);
    }

    public PublicationTypeGetDto Get(int dtoId)
    {
        var type = typeRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Тип издания с ID {dtoId} не найден.");

        return mapper.Map<PublicationTypeGetDto>(type);
    }

    public List<PublicationTypeGetDto> GetAll()
    {
        var types = typeRepository.ReadAll();
        return mapper.Map<List<PublicationTypeGetDto>>(types);
    }

    public PublicationTypeGetDto Update(PublicationTypeCreateDto dto, int dtoId)
    {
        var typeToUpdate = typeRepository.Read(dtoId) ?? throw new KeyNotFoundException($"Тип издания с ID {dtoId} не найден.");
        mapper.Map(dto, typeToUpdate);
        typeRepository.Update(typeToUpdate);
        return mapper.Map<PublicationTypeGetDto>(typeToUpdate);
    }

    public void Delete(int dtoId)
    {
        typeRepository.Delete(dtoId);
    }
}
