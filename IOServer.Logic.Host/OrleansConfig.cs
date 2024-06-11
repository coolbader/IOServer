

using Orleans.Runtime;
using IOServer.Logic.Models;
using Orleans.Serialization;
namespace IOServer.Logic.Host
{
    public sealed class OrleansConfig
    {
        public static void OrleansServer(ISiloBuilder builder)
        {
            builder.UseLocalhostClustering()
                .AddMemoryStreams(Const.FSMStreamProvider)
    .AddMemoryGrainStorage(Const.FSMMemoryStorage)
    .AddMemoryGrainStorageAsDefault()
    .Services.AddSerializer(serializerBuilder =>
    {
        serializerBuilder.AddJsonSerializer(
            isSupported: type => type.Namespace.StartsWith("IOServer.Logic"));
    });
        }
    }
}
