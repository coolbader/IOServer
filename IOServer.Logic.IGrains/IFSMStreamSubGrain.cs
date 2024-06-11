using IOServer.Logic.Models;
using Orleans.Runtime;
using Orleans.Streams;
using Orleans;
namespace IOServer.Logic.IGrains
{

    public interface IFSMStreamSubGrain<TData>:IGrainWithGuidKey where TData : class,new()
    {
       
    }
}
