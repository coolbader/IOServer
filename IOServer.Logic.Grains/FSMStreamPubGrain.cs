using IOServer.Logic.IGrains;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Streams;
using Orleans.Runtime;
using IOServer.Logic.Models;
using Orleans.Concurrency;
namespace IOServer.Logic.Grains
{
    [StatelessWorker]
    public class FSMStreamPubGrain<TData> : Orleans.Grain, IFSMStreamPubGrain<TData> where TData : class, new()
    {
        private IAsyncStream<TData> _stream;
        public override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            // Pick a GUID for a chat room grain and chat room stream
            var guid = this.GetPrimaryKey();
            // Get one of the providers which we defined in our config
            var streamProvider = this.GetStreamProvider(Const.FSMStreamProvider);
            // Get the reference to a stream
            var streamId = StreamId.Create(Const.FSMStreamId, guid);
            _stream = streamProvider.GetStream<TData>(streamId);
            return base.OnActivateAsync(cancellationToken);
        }
   

        public Task Pubish(TData data)
        {
            return _stream.OnNextAsync(data);
        }
    }
}
