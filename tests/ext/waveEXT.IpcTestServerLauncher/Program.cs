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
using System.Linq;
using System.Threading.Tasks;

using OceanApocalypse.Wave.Extensibility.Protocol.AotCompatibility;
using OceanApocalypse.Wave.Extensibility.Protocol.Transport;

using StreamJsonRpc;

namespace OceanApocalypse.Wave.Extensibility.Tests.IpcLauncher.Servers;

internal sealed class Program
{
    private Program() { }

    private static async Task Main(string[] args)
    {
        string endpoint = args.FirstOrDefault(IpcTransportFactory.CreateEndpointString());
        using IIpcTransportListener listener = IpcTransportFactory.CreateServer(endpoint);
        Console.WriteLine($"Server started at {endpoint}.");

        Stream server = await listener.AcceptConnectionAsync(new()).ConfigureAwait(false);

        using SystemTextJsonFormatter formatter = NativeJsonHelper.CreateSourceGenerationFormatter();
        using HeaderDelimitedMessageHandler handler = new(server, formatter);
        using JsonRpc rpc = new(handler);

        var targetMetadata = RpcTargetMetadata.FromShape<IServer>();

        rpc.AddLocalRpcTarget(targetMetadata, new Server(), null);
        rpc.StartListening();

        await rpc.Completion.ConfigureAwait(false);
    }
}
