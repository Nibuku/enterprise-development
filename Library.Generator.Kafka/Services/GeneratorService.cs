using Confluent.Kafka;
using Library.Application.Contracts.Dtos;

namespace Library.Generator.Kafka.Services;

/// <summary>
/// Сервис для отправки сообщений о выдачах книг.
/// /// </summary>
public class GeneratorService(IConfiguration configuration,IProducer<Guid, IList<CheckoutCreateDto>> producer,ILogger<GeneratorService> logger) : IProducerService
{
    private readonly string _topicName = configuration.GetSection("Kafka")["Topic"]
        ?? throw new KeyNotFoundException("TopicName section of Kafka is missing");

    /// <summary>
    /// Отправляет пакет DTO с информацией о выдачах книг.
    /// </summary>
    /// <param name="batch">Список DTO для отправки.</param>
    /// <exception cref="Exception">Выбрасывается при ошибке отправки сообщения в Kafka.</exception>
    public async Task SendAsync(IList<CheckoutCreateDto> batch)
    {
        var topic = configuration.GetSection("Kafka")["TopicName"];
        logger.LogInformation("Kafka Topic: {Topic}", topic);

        try
        {
            logger.LogInformation("Sending a batch of {count} checkouts to {topic}", batch.Count, _topicName);

            var message = new Message<Guid, IList<CheckoutCreateDto>>
            {
                Key = Guid.NewGuid(), 
                Value = batch
            };

            await producer.ProduceAsync(_topicName, message);

            logger.LogInformation("Batch of {count} checkouts sent successfully", batch.Count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occurred during sending a batch of {count} checkouts to {topic}", batch.Count, _topicName);
        }
    }

}
