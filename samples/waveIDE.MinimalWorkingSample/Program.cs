using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using OceanApocalypse.Wave.SDK.Protocol.Transport;
using OceanApocalypse.Wave.SDK.Tests.IpcLauncher;

using StreamJsonRpc;

namespace OceanApocalypse.Wave.IDE.Samples.MinimalWorkingSample;

internal sealed class Program
{
	private Program() { }

	static async Task<int> Main(string[] args)
	{
		if (args.Length == 0)
		{
			await Console.Error.WriteLineAsync("No endpoint was given.").ConfigureAwait(false);
			return 1;
		}

		string endpoint = args[0];

		using var client = await IpcTransportFactory.ConnectToHostAsync(endpoint, new CancellationToken()).ConfigureAwait(false);
		using var formatter = JsonFormatterHelper.CreateFormatter();
		using var handler = new HeaderDelimitedMessageHandler(client, formatter);
		using var rpc = new JsonRpc(handler);
		var proxy = rpc.Attach<IServer>();
		rpc.StartListening();

		const string str = "My amazing little string";

		proxy.OnLogged += OnMessageLogged;
		int len = await proxy.GetLengthOfString(str).ConfigureAwait(false);
		Debug.Assert(len == str.Length);
		await proxy.Log("Minimal Working Sample").ConfigureAwait(false);

		return 0;
	}

	static void OnMessageLogged(object? sender, int args) => Console.WriteLine($"A message was logged from {sender}!");
}
