using IOServer.Logic.IGrains;
using IOServer.Logic.Models;
using Microsoft.Extensions.Logging;
using Orleans.Providers;
using Orleans.Streams;

namespace IOServer.IOServer.Logic.Grains;
[StorageProvider(ProviderName = Const.FSMMemoryStorage)]
public class SubscriberGrain<T> : Grain, ISubscriberGrain<T>
{
    private readonly ILogger<SubscriberGrain<T>> _logger;

    public SubscriberGrain(ILogger<SubscriberGrain<T>> logger)
    {
        _logger = logger;
    }

    public Task ReceiveMessage(T message, StreamSequenceToken token)
    {
        _logger.LogInformation($"Received message: {message}");
        return Task.CompletedTask;
    }
}
