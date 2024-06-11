using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOServer.Logic.IGrains;
public interface IFSMGrain<TState,TData> where TState : Enum where TData : class, new()
{

    Task TransitionTo(TState newState);
    Task<TState> GetCurrentState();
    Task<TData> GetCurrentData();
    Task Register();
    Task Publish(TData data);
}
