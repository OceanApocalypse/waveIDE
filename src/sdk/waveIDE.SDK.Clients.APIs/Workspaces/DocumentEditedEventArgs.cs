using System;

namespace OceanApocalypse.Wave.SDK.Clients.APIs.Workspaces;

/// <summary>
/// Arguments for events that are triggered when documents are edited.
/// </summary>
public class DocumentEditedEventArgs : EventArgs
{
	/// <summary>
	/// The ID of the edited document.
	/// </summary>
	public Guid DocId { get; set; }

	// todo: add actual args for what was edited and where
}
