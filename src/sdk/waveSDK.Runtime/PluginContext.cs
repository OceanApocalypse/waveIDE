/*
 * Licensed to Ocean Apocalypse under one or more contributor
 * license agreements. See the NOTICE file distributed
 * with this work for additional information regarding
 * copyright ownership. Ocean Apocalypse licenses this file to
 * you under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance
 * with the License. You may obtain a copy of the License at
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied. See the License for the
 * specific language governing permissions and limitations
 * under the License.
*/

using System;
using System.IO;
using System.Threading.Tasks;

using OceanApocalypse.Wave.Extensibility.Plugins.SDK.Runtime.States;

using StreamJsonRpc;

namespace OceanApocalypse.Wave.Extensibility.Plugins.SDK.Runtime;

/// <summary>
/// The exposed plugin API plugins can use to interact with the underlying host.
/// </summary>
public sealed class PluginContext : IAsyncDisposable
{
    private readonly Stream clientPipe;
    private readonly JsonRpc clientRpc;
    private bool wasDisposed;

    /// <summary>
    /// The plugin proxy.
    /// </summary>
    public IPluginProxy Proxy { get; }

    /// <inheritdoc/>
    public event EventHandler<PluginStateChangedEventArgs>? OnStateChanged;

    /// <summary>
    /// Creates a new plugin context.
    /// </summary>
    /// <param name="pipe">The client pipe.</param>
    public PluginContext(Stream pipe)
    {
        clientPipe = pipe;
        clientRpc = JsonRpc.Attach(clientPipe);
        clientRpc.StartListening();
        Proxy = clientRpc.Attach<IPluginProxy>();
    }

    private async ValueTask DisposeAsync(bool disposing)
    {
        if (!wasDisposed)
        {
            if (disposing)
            {
                OnStateChanged?.Invoke(this, new() { NewState = PluginState.Disconnected });
                clientRpc.Dispose();
                await clientPipe.DisposeAsync();
                await Proxy.DisposeAsync();
            }

            wasDisposed = true;
        }
    }

    /// <inheritdoc/>
    /// <remarks>
    /// This method also disconnects from the host.
    /// </remarks>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(disposing: true);
        GC.SuppressFinalize(this);
    }
}
