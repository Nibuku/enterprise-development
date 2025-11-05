using Library.Domain.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.Application.Services;

/// <summary>
/// Сервис для заполнения базы данных начальными данными.
/// </summary>
public class DbService(IServiceProvider serviceProvider) : IHostedService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    /// <summary>
    /// Запускает инициализацию базы данных при старте приложения.
    /// </summary>
    public async Task StartAsync(CancellationToken cancellationToken)
    {

        using var scope = _serviceProvider.CreateScope();
        var dbSeed = scope.ServiceProvider.GetRequiredService<DbSeed>();
        await dbSeed.Seed(cancellationToken);
    }

    /// <summary>
    /// Метод выполняется при остановке приложения.
    /// </summary>
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}