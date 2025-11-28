using Confluent.Kafka;
using Library.Application.Contracts.Dtos;
using Polly;
using Polly.Retry;

namespace Library.Generator.Kafka.Services;

/// <summary>
/// Сервис для отправки сообщений о выдачах книг.
/// /// </summary>
public class GeneratorService : IProducerService
{
    private readonly string _topicName;
    private readonly ILogger _logger;
    private readonly IProducer<Guid, IList<CheckoutCreateDto>> _producer;
    private readonly AsyncRetryPolicy _retryPolicy;
    public GeneratorService(IConfiguration configuration,
                           IProducer<Guid, IList<CheckoutCreateDto>> producer,
                           ILogger<GeneratorService> logger)
    {
        _topicName = configuration.GetSection("Kafka")["Topic"]
                     ?? throw new KeyNotFoundException("TopicName section of Kafka is missing");

        _producer = producer;
        _logger = logger;

        _retryPolicy = Policy
            .Handle<ProduceException<Guid, IList<CheckoutCreateDto>>>()
            .Or<KafkaException>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(2),
                onRetry: (ex, delay, retry, ctx) =>
                {
                    _logger.LogWarning(ex, "Error: send to Kafka fail. Attempt to {Retry} after {Delay}", retry, delay.TotalSeconds);
                });
    }
    /// <summary>
    /// Отправляет пакет DTO с информацией о выдачах книг.
    /// </summary>
    /// <param name="batch">Список DTO для отправки.</param>
    /// <exception cref="Exception">Выбрасывается при ошибке отправки сообщения в Kafka.</exception>
    public async Task SendAsync(IList<CheckoutCreateDto> batch)
    {
        try
        {
            _logger.LogInformation("Sending a batch of {count} checkouts to {topic}", batch.Count, _topicName);
            await _retryPolicy.ExecuteAsync(async () =>
            {
                var message = new Message<Guid, IList<CheckoutCreateDto>>
                {
                    Key = Guid.NewGuid(),
                    Value = batch
                };

                await _producer.ProduceAsync(_topicName, message);

                _logger.LogInformation("Batch of {count} checkouts sent successfully", batch.Count);
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred during sending a batch of {count} checkouts to {topic}", batch.Count, _topicName);
        }
    }
}
