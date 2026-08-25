using System;
using System.IO;
using System.IO.Pipes;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace OceanApocalypse.Wave.SDK.Protocol.Transport;

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
    public static string CreateEndpoint() => CreateEndpoint(Guid.NewGuid());

    /// <summary>
    /// Creates a waveIDE endpoint from a GUID.
    /// </summary>
    /// <param name="guid">The GUID.</param>
    /// <returns>The endpoint, containing the GUID.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string CreateEndpoint(Guid guid)
	{
		var @base = $"waveIDE-{guid:N}";
		return OperatingSystem.IsWindows() ? @base : Path.Join(Path.GetTempPath(), $"{@base}.sock");
	}

    /// <summary>
    /// Turns a base endpoint name into an actual endpoint depending on the system.
    /// </summary>
    /// <param name="baseEndpoint">The base string for the endpoint.</param>
    /// <returns>The actual endpoint.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string CreateEndpoint(string baseEndpoint) =>
        OperatingSystem.IsWindows() ? baseEndpoint : Path.Join(Path.GetTempPath(), $"{baseEndpoint}.sock");

	public static async Task<Stream> ConnectToHostAsync(string endpoint, CancellationToken cancellationToken)
    {
		if (!IsPlatformSupported)
			throw new PlatformNotSupportedException("waveIDE for browser does not support plugins.");

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
		if (!IsPlatformSupported)
		    throw new PlatformNotSupportedException("waveIDE for browser does not support plugins.");

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
