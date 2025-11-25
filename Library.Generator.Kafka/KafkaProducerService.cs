using Library.Generator.Kafka.Services;

namespace Library.Generator.Kafka;

/// <summary>
/// Служба для генерации и отправки заданного числа контрактов через заданные интервалы
/// </summary>
/// <param name="configuration">Конфигурация</param>
/// <param name="scopeFactory">Фабрика контекста</param>
/// <param name="logger">Логгер</param>
public class KafkaProducerService(IConfiguration configuration, IServiceScopeFactory scopeFactory, ILogger<KafkaProducerService> logger) : BackgroundService
{
    private readonly string _batchSize = configuration.GetSection("Generator")["BatchSize"] 
        ?? throw new KeyNotFoundException("BatchSize section of Generator is missing");
    private readonly string _payloadLimit = configuration.GetSection("Generator")["PayloadLimit"] 
        ?? throw new KeyNotFoundException("PayloadLimit section of Generator is missing");
    private readonly string _waitTime = configuration.GetSection("Generator")["WaitTime"] 
        ?? throw new KeyNotFoundException("WaitTime section of Generator is missing");

    /// <summary>
    /// Метод, который генерирует и отправляет партии DTO выдач книг через заданные интервалы до достижения лимита.
    /// </summary>
    /// <param name="stoppingToken">Токен отмены</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Starting to send {total} messages with {time}s interval with {batch} messages in batch", _payloadLimit, _waitTime, _batchSize);

        if (!int.TryParse(_batchSize, out var batchSize)) 
            throw new FormatException("Unable to parse BatchSize");
        if (!int.TryParse(_payloadLimit, out var payloadLimit)) 
            throw new FormatException("Unable to parse PayloadLimit");
        if (!int.TryParse(_waitTime, out var waitTime)) 
            throw new FormatException("Unable to parse WaitTime");

        var counter = 0;
        while (counter < payloadLimit)
        {
            using var scope = scopeFactory.CreateScope();
            var producer = scope.ServiceProvider.GetRequiredService<IProducerService>();

            await producer.SendAsync(BookChechoutGenerator.GenerateLinks(batchSize));
            await Task.Delay(waitTime * 1000, stoppingToken);
            counter += batchSize;
        }
        logger.LogInformation("Finished sending {total} messages with {time}s interval with {batch} messages in batch", _payloadLimit, _waitTime, _batchSize);
    }
}