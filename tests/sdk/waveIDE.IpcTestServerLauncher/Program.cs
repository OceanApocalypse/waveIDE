using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using OceanApocalypse.Wave.SDK.Protocol.Transport;

using StreamJsonRpc;

namespace OceanApocalypse.Wave.SDK.Tests.IpcLauncher;

internal sealed class Program
{
	private Program() { }

	static async Task Main(string[] args)
	{
		var endpoint = args.FirstOrDefault(IpcTransportFactory.CreateEndpointString());
		using var listener = IpcTransportFactory.CreateServer(endpoint);
		Console.WriteLine($"Server started at {endpoint}.");

		var server = await listener.AcceptConnectionAsync(new()).ConfigureAwait(false);

		using var formatter = JsonFormatterHelper.CreateFormatter();
		using var handler = new HeaderDelimitedMessageHandler(server, formatter);
		using var rpc = new JsonRpc(handler);

		var targetMetadata = RpcTargetMetadata.FromShape<IServer>();

		rpc.AddLocalRpcTarget(targetMetadata, new Server(), null);
		rpc.StartListening();

		await rpc.Completion.ConfigureAwait(false);
	}
}
