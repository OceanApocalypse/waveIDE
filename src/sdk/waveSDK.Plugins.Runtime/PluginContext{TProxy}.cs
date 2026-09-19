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

using OceanApocalypse.Wave.Extensibility.Protocol.AotCompatibility;
using OceanApocalypse.Wave.SDK.Plugins.Runtime.States;

using StreamJsonRpc;

namespace OceanApocalypse.Wave.SDK.Plugins.Runtime;

/// <summary>
/// The exposed plugin API plugins can use to interact with the underlying host.
/// </summary>
public sealed class PluginContext<TProxy> : IAsyncDisposable
    where TProxy : IPluginProxy
{
    private readonly Stream clientPipe;
    private readonly JsonRpc clientRpc;
    private bool wasDisposed;

    /// <inheritdoc/>
    public event EventHandler<PluginStateChangedEventArgs>? OnStateChanged;

    internal PluginContext(Stream pipe, TProxy proxy)
    {
        clientPipe = pipe;

        using SystemTextJsonFormatter formatter = NativeJsonHelper.CreateSourceGenerationFormatter();
        using HeaderDelimitedMessageHandler handler = new(clientPipe, formatter);
        var targetMetadata = RpcTargetMetadata.FromShape<IPluginProxy>();

        clientRpc = new(handler);

        clientRpc.AddLocalRpcTarget(targetMetadata, proxy, null);
    }

    private async Task DisconnectAsync()
    {
        throw new NotImplementedException(); // todo
    }

    private async ValueTask DisposeAsync(bool disposing)
    {
        if (!wasDisposed)
        {
            if (disposing)
            {
                clientRpc.Dispose();
                await clientPipe.DisposeAsync();
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
        await DisconnectAsync();
        await DisposeAsync(disposing: true);
        GC.SuppressFinalize(this);
    }
}
