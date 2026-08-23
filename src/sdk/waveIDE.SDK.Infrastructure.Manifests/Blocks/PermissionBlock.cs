using System.Collections.Generic;

namespace OceanApocalypse.Wave.SDK.Infrastructure.Manifests.Blocks;

/// <summary>
/// A block that represents the declaration of the necessary permissions for the plugin
/// to function correctly.
/// </summary>
/// <remarks>
/// This is an object for TOML serialization and deserialization, thus
/// not being used to define permissions for plugins.
/// </remarks>
/// <param name="Scopes">A read-only list of the scopes the plugin needs access to.</param>
/// <param name="LoadDynamically">
/// When set to <c>true</c>, the plugin will be allowed to dynamically request intents at runtime.
/// Setting this to <c>true</c> does not mean the intents will be automatically granted, as the host
/// has the ultimate saying in what happens.
/// </param>
public record PermissionBlock(
	IReadOnlyList<string> Scopes,
	bool LoadDynamically
);
