using OceanApocalypse.Wave.SDK.Clients.APIs.Workspaces;

namespace OceanApocalypse.Wave.SDK.Clients;

/// <summary>
/// The exposed plugin API plugins can use to interact with the underlying host.
/// </summary>
public sealed class PluginContext
{
    public WorkspaceApi Workspace { get; }
}
