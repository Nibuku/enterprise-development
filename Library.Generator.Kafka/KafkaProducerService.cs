using Library.Generator.Kafka.Services;

namespace Library.Generator.Kafka;

/// <summary>
/// Служба для генерации и отправки заданного числа контрактов через заданные интервалы
/// </summary>
/// <param name="configuration">Конфигурация</param>
/// <param name="logger">Логгер</param>
public class KafkaProducerService: BackgroundService
{
    private readonly int _batchSize;
    private readonly int _payloadLimit;
    private readonly int _waitTime;
    private readonly IProducerService _producer;
    private readonly ILogger<KafkaProducerService> _logger;

    /// <summary>
    /// Конструктор сервиса KafkaProducerService.
    /// Заполняет параметры генерации сообщений
    /// </summary>
    /// <param name="configuration">IConfiguration для получения параметров генератора</param>
    /// <param name="producer">Сервис продюсера Kafka.</param>
    /// <param name="logger">Логгер</param>
    /// <exception cref="ArgumentException">Выбрасывается при некоррекных параметрах генератора их appsetting.json</exception>
    public KafkaProducerService(IConfiguration configuration, IProducerService producer, ILogger<KafkaProducerService> logger)
    {
        _batchSize = configuration.GetValue<int>("Generator:BatchSize");
        _payloadLimit = configuration.GetValue<int>("Generator:PayloadLimit");
        _waitTime = configuration.GetValue<int>("Generator:WaitTime");

        if (_batchSize <= 0)
            throw new ArgumentException($"Invalid argument value for BatchSize: {_batchSize}");
        if (_payloadLimit <= 0)
            throw new ArgumentException($"Invalid argument value for PayloadLimit: {_payloadLimit}");
        if (_waitTime <= 0)
            throw new ArgumentException($"Invalid argument value for WaitTime: {_waitTime}");

        _producer = producer;
        _logger = logger;
    }

    /// <summary>
    /// Метод, который генерирует и отправляет партии DTO выдач книг через заданные интервалы до достижения лимита.
    /// </summary>
    /// <param name="stoppingToken">Токен отмены</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting to send {total} messages with {time}s interval with {batch} messages in batch", _payloadLimit, _waitTime, _batchSize);
        var counter = 0;
        while (counter < _payloadLimit && !stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _producer.SendAsync(BookCheckoutGenerator.GenerateLinks(_batchSize));
                counter += _batchSize;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Send batch with error. Retry");
            }
            await Task.Delay(_waitTime, stoppingToken);
        }
        _logger.LogInformation("Finished sending {total} messages with {time}s interval with {batch} messages in batch", _payloadLimit, _waitTime, _batchSize);
    }
}