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
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using OceanApocalypse.Wave.Extensibility.Protocol.AotCompatibility;
using OceanApocalypse.Wave.Extensibility.Protocol.Transport;
using OceanApocalypse.Wave.Extensibility.Tests.IpcLauncher.Servers;

using StreamJsonRpc;

namespace OceanApocalypse.Wave.Extensibility.Tests.IpcLauncher.Clients;

internal sealed class Program
{
    private Program() { }

    private static async Task<int> Main(string[] args)
    {
        if (args.Length == 0)
        {
            await Console.Error.WriteLineAsync("No endpoint was given.").ConfigureAwait(false);
            return 1;
        }

        string endpoint = args[0];

        using Stream client = await IpcTransportFactory.ConnectToHostAsync(endpoint, new CancellationToken()).ConfigureAwait(false);
        using SystemTextJsonFormatter formatter = NativeJsonHelper.CreateSourceGenerationFormatter();
        using HeaderDelimitedMessageHandler handler = new(client, formatter);
        using JsonRpc rpc = new(handler);
        IServer proxy = rpc.Attach<IServer>();
        rpc.StartListening();

        const string str = "My amazing little string";

        proxy.OnLogged += OnMessageLogged;
        int len = await proxy.GetLengthOfString(str).ConfigureAwait(false);
        Debug.Assert(len == str.Length);

        await proxy.Log("Minimal Working Sample").ConfigureAwait(false);
        await proxy.Log("See... it doesn't die").ConfigureAwait(false);

        await rpc.Completion.ConfigureAwait(false);

        return 0;
    }

    private static void OnMessageLogged(object? sender, int args) => Console.WriteLine($"A message was logged from {sender}!");
}
