namespace IOServer.Logic.Grains;
using IOServer.Logic.Models;
using IOServer.Logic.IGrains;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using Orleans;
using Orleans.Runtime;

public class BusinessLogicGrain : FSMGrain<BusinessState, DataPoint>, IBusinessLogicGrain
{
    private ILogger<FSMGrain<BusinessState, DataPoint>> _logger;
    private IPersistentState<FSMModel<BusinessState, DataPoint>> _state;
    public BusinessLogicGrain([PersistentState(
            stateName: Const.BusinessLogicStateName,
            storageName: Const.FSMMemoryStorage)]
        IPersistentState<FSMModel<BusinessState, DataPoint>> state, ILogger<FSMGrain<BusinessState, DataPoint>> logger):base(state,logger)
    {
        _logger = logger;
        _state = state;
    }

    //public BusinessLogicGrain(ILogger<FSMGrain<BusinessState, DataPoint>> logger) : base(logger)
    //{
    //    _logger = logger;
    //}
    public Task StartProcess()
    {
        TransitionTo(BusinessState.Start);
        _logger.LogInformation("Process started with data: {Data}", _state. State.data);
        return Task.CompletedTask;
    }
    public Task StartProcess(DataPoint inputData)
    {
        this.SetData(inputData);
        TransitionTo(BusinessState.Start);
        _logger.LogInformation("Process started with data: {Data}", _state.State.data);
        return Task.CompletedTask;
    }

    public Task WorkProcess()
    {
        if (_state.State.state == BusinessState.Start)
        {
            // Simulate data processing for step 1
            //State.data = _data.ToUpper();
            TransitionTo(BusinessState.Work);
            _logger.LogInformation("Processing step 1 completed. Data: {Data}", _state.State.data);
        }
        return Task.CompletedTask;
    }
    public Task WorkProcess(DataPoint inputData)
    {
        if (_state.State.state == BusinessState.Start)
        {
            this.SetData(inputData);
            TransitionTo(BusinessState.Work);
            _logger.LogInformation("Processing step 1 completed. Data: {Data}", _state.State.data);
        }
        return Task.CompletedTask;
    }
    public Task CompletedProcess(DataPoint inputData)
    {
        if (_state.State.state != BusinessState.Completed)
        {
            this.SetData(inputData);
            TransitionTo(BusinessState.Completed);
            _logger.LogInformation("Processing step 1 completed. Data: {Data}", _state.State.data);
        }
        return Task.CompletedTask;
    }
    public Task CompletedProcess()
    {
        if (_state.State.state != BusinessState.Completed)
        {
            TransitionTo(BusinessState.Completed);
            _logger.LogInformation("Processing step 2 completed. Result: {Result}", _state.State.data);
        }
        return Task.CompletedTask;
    }

    public Task CompleteProcess()
    {
        if (_state.State.state == BusinessState.Completed)
        {
         
            _logger.LogInformation("Process completed. Final result: {Result}", _state.State.data);
            TransitionTo(BusinessState.Listening);
        }
        return Task.CompletedTask;
    }

    public Task Listen()
    {
        if (_state.State.state == BusinessState.Listening)
        {
            _logger.LogInformation("Listening for further instructions.");
            // Simulate listening state
        }
        return Task.CompletedTask;
    }

    public Task End()
    {
        if (_state.State.state == BusinessState.Listening)
        {
            TransitionTo(BusinessState.Ended);
            _logger.LogInformation("Process ended.");
        }
        return Task.CompletedTask;
    }

    public Task<DataPoint> GetResult()
    {
      return Task.FromResult(_state.State.data);
    }
    public Task<BusinessState> GetBusinessState() { 
        return Task.FromResult(_state.State.state);
    }

    public Task CompleteProcess(DataPoint inputData)
    {
        if (_state.State.state == BusinessState.Completed)
        {
            this.SetData(inputData);
            _logger.LogInformation("Process completed. Final result: {Result}", _state.State.data);
            TransitionTo(BusinessState.Listening);
        }
        return Task.CompletedTask;
    }

    public Task Listen(DataPoint inputData)
    {
        if (_state.State.state == BusinessState.Listening)
        {
            this.SetData(inputData);
            _logger.LogInformation("Listening for further instructions.");
            // Simulate listening state
        }
        return Task.CompletedTask;
    }

    public Task End(DataPoint inputData)
    {
        if (_state.State.state == BusinessState.Listening)
        {
            this.SetData(inputData);
            TransitionTo(BusinessState.Ended);
            _logger.LogInformation("Process ended.");
        }
        return Task.CompletedTask;
    }
}
// 泛型 FSMGrain (Finite State Machine Grain) 的实现

