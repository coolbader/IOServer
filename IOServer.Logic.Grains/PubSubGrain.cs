using IOServer.Logic.IGrains;
using IOServer.Logic.Models;
using Microsoft.Extensions.Logging;
using Orleans.Providers;
using Orleans.Streams;

namespace IOServer.IOServer.Logic.Grains;
[StorageProvider(ProviderName = Const.FSMMemoryStorage)]
public class PubSubGrain<TData> : Grain, IPubSubGrain<TData>
{
    private readonly ILogger<PubSubGrain<TData>> _logger;
    private Dictionary<string, List<StreamSubscriptionHandle<TData>>> _subscriptions = new Dictionary<string, List<StreamSubscriptionHandle<TData>>>();

    public PubSubGrain(ILogger<PubSubGrain<TData>> logger)
    {
        _logger = logger;
    }

    public async Task Subscribe(string topic, ISubscriberGrain<TData> subscriber)
    {
        var streamProvider =this.GetStreamProvider(Const.FSMStreamProvider);
        var stream = streamProvider.GetStream<TData>(topic,Guid.NewGuid());
        var handle = await stream.SubscribeAsync<TData>(onNextAsync:subscriber.ReceiveMessage);
        if (!_subscriptions.ContainsKey(topic))
        {
            _subscriptions[topic] = new List<StreamSubscriptionHandle<TData>>();
        }
        _subscriptions[topic].Add(handle);
        _logger.LogInformation("Subscriber added to topic: {Topic}", topic);
    }

    public async Task Unsubscribe(string topic, ISubscriberGrain<TData> subscriber)
    {
        if (_subscriptions.ContainsKey(topic))
        {
            var streamProvider = this.GetStreamProvider(Const.FSMStreamProvider);
            var stream = streamProvider.GetStream<TData>(topic,Guid.NewGuid());
            foreach (var handle in _subscriptions[topic])
            {
                await handle.UnsubscribeAsync();
                _subscriptions[topic].RemoveAll(h => h.Equals(handle));
                _subscriptions.Remove(topic);
            }
            _logger.LogInformation("Subscriber removed from topic: {Topic}", topic);
        }
    }

    public async Task Publish(string topic, TData message)
    {
        if (_subscriptions.ContainsKey(topic))
        {
            var streamProvider = this.GetStreamProvider(Const.FSMStreamProvider);
            var stream = streamProvider.GetStream<TData>(topic,Guid.NewGuid() );
            await stream.OnNextAsync(message);
            _logger.LogInformation("Message published to topic: {Topic}, message: {Message}", topic, message);
        }
    }
}




