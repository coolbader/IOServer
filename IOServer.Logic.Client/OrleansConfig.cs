

using Orleans.Runtime;
using IOServer.Logic.Models;
using Orleans.Serialization;

namespace IOServer.Logic.Client
{
    public sealed class OrleansConfig
    {
        public static void OrleansServer(ISiloBuilder builder)
        {
            builder.UseLocalhostClustering()
                .AddMemoryStreams(Const.FSMStreamProvider);
        }

        public static void OrleansClient(IClientBuilder builder)
        {
            builder.UseLocalhostClustering()
                .AddMemoryStreams(Const.FSMStreamProvider)
                 .Services.AddSerializer(serializerBuilder =>
                 {
                     serializerBuilder.AddJsonSerializer(
                         isSupported: type => type.Namespace.StartsWith("IOServer.Logic"));
                 });
            ;
              
        }
    }
}
