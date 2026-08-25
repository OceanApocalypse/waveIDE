using System;
using System.Threading.Tasks;

namespace OceanApocalypse.Wave.SDK.Clients;

/// <summary>
/// The exposed plugin API plugins can use to interact with the underlying host.
/// </summary>
public sealed class PluginContext
{
	public async Task DisconnectAsync() => throw new NotImplementedException(); // todo
}
