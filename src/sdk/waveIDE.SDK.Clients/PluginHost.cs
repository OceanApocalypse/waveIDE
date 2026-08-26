using System;
using System.Threading.Tasks;

namespace OceanApocalypse.Wave.SDK.Clients;

public static class PluginHost
{
	public static async Task<PluginContext> ConnectAsync(string[] args)
	{
		string? endpoint = args.Length == 0 ? Environment.GetEnvironmentVariable("WAVEIDE_IPC_ENDPOINT") : args[0];

		// todo: implement and fail if endpoint is null
		throw new NotImplementedException();
	}
}
