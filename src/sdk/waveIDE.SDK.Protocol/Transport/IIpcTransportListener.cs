using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OceanApocalypse.Wave.SDK.Protocol.Transport;

/// <summary>
/// Represents a listener for IPC connections.
/// </summary>
public interface IIpcTransportListener : IDisposable
{
	/// <summary>
	/// Waits for a connection to the server and accepts it.
	/// </summary>
	/// <param name="cancellationToken">A token that, when cancelled, cancels the operation.</param>
	/// <returns>The server pipe, now with the connection established.</returns>
	Task<Stream> AcceptConnectionAsync(CancellationToken cancellationToken);
}
