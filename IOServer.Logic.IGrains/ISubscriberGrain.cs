using Orleans.Streams;

namespace IOServer.Logic.IGrains;

public interface ISubscriberGrain<T> : IGrainWithGuidKey
{
    Task ReceiveMessage(T message, StreamSequenceToken token);
}

