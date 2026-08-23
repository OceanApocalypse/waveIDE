using System.Collections.Generic;

namespace OceanApocalypse.Wave.SDK.Infrastructure.Manifests.Blocks;

/// <summary>
/// A block that represents the declaration of supported and unsupported editors.
/// </summary>
/// <remarks>
/// This is an object for TOML serialization and deserialization, thus
/// not being used to define editor support in plugins.
/// </remarks>
/// <param name="CompatibleWith">
/// A read-only list of editors the plugin has been tested against with a positive outcome.
/// Do not include editors that are "probably" compatible - only the ones who have undergone actual testing.
/// </param>
public record EditorSupportBlock(
	IReadOnlyList<string> CompatibleWith
)
{
	/// <summary>
	/// A read-only list of editors the plugin has been tested against with a negative outcome.
	/// Do not include editors that were not tested against - only the ones who have undergone actual testing.
	/// Default is an empty list.
	/// </summary>
	public IReadOnlyList<string> IncompatibleWith { get; init; } = [];

	/// <summary>
	/// When set to <c>true</c>, any editor not defined in <see cref="CompatibleWith"/> will be considered fully incompatible.
	/// Default is <c>false</c>.
	/// </summary>
	public bool UnspecifiedAreStrictlyIncompatible { get; init; }
}
