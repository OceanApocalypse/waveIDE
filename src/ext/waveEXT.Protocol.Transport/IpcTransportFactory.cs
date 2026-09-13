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
using System.IO.Pipes;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace OceanApocalypse.Wave.Extensibility.Protocol.Transport;

/// <summary>
/// A factory class for IPC transports.
/// </summary>
public static class IpcTransportFactory
{
    /// <summary>
    /// Indicates whether the current platform supports IPC transports.
    /// </summary>
    public static bool IsPlatformSupported => !OperatingSystem.IsBrowser();

    /// <summary>
    /// Creates a waveIDE endpoint by using a brand-new GUID.
    /// </summary>
    /// <returns>The endpoint, containing the created GUID.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string CreateEndpointString() => CreateEndpointString(Guid.NewGuid());

    /// <summary>
    /// Creates a waveIDE endpoint from a GUID.
    /// </summary>
    /// <param name="guid">The GUID.</param>
    /// <returns>The endpoint, containing the GUID.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string CreateEndpointString(Guid guid)
    {
        string @base = $"waveIDE-{guid:N}";
        return OperatingSystem.IsWindows() ? @base : Path.Join(Path.GetTempPath(), $"{@base}.sock");
    }

    /// <summary>
    /// Turns a base endpoint name into an actual endpoint depending on the system.
    /// </summary>
    /// <param name="baseEndpoint">The base string for the endpoint.</param>
    /// <returns>The actual endpoint.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string CreateEndpointString(string baseEndpoint) =>
        OperatingSystem.IsWindows() ? baseEndpoint : Path.Join(Path.GetTempPath(), $"{baseEndpoint}.sock");

    /// <summary>
    /// Connects to the server at the given endpoint.
    /// </summary>
    /// <param name="endpoint">The endpoint to connect to.</param>
    /// <param name="cancellationToken">A token that, when cancelled, cancels the operations.</param>
    /// <returns>The IPC transport listener.</returns>
    /// <exception cref="PlatformNotSupportedException">Platform does not support IPC transports.</exception>
    public static async Task<Stream> ConnectToHostAsync(string endpoint, CancellationToken cancellationToken)
    {
        if (!IsPlatformSupported)
            throw new PlatformNotSupportedException("waveIDE for browser does not support plugins.");

        if (OperatingSystem.IsWindows())
        {
            NamedPipeClientStream pipe = new(".", endpoint, PipeDirection.InOut, PipeOptions.Asynchronous);
            await pipe.ConnectAsync(cancellationToken);
            return pipe;
        }

        Socket socket = new(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);
        await socket.ConnectAsync(new UnixDomainSocketEndPoint(endpoint), cancellationToken);
        return new NetworkStream(socket);
    }

    /// <summary>
    /// Creates a server at the default endpoint, which is the string <c>"waveIDE-"</c>
    /// followed by a random GUID.
    /// </summary>
    /// <returns>The IPC transport listener.</returns>
    public static IIpcTransportListener CreateServerWithDefaultEndpoint() => CreateServer(CreateEndpointString());

    /// <summary>
    /// Creates a server at the given endpoint.
    /// </summary>
    /// <param name="endpoint">The endpoint to start the server at.</param>
    /// <returns>The IPC transport listener.</returns>
    /// <exception cref="PlatformNotSupportedException">Platform does not support IPC transports.</exception>
    public static IIpcTransportListener CreateServer(string endpoint)
    {
        if (!IsPlatformSupported)
            throw new PlatformNotSupportedException("Platform does not support IPC transports.");

        if (OperatingSystem.IsWindows())
        {
            NamedPipeServerStream server = new(
                endpoint, PipeDirection.InOut, 1,
                PipeTransmissionMode.Byte, PipeOptions.Asynchronous
            );

            return new GenericIpcTransportListener(server, async cancellationToken =>
            {
                await server.WaitForConnectionAsync(cancellationToken);
                return server;
            });
        }

        if (File.Exists(endpoint))
            File.Delete(endpoint);

        Socket listener = new(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);
        listener.Bind(new UnixDomainSocketEndPoint(endpoint));
        listener.Listen(1);

        return new GenericIpcTransportListener(listener, async cancellationToken =>
        {
            Socket accepted = await listener.AcceptAsync(cancellationToken);
            return new NetworkStream(accepted, true);
        });
    }
}
