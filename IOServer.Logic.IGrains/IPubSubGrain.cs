namespace IOServer.Logic.IGrains;

public interface IPubSubGrain<T> : IGrainWithGuidKey
{
    Task Subscribe(string topic, ISubscriberGrain<T> subscriber);
    Task Unsubscribe(string topic, ISubscriberGrain<T> subscriber);
    Task Publish(string topic, T message);
}