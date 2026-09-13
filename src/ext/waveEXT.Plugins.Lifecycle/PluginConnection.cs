using System;
using System.IO;
using System.Threading.Tasks;

using OceanApocalypse.Wave.Extensibility.Protocol.AotCompatibility;

using StreamJsonRpc;

namespace OceanApocalypse.Wave.Extensibility.Plugins.Lifecycle;

/// <summary>
/// An out-of-process connection to a plugin.
/// </summary>
public sealed class PluginConnection : IAsyncDisposable
{
    private bool isDisposed;

    private readonly Stream pluginPipe;
    private readonly SystemTextJsonFormatter formatter;
    private readonly HeaderDelimitedMessageHandler messageHandler;
    private readonly JsonRpc pluginRpc;

    /// <summary>
    /// Initializes a connection instance given the <see cref="Stream" /> that is to
    /// be used by RPC.
    /// </summary>
    public PluginConnection(Stream pluginPipe)
    {
        this.pluginPipe = pluginPipe;
        formatter = NativeJsonHelper.CreateSourceGenerationFormatter();
        messageHandler = new(this.pluginPipe, formatter);
        pluginRpc = new JsonRpc(messageHandler);
    }

    /// <summary>
    /// An awaitable property that marks plugin termination, which can be a safe exit,
    /// a crash or any other form of exit.
    /// <code lang="csharp">
    /// await conn.Termination;
    /// </code>
    /// </summary>
    public Task Termination => pluginRpc.Completion;

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }

    private async ValueTask DisposeAsync(bool disposing)
    {
        if (isDisposed)
            return;

        if (disposing)
        {
            pluginRpc.Dispose();
            await messageHandler.DisposeAsync();
            formatter.Dispose();
            await pluginPipe.DisposeAsync();
        }

        isDisposed = true;
    }
}
