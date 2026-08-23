using System.Globalization;

namespace OceanApocalypse.Wave.SDK.Infrastructure.Manifests.Blocks;

/// <summary>
/// A representation of the declaration of a locale.
/// </summary>
/// <remarks>
/// This is an object for TOML serialization and deserialization, thus
/// not being used to define locales in plugins.
/// </remarks>
/// <param name="IsoName">
/// The ISO 639-1 or ISO 639-3 name of the locale.
/// (See <see cref="CultureInfo.TwoLetterISOLanguageName"/>.)
/// </param>
/// <param name="CultureName">
/// The name followed by the associated country code. It's either
/// "{<see cref="IsoName"/>}-{CountryCode}" or the region code.
/// (See <see cref="CultureInfo.Name"/>.)
/// </param>
/// <param name="FilePath">The path to the file that contains the actual locale data.</param>
public record LocaleBlock(
	string IsoName,
	string CultureName,
	string FilePath
);
