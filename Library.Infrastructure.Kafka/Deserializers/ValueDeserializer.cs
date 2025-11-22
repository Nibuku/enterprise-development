using Confluent.Kafka;
using Library.Application.Contracts.Dtos;
using System.Text.Json;

namespace Library.Infrastructure.Kafka.Deserializers;
public class ValueDeserializer: IDeserializer<IList<CheckoutCreateDto>>
{
    public IList<CheckoutCreateDto> Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull) return [];
        return JsonSerializer.Deserialize<IList<CheckoutCreateDto>>(data) ?? [];
    }
}
