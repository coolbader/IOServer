namespace IOServer.IOServer.Logic.Grains;
public class PubSubGrain<T> : Grain, IPubSubGrain<T>
{
    private readonly ILogger<PubSubGrain<T>> _logger;
    private Dictionary<string, List<StreamSubscriptionHandle<T>>> _subscriptions = new Dictionary<string, List<StreamSubscriptionHandle<T>>>();

    public PubSubGrain(ILogger<PubSubGrain<T>> logger)
    {
        _logger = logger;
    }

    public async Task Subscribe(string topic, ISubscriberGrain<T> subscriber)
    {
        var streamProvider = GetStreamProvider("SMSProvider");
        var stream = streamProvider.GetStream<T>(Guid.NewGuid(), topic);
        var handle = await stream.SubscribeAsync(subscriber.ReceiveMessage);
        if (!_subscriptions.ContainsKey(topic))
        {
            _subscriptions[topic] = new List<StreamSubscriptionHandle<T>>();
        }
        _subscriptions[topic].Add(handle);
        _logger.LogInformation("Subscriber added to topic: {Topic}", topic);
    }

    public async Task Unsubscribe(string topic, ISubscriberGrain<T> subscriber)
    {
        if (_subscriptions.ContainsKey(topic))
        {
            var streamProvider = GetStreamProvider("SMSProvider");
            var stream = streamProvider.GetStream<T>(Guid.NewGuid(), topic);
            foreach (var handle in _subscriptions[topic])
            {
                await handle.UnsubscribeAsync();
            }
            _subscriptions[topic].RemoveAll(h => h.Equals(handle));
            _logger.LogInformation("Subscriber removed from topic: {Topic}", topic);
        }
    }

    public async Task Publish(string topic, T message)
    {
        if (_subscriptions.ContainsKey(topic))
        {
            var streamProvider = GetStreamProvider("SMSProvider");
            var stream = streamProvider.GetStream<T>(Guid.NewGuid(), topic);
            await stream.OnNextAsync(message);
            _logger.LogInformation("Message published to topic: {Topic}, message: {Message}", topic, message);
        }
    }
}

public interface IPubSubGrain<T> : IGrainWithGuidKey
{
    Task Subscribe(string topic, ISubscriberGrain<T> subscriber);
    Task Unsubscribe(string topic, ISubscriberGrain<T> subscriber);
    Task Publish(string topic, T message);
}

public interface ISubscriberGrain<T> : IGrainWithGuidKey
{
    Task ReceiveMessage(T message);
}

