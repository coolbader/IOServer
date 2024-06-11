namespace IOServer.Logic.IGrains;
using IOServer.Logic.Models;
public interface IBusinessLogicGrain : IFSMGrain<BusinessState,DataPoint>,IGrainWithStringKey
{
    Task StartProcess(DataPoint inputData);
    Task StartProcess();
    Task WorkProcess();
    Task WorkProcess(DataPoint inputData);
    Task CompletedProcess();
    Task CompletedProcess(DataPoint inputData);
    Task CompleteProcess();
    Task CompleteProcess(DataPoint inputData);
    Task Listen();
    Task Listen(DataPoint inputData);
    Task End();
    Task End(DataPoint inputData);
    Task<DataPoint> GetResult();
    Task<BusinessState> GetBusinessState();
}
