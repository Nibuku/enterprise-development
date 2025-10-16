using AutoMapper;
using Library.Application.Dtos;
using Library.Domain.Models;
using Library.Infrastructure.Repositories;


namespace Library.Application.Services;
public class PublisherService(
    PublisherRepository publisherRepository,
    IMapper mapper) : IApplicationService<PublisherGetDto, PublisherCreateDto, int>
{
    public PublisherGetDto Create(PublisherCreateDto dto)
    {
        var publisher = mapper.Map<Publisher>(dto);
        publisherRepository.Create(publisher);

        return mapper.Map<PublisherGetDto>(publisher);
    }

    public PublisherGetDto Get(int dtoId)
    {
        var publisher = publisherRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Издательство с ID {dtoId} не найден.");

        return mapper.Map<PublisherGetDto>(publisher);
    }

    public List<PublisherGetDto> GetAll()
    {
        var publishers = publisherRepository.ReadAll();
        return mapper.Map<List<PublisherGetDto>>(publishers);
    }

    public PublisherGetDto Update(PublisherCreateDto dto, int dtoId)
    {
        var publisherToUpdate = publisherRepository.Read(dtoId) ?? throw new KeyNotFoundException($"Издательство с ID {dtoId} не найден.");
        mapper.Map(dto, publisherToUpdate);
        publisherRepository.Update(publisherToUpdate);
        return mapper.Map<PublisherGetDto>(publisherToUpdate);
    }

    public void Delete(int dtoId)
    {
        publisherRepository.Delete(dtoId);
    }
}
