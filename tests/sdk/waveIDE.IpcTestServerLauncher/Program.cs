using System;
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
		var endpoint = args.FirstOrDefault(IpcTransportFactory.CreateEndpoint());
		using var listener = IpcTransportFactory.CreateServer(endpoint, out var callback);
		var server = await callback(new()).ConfigureAwait(false);
		var rpc = JsonRpc.Attach(server);

		// todo: listen asynchronously
		throw new NotImplementedException();
	}
}
