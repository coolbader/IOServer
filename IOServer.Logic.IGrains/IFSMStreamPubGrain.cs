namespace IOServer.Logic.IGrains;
using Orleans;
public interface IFSMStreamPubGrain<TData> : IGrainWithGuidKey where TData : class, new()
{

    Task Pubish(TData data);
}
