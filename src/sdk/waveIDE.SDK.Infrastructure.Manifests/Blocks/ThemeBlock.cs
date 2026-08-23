namespace OceanApocalypse.Wave.SDK.Infrastructure.Manifests.Blocks;

/// <summary>
/// A block that represents the declaration of a theme.
/// </summary>
/// <remarks>
/// This is an object for TOML serialization and deserialization, thus
/// not being used to define themes in plugins.
/// </remarks>
/// <param name="Name">The name of the theme.</param>
/// <param name="FilePath">The path to the file that contains the actual theme data.</param>
public record ThemeBlock(
	string Name,
	string FilePath
);
