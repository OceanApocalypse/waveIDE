using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OceanApocalypse.Wave.SDK.Protocol.Transport;

/// <summary>
/// The async callback type used for when a client successfully establishes a connection with the IPC server.
/// </summary>
/// <param name="cancellationToken">The cancellation token.</param>
/// <returns>A <see cref="Stream"/>.</returns>
public delegate Task<Stream> ServerConnectionEstablishedCallback(CancellationToken cancellationToken);
