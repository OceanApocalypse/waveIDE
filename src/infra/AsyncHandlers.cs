using System.Threading;
using System.Threading.Tasks;

namespace OceanApocalypse.Wave;

/// <summary>
/// An asynchronous event handler with custom argument types.
/// </summary>
/// <typeparam name="TEventArgs">The type of event arguments.</typeparam>
/// <param name="e">The event's arguments.</param>
/// <param name="token">The associated cancellation token.</param>
/// <returns>A task.</returns>
public delegate Task AsyncHandler<in TEventArgs>(TEventArgs e, CancellationToken token = default);
