using System;

namespace OceanApocalypse.Wave.SDK.Clients.APIs.Workspaces;

/// <summary>
/// Arguments for evens that are triggered when the document focus changes.
/// </summary>
public class ActiveDocumentChangedEventArgs : EventArgs
{
	/// <summary>
	/// The document ID of the previously active document.
	/// </summary>
	public int PreviousDocId { get; set; }

	/// <summary>
	/// The document ID of the currently active document.
	/// </summary>
	public int CurrentDocId { get; set; }

	/// <summary>
	/// Whether the current document is read-only.
	/// </summary>
	public bool IsCurrentReadOnly { get; set; }
}
