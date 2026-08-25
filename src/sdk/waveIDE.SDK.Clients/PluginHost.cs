using System;
using System.Threading.Tasks;

namespace OceanApocalypse.Wave.SDK.Clients;

public static class PluginHost
{
	public static async Task<PluginContext> ConnectAsync(string[] args)
	{
		string? endpoint;

		if (args.Length < 1)
			endpoint = Environment.GetEnvironmentVariable("WAVEIDE_IPC_ENDPOINT");
		else
			endpoint = args[0];

		// todo: implement and fail if endpoint is null
		throw new NotImplementedException();
	}
}
