using Confluent.Kafka;
using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;
using Library.Infrastructure.Kafka.Deserializers;

namespace Library.Infrastructure.Kafka;

/// <summary>
/// Сервис для чтения сообщений из Kafka и обработки данных о выдаче книг.
/// Сервис подписывается на указанный топик Kafka и получает сообщения типа <see cref="IList{CheckoutCreateDto}"/>.
/// </summary>
public class KafkaConsumer : BackgroundService
{
    private readonly IConsumer<Guid, IList<CheckoutCreateDto>> _consumer;
    private readonly ILogger<KafkaConsumer> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    /// <summary>
    /// Создает экземпляр KafkaConsumer и настраивает подключение к Kafka.
    /// </summary>
    /// <param name="configuration">Конфигурация с настройками Kafka.</param>
    /// <param name="logger">Логгер</param>
    /// <param name="keyDeserializer">Десериализатор ключей сообщений Kafka.</param>
    /// <param name="valueDeserializer">Десериализатор значений сообщений Kafka.</param>
    /// <param name="scopeFactory">Фабрика областей внедрения зависимостей для сервисов.</param>
    public KafkaConsumer(
        IConfiguration configuration,
        ILogger<KafkaConsumer> logger,
        KeyDeserializer keyDeserializer,
        ValueDeserializer valueDeserializer,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;

        var kafkaConfig = configuration.GetSection("Kafka");

        var topicName = kafkaConfig["Topic"] 
            ?? throw new KeyNotFoundException("Topic is missing");

        var bootstrapServers = configuration.GetConnectionString("library-kafka") 
            ?? throw new KeyNotFoundException("ConnectionString 'library-kafka' is missing");

        var groupId = kafkaConfig["GroupId"] 
            ?? throw new KeyNotFoundException("GroupId is missing");

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = bootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true,
            AllowAutoCreateTopics = true
        };

        _consumer = new ConsumerBuilder<Guid, IList<CheckoutCreateDto>>(consumerConfig)
            .SetKeyDeserializer(keyDeserializer)
            .SetValueDeserializer(valueDeserializer)
            .SetErrorHandler((_, e) => _logger.LogError("Kafka Error: {Reason}", e.Reason))
            .Build();

        _consumer.Subscribe(topicName);
        _logger.LogInformation("Subscribed to Kafka topic: {Topic}", topicName);
    }

    /// <summary>
    /// Метод, выполняющий бесконечный цикл чтения сообщений из Kafka.
    /// </summary>
    /// Метод считывает сообщения из Kafka и передает на обработку через ProcessMessageAsync.
    /// <param name="stoppingToken">Токен отмены.</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = _consumer.Consume(stoppingToken);

                if (result == null || result.IsPartitionEOF)
                    continue;

                _logger.LogInformation(
                    "Received message {Key} with {Count} checkouts (Partition: {Partition}, Offset: {Offset})",
                    result.Message.Key,
                    result.Message.Value.Count,
                    result.Partition,
                    result.Offset
                );

                await ProcessMessageAsync(result.Message.Key, result.Message.Value);
            }
            catch (ConsumeException ex)
            {
                _logger.LogError(ex, "Consume error: {Reason}", ex.Error.Reason);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Kafka consumer cancelled");
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in Kafka consumer");
                await Task.Delay(2000, stoppingToken);
            }
        }
        _logger.LogInformation("Kafka consumer stop");
    }

    /// <summary>
    /// Обрабатывает полученное сообщение из Kafka и сохраняет данные о выдаче книг.
    /// </summary>
    /// <param name="key">ID сообщения.</param>
    /// <param name="checkouts">Список DTO с данными о выдачах книг</param>
    private async Task ProcessMessageAsync(Guid key, IList<CheckoutCreateDto>? checkouts)
    {
        if (checkouts == null || checkouts.Count == 0)
        {
            _logger.LogWarning("Empty checkouts list for message {Key}", key);
            return;
        }
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var checkoutService = scope.ServiceProvider.GetRequiredService<IBookCheckoutService>();

            await checkoutService.ReceiveContract(checkouts);

            _logger.LogInformation("Processed message {Key}: {Count} checkouts saved", key, checkouts.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing message {Key}", key);
        }
    }
}
