using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using OceanApocalypse.Wave.SDK.Infrastructure.Manifests.Blocks;

namespace OceanApocalypse.Wave.SDK.Infrastructure.Manifests;

/// <summary>
/// A representation of a plugin manifest.
/// </summary>
/// <remarks>
/// This is an object for TOML serialization and deserialization, thus
/// not being used to define actual plugins.
/// </remarks>
/// <param name="Id">The ID of the plugin.</param>
/// <param name="Version">The version of the plugin.</param>
/// <param name="EntryPoint">The plugin's entry point, or null if it has none.</param>
/// <param name="Permissions">The plugin's permissions.</param>
/// <param name="EditorSupportDeclaration">A declaration of supported and unsupported editors.</param>
public sealed record PluginManifest(
	string Id,
	string Version,
	string? EntryPoint,
	PermissionBlock Permissions,
	EditorSupportBlock EditorSupportDeclaration
)
{
	/// <summary>
	/// The plugin's display name. Default is the plugin's ID.
	/// </summary>
	[NotNull]
	public string? DisplayName { get => field ?? Id; init; }

	/// <summary>
	/// The name of the author of the plugin. Default is taken from the plugin's ID.
	/// </summary>
	[NotNull]
	public string? AuthorName
	{
		get
		{
			if (field is not null)
				return field;

			var afterFirstDot = Id.IndexOf('.', StringComparison.OrdinalIgnoreCase) + 1;
			var nextDot = Id.IndexOf('.', afterFirstDot);

			return Id[afterFirstDot..nextDot];
		}
		init;
	}

	/// <summary>
	/// A SPDX license expression. Default is unlicensed (null).
	/// </summary>
	public string? LicenseExpression { get; init; }

	/// <summary>
	/// A URL associated with the plugin. Use this to point to home pages, repos or documentation.
	/// Default is null.
	/// </summary>
	public Uri? Url { get; init; }

	/// <summary>
	/// A read-only list of the IDs whose matching plugins conflict with this one.
	/// </summary>
	public IReadOnlyList<string> Conflicts { get; init; } = [];

	/// <summary>
	/// A read-only list of themes this plugin adds.
	/// </summary>
	public IReadOnlyList<ThemeBlock> Themes { get; init; } = [];

	/// <summary>
	/// A read-only list of locales this plugin adds/improves support for.
	/// </summary>
	public IReadOnlyList<LocaleBlock> Locales { get; init; } = [];

	/// <summary>
	/// A read-only list of programming languages this plugin adds/improves support for.
	/// </summary>
	public IReadOnlyList<LanguageBlock> Languages { get; init; } = [];
}
