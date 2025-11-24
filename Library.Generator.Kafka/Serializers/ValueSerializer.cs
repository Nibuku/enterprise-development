using Library.Application.Contracts.Dtos;
using Confluent.Kafka;
using System.Text.Json;

namespace Library.Generator.Kafka.Serializers;

/// <summary>
/// Сериализатор для значения Kafka.
/// </summary>
public class ValueSerializer: ISerializer<IList<CheckoutCreateDto>>
{
    public byte[] Serialize(IList<CheckoutCreateDto> list, SerializationContext context) => JsonSerializer.SerializeToUtf8Bytes(list);
}
