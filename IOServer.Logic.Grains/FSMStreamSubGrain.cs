using IOServer.Logic.IGrains;
using IOServer.Logic.Models;
using Newtonsoft.Json;
using Orleans.Concurrency;
using Orleans.Runtime;
using Orleans.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IOServer.Logic.Grains
{
    [ImplicitStreamSubscription(streamNamespace: Const.FSMStreamId),StatelessWorker]
    public class FSMStreamSubGrain<TData>:Orleans.Grain, IFSMStreamSubGrain<TData> where TData : class,new()
    {
        public override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            // Create a GUID based on our GUID as a grain
            var guid = this.GetPrimaryKey();
            // Get one of the providers which we defined in config
            var streamProvider =this. GetStreamProvider(Const.FSMStreamProvider);
            // Get the reference to a stream
            var streamId = StreamId.Create(Const.FSMStreamId, guid);
            var stream = streamProvider.GetStream<TData>(streamId);
            // Set our OnNext method to the lambda which simply prints the data.
            // This doesn't make new subscriptions, because we are using implicit
            // subscriptions via [ImplicitStreamSubscription].
            await stream.SubscribeAsync<TData>(OnSubscribeAsync);
            //await stream.SubscribeAsync<TData>( (data, token) =>
            //{
            //    Console.WriteLine(data);
            //    return Task.CompletedTask;
            //});
            await base.OnActivateAsync(cancellationToken);
        }

        private async Task OnSubscribeAsync(TData data,StreamSequenceToken token)
        {
            Console.WriteLine(typeof(TData).Name);
            Console.WriteLine(JsonConvert.SerializeObject(data));
        }
    }
}
