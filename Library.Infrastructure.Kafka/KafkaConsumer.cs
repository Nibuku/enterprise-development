using Confluent.Kafka;
using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;
using Library.Infrastructure.Kafka.Deserializers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class KafkaConsumer : BackgroundService
{
    private readonly IConsumer<Guid, IList<CheckoutCreateDto>> _consumer;
    private readonly ILogger<KafkaConsumer> _logger;

    public KafkaConsumer(
        IConfiguration configuration,
        ILogger<KafkaConsumer> logger,
        KeyDeserializer keyDeserializer,
        ValueDeserializer valueDeserializer)
    {
        _logger = logger;

        var kafkaConfig = configuration.GetSection("Kafka");

        var topicName = kafkaConfig["Topic"]
                        ?? throw new KeyNotFoundException("Kafka:Topic is missing");

        var bootstrapServers = configuration.GetConnectionString("library-kafka")
                           ?? throw new KeyNotFoundException("Aspire ConnectionString 'library-kafka' is missing");

        var groupId = kafkaConfig["GroupId"]
                      ?? throw new KeyNotFoundException("Kafka:GroupId is missing");

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

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Kafka consumer started");

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
                    result.Message.Value?.Count ?? 0,
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

        _logger.LogInformation("Kafka consumer stopped");
    }

    private Task ProcessMessageAsync(Guid key, IList<CheckoutCreateDto> checkouts)
    {
        if (checkouts == null || checkouts.Count == 0)
        {
            _logger.LogWarning("Empty checkouts list for message {Key}", key);
            return Task.CompletedTask;
        }

        var count = 0;
        foreach (var dto in checkouts)
        {
            _logger.LogInformation("Stub process checkout: BookId={BookId}, ReaderId={ReaderId}",
                dto.BookId, dto.ReaderId);
            count++;
        }

        _logger.LogInformation("Processed message {Key}: {Count} checkouts logged", key, count);
        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        try
        {
            _consumer?.Close();
            _consumer?.Dispose();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error disposing Kafka consumer");
        }

        base.Dispose();
    }
}
