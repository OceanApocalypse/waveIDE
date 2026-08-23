using System;
using System.IO;
using System.IO.Pipes;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace OceanApocalypse.Wave.SDK.Protocol.Transport;

public static class IpcTransportFactory
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string CreateEndpoint()
    {
        var @base = $"waveIDE-{Guid.NewGuid():N}";
        return OperatingSystem.IsWindows() ? @base : Path.Join(Path.GetTempPath(), $"{@base}.sock");
    }

    public static async Task<Stream> ConnectToHostAsync(string endpoint, CancellationToken cancellationToken)
    {
        if (OperatingSystem.IsWindows())
        {
            var pipe = new NamedPipeClientStream(".", endpoint, PipeDirection.InOut, PipeOptions.Asynchronous);
            await pipe.ConnectAsync(cancellationToken);
            return pipe;
        }

        var socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);
        await socket.ConnectAsync(new UnixDomainSocketEndPoint(endpoint), cancellationToken);
        return new NetworkStream(socket);
    }

    public static IDisposable CreateServer(string endpoint, out ServerConnectionEstablishedCallback connectionCallback)
    {
        if (OperatingSystem.IsWindows())
        {
            var server = new NamedPipeServerStream(
                endpoint, PipeDirection.InOut, 1,
                PipeTransmissionMode.Byte, PipeOptions.Asynchronous
            );

            connectionCallback = async cancellationToken =>
            {
                await server.WaitForConnectionAsync(cancellationToken);
                return server;
            };

            return server;
        }

        if (File.Exists(endpoint))
            File.Delete(endpoint);

        var listener = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);
        listener.Bind(new UnixDomainSocketEndPoint(endpoint));
        listener.Listen(1);

        connectionCallback = async cancellationToken =>
        {
            var accepted = await listener.AcceptAsync(cancellationToken);
            return new NetworkStream(accepted, true);
        };

        return listener;
    }
}
