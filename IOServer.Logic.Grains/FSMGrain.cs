﻿using IOServer.Logic.IGrains;
using IOServer.Logic.Models;
using Microsoft.Extensions.Logging;
using Orleans.Providers;
using Orleans.Runtime;
using System;

namespace IOServer.Logic.Grains;
public class FSMModel<TState, TData> where TState : Enum
where TData : class, new()
{
    public TState? state { get; set; }
    public TData? data { get; set; }
}
[StorageProvider(ProviderName = Const.FSMMemoryStorage)]
public class FSMGrain<TState, TData> : Orleans.Grain, IFSMGrain<TState, TData> where TState : Enum
where TData : class,new()
{
    private IFSMStreamPubGrain<TData> PubGrain;
    private IFSMStreamSubGrain<TData> SubGrain;
    private ILogger<FSMGrain<TState, TData>> _logger;
    private IPersistentState<FSMModel<TState, TData>> _state;

    public FSMGrain([PersistentState(
            stateName: Const.FSMStateName,
            storageName: Const.FSMMemoryStorage)]
        IPersistentState<FSMModel<TState, TData>> state, ILogger<FSMGrain<TState, TData>> logger)
    {
        _state = state;
        _logger = logger;
    }
    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        //Guid guid= Guid.NewGuid();
        //PubGrain = GrainFactory.GetGrain<IFSMStreamPubGrain<TData>>(guid);
        //SubGrain = GrainFactory.GetGrain<IFSMStreamSubGrain<TData>>(guid);
        _state.State.state = default(TState); // 默认状态为枚举的第一个值
        return base.OnActivateAsync(cancellationToken);
    }

    public Task Register()
    {
        Guid guid = Guid.NewGuid();
        PubGrain = GrainFactory.GetGrain<IFSMStreamPubGrain<TData>>(guid);
        SubGrain = GrainFactory.GetGrain<IFSMStreamSubGrain<TData>>(guid);
        return Task.CompletedTask;
    }

    public async Task Publish(TData data)
    {
        await PubGrain.Pubish(data);
    }

    public override Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
    {
        _state.State.state = default(TState); // 默认状态为枚举的第一个值
        return base.OnDeactivateAsync(reason, cancellationToken);
    }

    public async Task SetData(TData data)
    {
        await Register();
        _state.State.data = null;
        _state.State.data = data;
        await PubGrain.Pubish(data);
        // Task.CompletedTask;
    }

    public Task TransitionTo(TState newState)
    {

        _state.State.state = newState;
        _logger.LogInformation("Transitioned to new state: {_state.State.state}", _state.State.state);
        return Task.CompletedTask;
    }

    public Task<TState> GetCurrentState()
    {
        return Task.FromResult(result: _state.State.state);
    }
    public Task<TData> GetCurrentData()
    {
        return Task.FromResult(_state.State.data);
    }
}

