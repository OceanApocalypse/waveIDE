using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OceanApocalypse.Wave.SDK.Protocol.Transport;

internal class GenericIpcTransportListener(IDisposable baseListener, Func<CancellationToken, Task<Stream>> asyncHandler) : IIpcTransportListener
{
	private bool isDisposed;

	public async Task<Stream> AcceptConnectionAsync(CancellationToken cancellationToken) => await asyncHandler(cancellationToken);

	protected virtual void Dispose(bool disposing)
	{
		if (!isDisposed)
		{
			if (disposing)
			{
				baseListener.Dispose();
			}

			isDisposed = true;
		}
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
